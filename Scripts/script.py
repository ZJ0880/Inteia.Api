from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.chrome.options import Options
import time
from bs4 import BeautifulSoup
import re

# Configurar el navegador
options = Options()
options.binary_location = "/usr/bin/chromium"  # Ruta de Chromium instalada en el contenedor
options.add_argument("--headless=new")  
options.add_argument("--no-sandbox")
options.add_argument("--disable-dev-shm-usage")

# Usar el ChromeDriver del sistema
service = Service("/usr/bin/chromedriver")
driver = webdriver.Chrome(service=service, options=options)

# Ir al sitio del SECOP
driver.get("https://community.secop.gov.co/Public/Tendering/ContractNoticeManagement/Index?currentLanguage=es-CO&Page=login&Country=CO&SkinName=CCE")

# Esperar que carguen los campos
wait = WebDriverWait(driver, 30)

# Fechas a ingresar
fecha_desde = "03/07/2025 00:00"
fecha_hasta = "04/07/2025 1:05 AM"
fecha_limite_presentacion = "09/07/2025 12:00 PM"

# Llenar campo: Fecha de publicación desde
campo_desde = wait.until(EC.presence_of_element_located((By.ID, "dtmbPublishDateFrom_txt")))
campo_desde.clear()
campo_desde.send_keys(fecha_desde)

# Llenar campo: Fecha de publicación hasta
campo_hasta = driver.find_element(By.ID, "dtmbPublishDateTo_txt")
campo_hasta.clear()
campo_hasta.send_keys(fecha_hasta)

# Llenar campo: Fecha de presentación de ofertas hasta
campo_fecha_ofertas = driver.find_element(By.ID, "dtmbTendersDeadlineTo_txt")
campo_fecha_ofertas.clear()
campo_fecha_ofertas.send_keys(fecha_limite_presentacion)

time.sleep(2)

# Clic en el botón de buscar
boton_buscar = driver.find_element(By.ID, "btnSearchButton")
boton_buscar.click()

# Esperar a que aparezcan enlaces "Detalle"
wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, "a[title='Detalle']")))

# Intentar cambiar paginación
try:
    btn_cambiar_paginacion = wait.until(
        EC.element_to_be_clickable((By.ID, "tblMainTable_trRowMiddle_tdCell1_tblForm_trGridRow_tdCell1_grdResultList_lnkLinkChangePagingStyle"))
    )
    driver.execute_script("arguments[0].scrollIntoView(true);", btn_cambiar_paginacion)
    time.sleep(1)
    btn_cambiar_paginacion.click()
    time.sleep(3)
except Exception as e:
    print("No se pudo cambiar la paginación:", e)

urls = []
while True:
    wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, "a[title='Detalle']")))
    soup = BeautifulSoup(driver.page_source, "html.parser")
    links_detalle = soup.find_all("a", title="Detalle")

    for link in links_detalle:
        onclick = link.get("onclick", "")
        match = re.search(r"noticeUID='\s*\+\s*'([^']+)'", onclick)
        if match:
            uid = match.group(1)
            url = f"https://community.secop.gov.co/Public/Tendering/OpportunityDetail/Index?noticeUID={uid}&isFromPublicArea=True&isModal=true&asPopupView=true"
            if url not in urls:
                urls.append(url)

    try:
        next_page = driver.find_element(By.XPATH, "//a[@class='VortalNumberedPaginatorButton' and contains(text(), '»')]")
        next_page.click()
        time.sleep(3)
    except:
        try:
            dots_button = driver.find_element(By.XPATH, "//input[@type='button' and @value='...']")
            dots_button.click()
            time.sleep(2)
        except:
            break

print("\n🔗 Enlaces de convocatorias encontradas:\n")
for i, url in enumerate(urls, 1):
    print(f"{i}. {url}")

print("\nVisitando convocatorias encontradas:\n")
convocatorias_visitadas = []
for i, url in enumerate(urls, 1):
    if i <= 5:
        driver.get(url)
        time.sleep(3)
        captcha_frames = driver.find_elements(By.CSS_SELECTOR, "iframe[src*='recaptcha']")
        if captcha_frames:
            print("CAPTCHA detectado, esperando resolución manual...")
            driver.switch_to.default_content()
            while driver.find_elements(By.CSS_SELECTOR, "iframe[src*='recaptcha']"):
                time.sleep(2)
        soup = BeautifulSoup(driver.page_source, "html.parser")
        texto_plano = soup.get_text(separator="\n", strip=True)
        lineas_limpias = [l for l in texto_plano.splitlines() if l.strip() and len(l.strip()) > 3]
        convocatorias_visitadas.append("\n".join(lineas_limpias)[:4000])

driver.quit()

# =====================
# PROCESAR CON GEMINI
# =====================
import google.generativeai as genai
from pymongo import MongoClient
import json

genai.configure(api_key="TU_API_KEY_AQUI")
model = genai.GenerativeModel("gemini-2.5-pro")

PROMPT_BASE = """
A partir del siguiente texto...
<<<
{texto}
>>>
"""

MONGO_URL = "mongodb+srv://USUARIO:PASS@cluster0.eiftxze.mongodb.net/?retryWrites=true&w=majority"
client = MongoClient(MONGO_URL)
db = client["inteiaDB"]
coleccion = db["oportunidad"]

for i, texto in enumerate(convocatorias_visitadas, 1):
    prompt = PROMPT_BASE.format(texto=texto)
    url_actual = urls[i - 1]
    try:
        response = model.generate_content(prompt)
        respuesta_texto = response.text
        json_data = json.loads(re.search(r"```json\n(.*?)```", respuesta_texto, re.DOTALL).group(1))
        if json_data.get("esPostulable"):
            json_data["urlConvocatoria"] = url_actual
            coleccion.update_one({"idProceso": json_data.get("idProceso")}, {"$set": json_data}, upsert=True)
    except Exception as e:
        print(f"Error procesando convocatoria {i}: {e}")
