using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using Entidades;
using Data;

namespace Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();

            // Mapeo de Usuario (Entidad) a UsuarioCursoDTO
            return usuarios.Select(u => new UsuarioDTO
            {
                ID = u.ID,
                NombreUsuario = u.NombreUsuario,
                Clave = u.Clave,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Habilitado = u.Habilitado
            }).ToList();
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var usuario = await _repository.GetAsync(id);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                ID = usuario.ID,
                NombreUsuario = usuario.NombreUsuario,
                Clave = usuario.Clave,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Habilitado = usuario.Habilitado
            };
        }

        public async Task<UsuarioDTO?> GetByUsernameAsync(string username)
        {
            var usuario = await _repository.GetByUsernameAsync(username);
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                ID = usuario.ID,
                NombreUsuario = usuario.NombreUsuario,
                Clave = usuario.Clave,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Habilitado = usuario.Habilitado
            };
        }

        public async Task<UsuarioDTO?> AddAsync(UsuarioDTO dto)
        {
            // Mapeamos de DTO a Entidad.
            var usuario = new Usuario(0, dto.Apellido, dto.Clave, dto.Habilitado, dto.Nombre, dto.NombreUsuario);
            await _repository.AddAsync(usuario);

            // Actualizamos el ID del DTO con el que se generó en la base de datos
            dto.ID = usuario.ID;

            return dto;
        }

        public async Task<bool> UpdateAsync(UsuarioDTO dto)
        {
            var existing = await _repository.GetAsync(dto.ID);

            if (existing == null)
                return false;

            // Aca le pasas dto.ID al principio
            var usuario = new Usuario(dto.ID, dto.Apellido, dto.Clave, dto.Habilitado, dto.Nombre, dto.NombreUsuario);

            return await _repository.UpdateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<UsuarioDTO?> LoginAsync(string username, string password)
        {
            var user = await _repository.GetByUsernameAsync(username);
            
            // Validamos que exista y que la clave coincida
            if (user != null && user.Clave == password)
            {
                return new UsuarioDTO
                {
                    ID = user.ID,
                    NombreUsuario = user.NombreUsuario,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Habilitado = user.Habilitado,
                    Clave = user.Clave
                };
            }
            return null;
        }
    }
}
