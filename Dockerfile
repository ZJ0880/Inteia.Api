# ==========================
# Etapa de compilación
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet publish -c Release -o /app/out

# ==========================
# Etapa de runtime con .NET + Python + Chromium
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Instalar Python, venv y Chromium (sin chromedriver)
RUN apt-get update && apt-get install -y \
    python3 \
    python3-pip \
    python3-venv \
    chromium \
    chromium-driver \
    && rm -rf /var/lib/apt/lists/*


# Crear entorno virtual para Python
RUN python3 -m venv /opt/venv
ENV PATH="/opt/venv/bin:$PATH"

# Copiar binarios publicados de .NET
COPY --from=build /app/out .

# Copiar scripts de Python y requirements
COPY Scripts/ ./Scripts/
COPY requirements.txt ./requirements.txt

# Instalar dependencias de Python en el entorno virtual
RUN pip install --no-cache-dir -r requirements.txt

EXPOSE 8080

CMD ["dotnet", "EcosistemaAPI.dll"]
