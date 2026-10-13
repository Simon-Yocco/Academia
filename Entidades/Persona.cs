using System;
using System.Text.RegularExpressions;

namespace Entidades
{
    public class Persona : BusinessEntity
    {
        public enum TiposPersonas
        {
            Administrador = 1,
            Docente = 2,
            Alumno = 3
        }

        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;
        public string Direccion { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Telefono { get; private set; } = string.Empty;
        public DateTime FechaNacimiento { get; private set; }
        public int Legajo { get; private set; }
        public TiposPersonas TipoPersona { get; private set; }
        public int? IDPlan { get; private set; }
        public Plan? Plan { get; set; }

        public Persona() { }

        public Persona(int id, string nombre, string apellido, string direccion, string email, string telefono, DateTime fechaNacimiento, int legajo, TiposPersonas tipoPersona, int? idPlan = null)
        {
            SetId(id);
            SetNombre(nombre);
            SetApellido(apellido);
            SetDireccion(direccion);
            SetEmail(email);
            SetTelefono(telefono);
            SetFechaNacimiento(fechaNacimiento);
            SetLegajo(legajo);
            SetTipoPersona(tipoPersona);
            SetIDPlan(idPlan);
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");
            Nombre = nombre.Trim();
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío.");
            Apellido = apellido.Trim();
        }

        public void SetDireccion(string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentException("La dirección no puede estar vacía.");
            Direccion = direccion.Trim();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email no puede estar vacío.");
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("El email no tiene un formato válido.");
            Email = email.Trim();
        }

        public void SetTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono no puede estar vacío.");
            Telefono = telefono.Trim();
        }

        public void SetFechaNacimiento(DateTime fechaNacimiento)
        {
            if (fechaNacimiento == default || fechaNacimiento > DateTime.Now)
                throw new ArgumentException("La fecha de nacimiento no es válida.");
            FechaNacimiento = fechaNacimiento;
        }

        public void SetLegajo(int legajo)
        {
            if (legajo <= 0)
                throw new ArgumentException("El legajo debe ser mayor a cero.");
            Legajo = legajo;
        }

        public void SetTipoPersona(TiposPersonas tipoPersona)
        {
            TipoPersona = tipoPersona;
        }

        public void SetIDPlan(int? idPlan)
        {
            if (idPlan.HasValue && idPlan.Value <= 0)
                throw new ArgumentException("El ID del plan no es válido.");
            IDPlan = idPlan;
        }
    }
}
