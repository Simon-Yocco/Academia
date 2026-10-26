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
El sistema permite iniciar sesión ingresando el **Nombre de Usuario**, el **Número de Legajo** o el **Email**:

### 👑 Administrador
* **Usuario / Legajo:** `admin` (Legajo: `1000`)
* **Contraseña:** `admin123`
* **Alcance:** Acceso completo a los 7 ABMs y a ambos Reportes.

---

### 👨‍🏫 Profesores (Docentes)
* **Contraseña común:** `123456`
* **Alcance:** Gestión de calificaciones de sus cursos asignados y acceso a ambos Reportes.

| Profesor | Usuario | Legajo | Email | Materias a cargo |
| :--- | :--- | :--- | :--- | :--- |
| **Ing. Carlos Gómez** | `carlos.gomez` | `2001` | `carlos.gomez@academia.utn.edu.ar` | Desarrollo .NET (Tarde/Noche), Sistemas Operativos |
| **Dra. Laura Martínez** | `laura.martinez` | `2002` | `laura.martinez@academia.utn.edu.ar` | Bases de Datos, Ingeniería de Software |
| **Lic. Roberto Sánchez** | `roberto.sanchez` | `2003` | `roberto.sanchez@academia.utn.edu.ar` | Algoritmos y Estructuras de Datos |

---

### 🎓 Alumnos
* **Contraseña común:** `123456`
* **Alcance:** Inscripción a cursos y consulta de notas/estados académicos.

| Alumno | Usuario | Legajo | Email |
| :--- | :--- | :--- | :--- |
| **Juan Pérez** | `juan.perez` | `5001` | `juan.perez@academia.utn.edu.ar` |
| **María López** | `maria.lopez` | `5002` | `maria.lopez@academia.utn.edu.ar` |
| **Lucas Fernández** | `lucas.fernandez` | `5003` | `lucas.fernandez@academia.utn.edu.ar` |
| **Sofía Rossi** | `sofia.rossi` | `5004` | `sofia.rossi@academia.utn.edu.ar` |
| **Mateo Benítez** | `mateo.benitez` | `5005` | `mateo.benitez@academia.utn.edu.ar` |
| **Camila Rodríguez** | `camila.rodriguez` | `5006` | `camila.rodriguez@academia.utn.edu.ar` |
| **Tomás Díaz** | `tomas.diaz` | `5007` | `tomas.diaz@academia.utn.edu.ar` |
| **Valentina Romero** | `valentina.romero` | `5008` | `valentina.romero@academia.utn.edu.ar` |

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
* Se abrirá la ventana de login. Puedes ingresar con el **Usuario** (`carlos.gomez`), con el **Legajo** (`2001`) o con el **Email**.
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
4. Establecer su **Nombre de Usuario** y **Contraseña**.
5. Al guardar, el nuevo usuario podrá iniciar sesión inmediatamente en el sistema con sus permisos correspondientes.
