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
        public string Apellido { get; private set; }
        public string Clave { get; private set; }
        public bool Habilitado { get; private set; }
        public string Nombre { get; private set; }
        public string NombreUsuario { get; private set; }

        public Usuario() { }

        public Usuario(int id, string apellido, string clave, bool habilitado, string nombre, string usuario)
        {
            SetId(id);
            SetApellido(apellido);
            SetClave(clave);
            SetHabilitado(habilitado);
            SetNombre(nombre);
            SetUsuario(usuario);
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío.");
            Apellido = apellido;
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
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");
            Nombre = nombre;
        }
        public void SetUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.");
            NombreUsuario = usuario;
        }
    }
}
