# Trabajo Práctico Integrador (TPI) - .NET

## Integrantes
* **Simón Yocco** - Legajo: 55263 - Email: yoccosimon@gmail.com
* **Salvador Tosar** - Legajo: 53914 - Email: salvadortosar@gmail.com

---

## Requisitos Previos
* **.NET 8 SDK** instalado.
* **SQL Server LocalDB** (incluido por defecto con Visual Studio).

---

## Credenciales de Acceso para Pruebas (Roles)
El sistema incluye datos de prueba precargados representativos para evaluar todos los roles y permisos:

| Rol | Usuario | Contraseña | Descripción / Alcance |
| :--- | :--- | :--- | :--- |
| **Administrador** | `admin` | `admin123` | Acceso total a todos los ABMs y a ambos Reportes |
| **Docente** | `profesor` | `profe123` | Carga de calificaciones de sus cursos y acceso a ambos Reportes |
| **Alumno** | `alumno` | `alumno123` | Inscripción a cursos y consulta de sus estados académicos |

*(Otros alumnos de prueba precargados: `maria` / `maria123`, `lucas` / `lucas123`)*

---

## Instrucciones de Ejecución

### 1. Iniciar la Web API (Backend)
La API debe estar en ejecución antes de utilizar cualquiera de las aplicaciones clientes.

**Desde la terminal:**
```bash
dotnet run --project WebAPI
```
* Swagger UI estará disponible en: `https://localhost:7001/swagger` (o el puerto configurado en `launchSettings.json`).
* La base de datos `AcademiaTPI` se creará automáticamente en `(localdb)\MSSQLLocalDB` con todos los datos de prueba y roles representativos.

---

### 2. Iniciar la Aplicación de Escritorio (Windows Forms)

**Desde la terminal:**
```bash
dotnet run --project WindowsForms
```
* Se abrirá la ventana de inicio de sesión centrada en pantalla.
* Ingresar con cualquiera de las credenciales indicadas arriba.
* En el menú principal se adaptarán las opciones según el rol del usuario logueado.
* Dispone de los 7 ABMs y de los **2 Reportes Gráficos**:
  1. **Reporte Cursos y Ocupación** (Gráfico comparativo de Cupo vs. Inscriptos con GDI+).
  2. **Reporte de Rendimiento Académico** (Gráfico de Torta / Pie Chart con % de Aprobados, Regulares y Libres).

---

### 3. Iniciar la Aplicación Web (Blazor WebAssembly)

**Desde la terminal:**
```bash
dotnet run --project Blazor.WebAssembly
```
* Acceder desde el navegador a la URL que indique la consola (habitualmente `http://localhost:5194`).
* Menú de navegación dinámico por rol con gestión de Cursos, Especialidades, Comisiones, Usuarios, Inscripciones y **Planes de Estudio (Maestro/Detalle)**.
* Los **2 Reportes Interactivos** con gráficos SVG y barras acumuladas:
  1. `/reporte-cursos`: Gráfico comparativo interactivo SVG de Cupo vs Inscriptos.
  2. `/reporte-rendimiento`: Gráfico porcentual de Aprobados, Regulares y Libres por curso.

---

## Arquitectura y Características
* **Backend:** ASP.NET Core Minimal APIs con Entity Framework Core, autenticación mediante tokens JWT y validaciones de dominio.
* **Persistencia Dual:** Entity Framework Core + ADO.NET (`SqlConnection`/`SqlCommand`) en búsqueda con filtros de Especialidades.
* **Patrón Maestro/Detalle:** Implementado en Plan de Estudio (Cabecera) con sus Materias (Líneas de detalle), administradas en memoria y persistidas de forma atómica.
* **Clientes:** Windows Forms y Blazor WebAssembly consumiendo la misma Web API REST.
