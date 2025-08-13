from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.chrome.options import Options
from webdriver_manager.chrome import ChromeDriverManager
import time
from bs4 import BeautifulSoup
import re

# Configurar el navegador
options = Options()
options.add_argument("--headless") 
options.add_argument("--no-sandbox")
options.add_argument("--disable-dev-shm-usage")
options.binary_location = "/usr/bin/chromium" 
options.add_argument("--start-maximized")

# Iniciar el navegador
driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()), options=options)

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

# Esperar 1 segundo para asegurar que el llenado fue exitoso
time.sleep(2)

# Clic en el botón de buscar
boton_buscar = driver.find_element(By.ID, "btnSearchButton")
boton_buscar.click()

# Esperar explícitamente a que al menos un enlace "Detalle" aparezca
wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, "a[title='Detalle']")))

# Scroll al botón y hacer clic
try:
    btn_cambiar_paginacion = wait.until(
        EC.element_to_be_clickable((By.ID, "tblMainTable_trRowMiddle_tdCell1_tblForm_trGridRow_tdCell1_grdResultList_lnkLinkChangePagingStyle"))
    )
    # Hacer scroll hasta el botón
    driver.execute_script("arguments[0].scrollIntoView(true);", btn_cambiar_paginacion)
    time.sleep(1)
    # Mover el mouse al botón para evitar superposición
    btn_cambiar_paginacion.click()
    time.sleep(3)
except Exception as e:
    print(" No se pudo hacer clic en 'Cambiar el estilo de paginación':", e)


urls = []

while True:
    # Esperar que aparezcan enlaces "Detalle"
    wait.until(EC.presence_of_element_located((By.CSS_SELECTOR, "a[title='Detalle']")))

    # Extraer HTML actual y parsear
    html = driver.page_source
    soup = BeautifulSoup(html, "html.parser")
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
        # Intentar ir a la siguiente página visible
        next_page = driver.find_element(By.XPATH, "//a[@class='VortalNumberedPaginatorButton' and contains(text(), '»')]")
        next_page.click()
        time.sleep(3)

    except:
        try:
            # Si no hay "»", intentamos hacer clic en los "..." para mostrar más páginas
            dots_button = driver.find_element(By.XPATH, "//input[@type='button' and @value='...']")
            dots_button.click()
            time.sleep(2)
        except:
            # No hay más páginas
            break




# Mostrar los enlaces extraídos
print("\n🔗 Enlaces de cada convocatoria encontrada:\n")



for i, url in enumerate(urls, 1):
    print(f"{i}. {url}")

print("\nVisitando convocatorias encontradas (con Selenium):\n")

convocatorias_visitadas = []

for i, url in enumerate(urls, 1):

    if i<=5:
        print(f" Visitando convocatoria {i}")
        driver.get(url)
        time.sleep(3)

        # Verificar si el CAPTCHA está presente
        captcha_frames = driver.find_elements(By.CSS_SELECTOR, "iframe[src*='recaptcha']")
        if captcha_frames:
            print("CAPTCHA detectado, intentando resolver...")
            try:
                driver.switch_to.frame(captcha_frames[0])

                checkbox = WebDriverWait(driver, 10).until(
                    EC.element_to_be_clickable((By.CLASS_NAME, "recaptcha-checkbox-border"))
                )
                checkbox.click()
                print("CAPTCHA clickeado")

                driver.switch_to.default_content()

                # Esperar indefinidamente a que el CAPTCHA desaparezca
                print("Esperando a que el humano resuelva el CAPTCHA...")
                while True:
                    time.sleep(2)
                    frames = driver.find_elements(By.CSS_SELECTOR, "iframe[src*='recaptcha']")
                    if not frames:
                        print("CAPTCHA resuelto por humano.")
                        break

            except Exception as e:
                print(" Error durante el intento de resolver CAPTCHA:", e)
                driver.switch_to.default_content()
        else:
            print("No hay CAPTCHA, haciendo scraping directo.")

        # Guardar el HTML
        soup = BeautifulSoup(driver.page_source, "html.parser")
        texto_plano = soup.get_text(separator="\n", strip=True)
        lineas_limpias = [linea for linea in texto_plano.splitlines() if linea.strip() and len(linea.strip()) > 3]
        texto_limpio = "\n".join(lineas_limpias)

        convocatorias_visitadas.append(texto_limpio[:4000])  # Guardar solo los primeros 1000 caracteres
        
        time.sleep(2)

driver.quit()

#Uso gemini para resumir convocatorias.

import google.generativeai as genai

# llenar api key google gemini
genai.configure(api_key="AIzaSyDOZLT6UNU6hZBszi4NDS_RW2mDTGMrs-0")

# Usa el modelo Gemini 2.0 Flash
model = genai.GenerativeModel("gemini-2.5-pro")

# Prompt base
PROMPT_BASE = """
A partir del siguiente texto desestructurado extraído del portal SECOP II, responde únicamente si el proceso es postulable o no. Si no lo es, indica el motivo bajo el campo "motivoNoPostulable" y no extraigas ni calcules ningún otro campo.

Si el proceso sí es postulable, extrae los siguientes campos en formato JSON:

- "esPostulable": (true o false)
- "motivoNoPostulable": (obligatorio solo si "esPostulable" es false)
- "idProceso": (ID único del proceso)
- "descripcion": (Resumen claro y conciso sobre lo que se debe ejecutar en el contrato, máximo 300 caracteres, evita frases genéricas)
- "fechaMaximaPostulacion": (fecha en formato ISO 8601: YYYY-MM-DD)
- "departamento": (Nombre del departamento en Colombia donde se ejecutará el proceso o se ubica la entidad contratante. El valor debe ir sin tildes y completamente en mayúscula)
- "aspectosImportantes": (Lista de aspectos clave como el tipo de entidad contratante, si requiere experiencia previa, restricciones, fechas clave, modalidad de contratación, etc.)

Texto del SECOP:
<<<
{texto}
>>>
"""

from pymongo import MongoClient
import json
import re

# Conexión a MongoDB
MONGO_URL = "mongodb+srv://karolopez1010:0XqKdPGNNVh6Zzed@cluster0.eiftxze.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"
client = MongoClient(MONGO_URL)
db = client["inteiaDB"]
coleccion = db["oportunidad"]

for i, texto in enumerate(convocatorias_visitadas, 1):
    prompt = PROMPT_BASE.format(texto=texto)
    url_actual = urls[i - 1]

    if i<=5:
        try:
            print(f"\n🔍 Enviando convocatoria {i} a Gemini...\n")
            response = model.generate_content(prompt)
            respuesta_texto = response.text
            print("Respuesta JSON de Gemini:\n")
            print(respuesta_texto)

            try:

                # Eliminar los backticks y extraer solo el contenido JSON
                respuesta_limpia = re.search(r"```json\n(.*?)```", respuesta_texto, re.DOTALL)
                if respuesta_limpia:
                    json_data = json.loads(respuesta_limpia.group(1))
                else:
                    raise ValueError("No se encontró un bloque JSON válido en la respuesta de Gemini.")

                if json_data.get("esPostulable") == True:

                    json_data["urlConvocatoria"] = url_actual
                    id_proceso = json_data.get("idProceso")

                    

                    if id_proceso:
                        resultado = coleccion.update_one(
                            {"idProceso": id_proceso},     # filtro
                            {"$set": json_data},           # datos nuevos o actualizados
                            upsert=True                    # crea si no existe
                        )

                        if resultado.matched_count > 0:
                            print(" Convocatoria actualizada en MongoDB.")
                        else:
                            print(" Convocatoria insertada en MongoDB.")
                    else:
                        print(" No se encontró un 'idProceso' válido para esta convocatoria.")


            except json.JSONDecodeError as e:
                    print(f" Error al interpretar respuesta de Gemini como JSON: {e}")

        except Exception as e:
            print(f" Error procesando convocatoria {i}: {e}")
