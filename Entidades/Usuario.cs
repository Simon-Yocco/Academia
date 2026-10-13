using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario : BusinessEntity
    {
        public string Clave { get; private set; } = string.Empty;
        public bool Habilitado { get; private set; }
        public string NombreUsuario { get; private set; } = string.Empty;
        public int IDPersona { get; private set; }
        public Persona? Persona { get; set; }

        public string Nombre => Persona?.Nombre ?? string.Empty;
        public string Apellido => Persona?.Apellido ?? string.Empty;

        public Usuario() { }

        public Usuario(int id, string clave, bool habilitado, string usuario, int idPersona)
        {
            SetId(id);
            SetClave(clave);
            SetHabilitado(habilitado);
            SetUsuario(usuario);
            SetIDPersona(idPersona);
        }

        public void SetClave(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("La clave no puede estar vacía.");
            Clave = clave;
        }

        public void SetHabilitado(bool habilitado)
        {
            Habilitado = habilitado;
        }

        public void SetUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.");
            NombreUsuario = usuario.Trim();
        }

        public void SetIDPersona(int idPersona)
        {
            if (idPersona <= 0)
                throw new ArgumentException("El ID de la persona debe ser mayor a cero.");
            IDPersona = idPersona;
        }
    }
}
