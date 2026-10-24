# Trabajo Práctico Integrador (TPI) - .NET

## Integrantes
* **Simón Yocco** - Legajo: 55263 - Email: yoccosimon@gmail.com
* **Salvador Tosar** - Legajo: 53914 - Email: salvadortosar@gmail.com

---

## Requisitos Previos
* **.NET 8 SDK** instalado.
* **SQL Server LocalDB** (incluido por defecto con Visual Studio).

---

## Credenciales de Acceso
* **Usuario:** `admin`
* **Contraseña:** `admin123`

---

## Instrucciones de Ejecución

### 1. Iniciar la Web API (Backend)
La API debe estar en ejecución antes de utilizar cualquiera de las aplicaciones clientes.

**Desde la terminal:**
```bash
dotnet run --project WebAPI
```
* Swagger UI estará disponible en: `https://localhost:7001/swagger` (o el puerto configurado en `launchSettings.json`).
* La base de datos `AcademiaTPI` se creará automáticamente en `(localdb)\MSSQLLocalDB` con el usuario administrador inicial.

---

### 2. Iniciar la Aplicación de Escritorio (Windows Forms)

**Desde la terminal:**
```bash
dotnet run --project WindowsForms
```
* Se abrirá la ventana de inicio de sesión centrada en pantalla.
* Ingresar con las credenciales indicadas arriba.
* En el menú principal se encuentra disponible la gestión de **Cursos**, **Especialidades** y el **CRUD Maestro/Detalle de Planes** (con sus materias asociadas).

---

### 3. Iniciar la Aplicación Web (Blazor WebAssembly)

**Desde la terminal:**
```bash
dotnet run --project Blazor.WebAssembly
```
* Acceder desde el navegador a la URL que indique la consola (habitualmente `http://localhost:5194`).
* En el menú lateral se encuentra disponible la gestión completa de Cursos, Especialidades y **Planes de Estudio (Maestro/Detalle)**.

---

## Arquitectura y Características
* **Backend:** ASP.NET Core Minimal APIs con Entity Framework Core, autenticación mediante tokens JWT y validaciones de dominio.
* **Patrón Maestro/Detalle:** Implementado en Plan de Estudio (Cabecera) con sus Materias (Líneas de detalle), administradas en memoria y persistidas de forma atómica.
* **Clientes:** Windows Forms y Blazor WebAssembly consumiendo la misma Web API REST.
