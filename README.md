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
El sistema utiliza la convención definida en la lógica de negocio (`UsuarioService.AddAsync`):
* **Nombre de Usuario:** Primer nombre en minúsculas.
* **Contraseña:** El número de legajo asignado automáticamente.

### 👑 Administrador
* **Usuario:** `admin`
* **Contraseña:** `admin123`
* **Alcance:** Acceso completo a los 7 ABMs y a ambos Reportes.

---

### 👨‍🏫 Profesores (Docentes)
*Acceso a calificaciones de sus cursos y a los Reportes.*

| Profesor | Usuario (Nombre) | Contraseña (Legajo) | Email | Cursos asignados |
| :--- | :--- | :--- | :--- | :--- |
| **Ing. Carlos Gómez** | `carlos` | **`2001`** | `carlos.gomez@academia.utn.edu.ar` | Desarrollo .NET (Tarde y Noche), Sistemas Operativos |
| **Dra. Laura Martínez** | `laura` | **`2002`** | `laura.martinez@academia.utn.edu.ar` | Bases de Datos, Ingeniería de Software |
| **Lic. Roberto Sánchez** | `roberto` | **`2003`** | `roberto.sanchez@academia.utn.edu.ar` | Algoritmos y Estructuras de Datos |

---

### 🎓 Alumnos
*Acceso a inscripción a cursados y visualización de sus notas y materias.*

| Alumno | Usuario (Nombre) | Contraseña (Legajo) | Email |
| :--- | :--- | :--- | :--- |
| **Juan Pérez** | `juan` | **`5001`** | `juan.perez@academia.utn.edu.ar` |
| **María López** | `maria` | **`5002`** | `maria.lopez@academia.utn.edu.ar` |
| **Lucas Fernández** | `lucas` | **`5003`** | `lucas.fernandez@academia.utn.edu.ar` |
| **Sofía Rossi** | `sofia` | **`5004`** | `sofia.rossi@academia.utn.edu.ar` |
| **Mateo Benítez** | `mateo` | **`5005`** | `mateo.benitez@academia.utn.edu.ar` |
| **Camila Rodríguez** | `camila` | **`5006`** | `camila.rodriguez@academia.utn.edu.ar` |
| **Tomás Díaz** | `tomas` | **`5007`** | `tomas.diaz@academia.utn.edu.ar` |
| **Valentina Romero** | `valentina` | **`5008`** | `valentina.romero@academia.utn.edu.ar` |

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
* Se abrirá la ventana de login.
* Ingresar con el usuario (ej: `carlos` o `juan`) y su legajo como contraseña (ej: `2001` o `5001`).
* La interfaz se adaptará según su rol y mostrará su nombre en la barra de título.
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

## Cómo dar de alta nuevos Profesores o Alumnos
Desde el rol **Administrador**, ingresar al módulo **Usuarios**:
1. Hacer clic en **Agregar** / **Nuevo Usuario**.
2. Completar los datos de la Persona (Nombre, Apellido, Email, Dirección, Teléfono, Fecha de Nacimiento).
3. Seleccionar el tipo de persona: **Docente** o **Alumno**.
4. Al guardar, el sistema le asigna el próximo número de legajo automáticamente, su usuario será su nombre en minúsculas y su contraseña será su número de legajo.
