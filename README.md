# **Versión**

## **Versión GIT**
- **Estado:** Actualizada y funcional.
- **Observación:** Contiene las últimas correcciones implementadas.

## **Versión Carpeta Adjunta**
- **Estado:** Desactualizada.
- **Problemas identificados:**
  1. El **campo "Género"** en la tabla *Cliente* está definido como **string**, mientras que el código trabaja con **ENUM**.
  2. El **campo "Caontrasena"** en la tabla *Cliente* contiene un error tipográfico (**"ñ"**).

- **Observación:** Estos problemas ya fueron corregidos en la **Versión GIT**.

 ---

# Proyecto: API Gateway y Microservicios

Este proyecto implementa una arquitectura de **microservicios** utilizando un **API Gateway** para centralizar las solicitudes. 
Consta de dos microservicios:
1. **Client Management**: Gestiona información de clientes.
2. **Account Management**: Gestiona información de cuentas.

El objetivo es crear un sistema modular y escalable con contenedores Docker y orquestación a través de Docker Compose.

---

## **Estructura del Proyecto**

```plaintext
project-root/
├── src/
│   ├── Api.Gateway.WebClient/          # API Gateway
│   │   ├── Dockerfile
│   │   ├── appsettings.json
│   │   └── ...
│   ├── ClientMgmt.WebApi/              # Microservicio: Client Management
│   │   ├── Dockerfile
│   │   ├── appsettings.json
│   │   └── ...
│   ├── AccountMgmt.WebApi/             # Microservicio: Account Management
│   │   ├── Dockerfile
│   │   ├── appsettings.json
│   │   └── ...
├── docker-compose.yml                  # Configuración de Docker Compose
├── docker-compose.override.yml         # Configuración adicional (desarrollo)
├── .env                                # Variables de entorno
├── README.md                           # Documentación del proyecto
```
---

## **Servicios**

### 1. **API Gateway**
- Responsable de centralizar las solicitudes a los microservicios.
- Expone un único punto de entrada para la comunicación externa.
- **Puerto:** `3300` (HTTP) - Docker
- **Puerto:** `3000` (HTTP) - Local

### 2. **Client Management**
- Microservicio para gestionar información de clientes.
- **Puerto:** `1100` (HTTP) - Docker
- **Puerto:** `1000` (HTTP) - Local
-
### 3. **Account Management**
- Microservicio para gestionar información de cuentas.
- **Puerto:** `2200` (HTTP) - Docker
- **Puerto:** `2000` (HTTP) - Local

---

## **Requisitos Previos**
- **Docker**: Asegúrate de tener Docker y Docker Compose instalados.
- **.NET SDK**: (opcional) Para realizar modificaciones o ejecutar localmente sin Docker.
- **Certificados HTTPS**: (opcional) Generar certificados si se requiere HTTPS.
- **SQL Server**: Gestor de la base de datos.
---


## **Instrucciones de Ejecución**

### **1. Clonar el Proyecto**
```bash
git clone https://github.com/DiloGunz/BankService.git
```

### **2. Crear Base de Datos**

- Ejecutar el script de base de Datos adjunto en el proyecto: BaseDatos.sql
- El script es de SQl Server

### **3. Configurar cadena de conexion**

- **En Entorno Local** Edita el archivo appsettings.Development.json en los proyectos:
    > ClientMgmt.WebApi
    > AccountMgmt.WebApi
- **En Docker** Configura el parámetro **ConnectionStrings__SqlServer** en el archivo **docker-compose.override.yml** para los proyectos:
    > ClientMgmt.WebApi
    > AccountMgmt.WebApi


### **4. Configurar microservicios en Api Gateway**

- **En entorno local** Edita el archivo **appsettings.Development.json** en el proyecto Api.Gateway.WebClient:
```json
"ApiUrls": {
    "ClientMgmt": "http://localhost:1000",
    "AccountMgmt": "http://localhost:2000"
  }
```

- **En Docker** Edita el archivo appsettings.json en el proyecto **Api.Gateway.WebClient**. Los nombres deben coincidir con los definidos en el archivo **docker-compose.override.yml**:
```json
"ApiUrls": {
    "ClientMgmt": "http://client_mgmt:8080",
    "AccountMgmt": "http://account_mgmt:8080"
  },
```

### **5. Probar Usando Postman**

- Se adjunta dos archivos json de **Entornos** para postam
    - **BankServiceEnv_Dev.postman_environment**: Sirve para hacer pruebas en entorno local.
    - **BankServiceEnv_Docker.postman_environment**: Sirve para hacer pruebas usando docker.

- También se adjunta una colección de Postman llamada **BankService.postman_collection.json**, que contiene ejemplos para probar los endpoints de cada microservicio y el API Gateway.

### **6. Ejecutar docker-compose**

Navega a la raíz del proyecto en el terminal (o PowerShell en Windows) y ejecuta el siguiente comando:

- **docker-compose up --build**
- 
Este comando construirá las imágenes de Docker y ejecutará los contenedores.
- 
Una vez que los contenedores estén en ejecución, puedes usar los archivos de Postman mencionados en el paso anterior para probar los endpoints.


## **Notas Adicionales**

- Asegúrate de que los puertos especificados en docker-compose.override.yml no estén en uso por otros servicios.
- Verifica que las dependencias como SQL Server estén correctamente configuradas antes de ejecutar los microservicios.