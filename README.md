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
El sistema incluye datos de prueba precargados representativos en `TPIContext.cs` para evaluar el sistema en funcionamiento real:

| Rol | Usuario | Contraseña | Persona Asociada | Alcance |
| :--- | :--- | :--- | :--- | :--- |
| **Administrador** | `admin` | `admin123` | Administrador General | Acceso total a los 7 ABMs y a ambos Reportes |
| **Docente** | `profesor` | `profe123` | Ing. Carlos Gómez | Cursos de .NET y Sistemas Operativos, carga notas y ve Reportes |
| **Docente** | `laura` | `laura123` | Dra. Laura Martínez | Cursos de Bases de Datos e Ing. de Software, carga notas y ve Reportes |
| **Docente** | `roberto` | `roberto123` | Lic. Roberto Sánchez | Cursos de Algoritmos, carga notas y ve Reportes |
| **Alumno** | `alumno` | `alumno123` | Juan Pérez | Inscripción a cursados y visualización de sus materias y notas |
| **Alumno** | `maria` | `maria123` | María López | Inscripción a cursados y visualización de sus materias y notas |
| **Alumno** | `lucas` | `lucas123` | Lucas Fernández | Inscripción a cursados y visualización de sus materias y notas |
| **Alumno** | `sofia` | `sofia123` | Sofía Rossi | Inscripción a cursados y visualización de sus materias y notas |
| **Alumno** | `mateo` | `mateo123` | Mateo Benítez | Inscripción a cursados y visualización de sus materias y notas |
| **Alumno** | `camila` | `camila123` | Camila Rodríguez | Inscripción a cursados y visualización de sus materias y notas |
| **Alumno** | `tomas` | `tomas123` | Tomás Díaz | Inscripción a cursados y visualización de sus materias y notas |
| **Alumno** | `valentina` | `valentina123` | Valentina Romero | Inscripción a cursados y visualización de sus materias y notas |

---

## Instrucciones de Ejecución

### 1. Iniciar la Web API (Backend)
La API debe estar en ejecución antes de utilizar cualquiera de las aplicaciones clientes.

**Desde la terminal:**
```bash
dotnet run --project WebAPI
```
* Swagger UI estará disponible en: `https://localhost:7001/swagger`.
* La base de datos `AcademiaTPI` se creará automáticamente en `(localdb)\MSSQLLocalDB` con todos los datos precargados listos.

---

### 2. Iniciar la Aplicación de Escritorio (Windows Forms)

**Desde la terminal:**
```bash
dotnet run --project WindowsForms
```
* Se abrirá la ventana de login. Al ingresar con cualquier usuario (Docente, Alumno o Administrador), la interfaz se adaptará según su rol y mostrará su nombre en la barra de título.
* Cuenta con los 7 ABMs y los **2 Reportes**:
  1. **Reporte Cursos y Ocupación** (Gráfico comparativo de Cupo vs. Inscriptos con GDI+).
  2. **Reporte de Rendimiento Académico** (Tabular, porcentaje de Aprobados, Regulares y Libres por Materia).

---

### 3. Iniciar la Aplicación Web (Blazor WebAssembly)

**Desde la terminal:**
```bash
dotnet run --project Blazor.WebAssembly
```
* Acceder desde el navegador a la URL que indique la consola (habitualmente `http://localhost:5194`).
* Menú de navegación dinámico por rol y visualización del usuario conectado en el header superior.
* Los **2 Reportes**:
  1. `/reporte-cursos`: Gráfico comparativo interactivo SVG de Cupo vs Inscriptos.
  2. `/reporte-rendimiento`: Tabla comparativa con buscador rápido y totales generales.

---

## Arquitectura y Características
* **Backend:** ASP.NET Core Minimal APIs con Entity Framework Core, autenticación mediante tokens JWT y validaciones de dominio.
* **Persistencia Dual:** Entity Framework Core + ADO.NET (`SqlConnection`/`SqlCommand`) en búsqueda con filtros de Especialidades.
* **Patrón Maestro/Detalle:** Implementado en Plan de Estudio (Cabecera) con sus Materias (Líneas de detalle), administradas en memoria y persistidas de forma atómica.
* **Clientes:** Windows Forms y Blazor WebAssembly consumiendo la misma Web API REST.
