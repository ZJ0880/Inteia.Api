# 🌐 EcosistemaAPI - Backend ASP.NET Core + MongoDB

**EcosistemaAPI de Inteia** es una API RESTful desarrollada con **ASP.NET Core** y **MongoDB** como base de datos NoSQL, orientada a la gestión de fuentes de información del ecosistema: organizaciones, recursos, investigaciones, y más.

## 🎯 Objetivos

- Visualizar fuentes de información del ecosistema
- Consultar y filtrar por categorías
- Subir nuevas fuentes desde formularios
- Gestionar dinámicamente las fuentes almacenadas

---

## 🏗️ Estructura del Proyecto

```bash
EcosistemaAPI/
├── Configurations/       # Configuración de MongoDB y otros servicios
├── Controllers/          # Endpoints de la API
├── DTOs/                 # Data Transfer Objects
├── Interfaces/           # Interfaces para los servicios
├── Models/               # Esquemas (entidades) que representan la BD
├── Services/             # Implementaciones de lógica de negocio
├── Program.cs            # Archivo principal de entrada
├── appsettings.json      # Configuración general (incluye conexión MongoDB)
├── .gitignore
