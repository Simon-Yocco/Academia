using DTOs;
using Entidades;
using Data;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IPersonaRepository personaRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IPersonaRepository personaRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.personaRepository = personaRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await usuarioRepository.GetAllAsync();
            return usuarios.Select(MapearADTO).ToList();
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var usuario = await usuarioRepository.GetAsync(id);
            if (usuario == null) return null;

            return MapearADTO(usuario);
        }

        public async Task<UsuarioDTO?> GetByUsernameAsync(string username)
        {
            var usuario = await usuarioRepository.GetByUsernameAsync(username);
            if (usuario == null) return null;

            return MapearADTO(usuario);
        }

        public async Task<UsuarioDTO?> AddAsync(UsuarioDTO dto)
        {
            // Mapeamos y guardamos la Persona
            var tipo = Enum.TryParse<Persona.TiposPersonas>(dto.Persona.TipoPersona, true, out var parsedTipo)
                ? parsedTipo
                : Persona.TiposPersonas.Alumno;

            // Asignación de legajo automática e incremental
            int proximoLegajo = await personaRepository.GetProximoLegajoAsync();
            dto.Persona.Legajo = proximoLegajo;

            var persona = new Persona(
                0,
                dto.Persona.Nombre,
                dto.Persona.Apellido,
                dto.Persona.Direccion,
                dto.Persona.Email,
                dto.Persona.Telefono,
                dto.Persona.FechaNacimiento,
                dto.Persona.Legajo,
                tipo,
                dto.Persona.IDPlan
            );
            await personaRepository.AddAsync(persona);

            // Nombre de usuario = nombre personal en minúsculas (evitando colisión si ya existe)
            var baseUsername = dto.Persona.Nombre.Trim().ToLower();
            var finalUsername = baseUsername;
            int suffix = 1;
            while (await usuarioRepository.GetByUsernameAsync(finalUsername) != null)
            {
                finalUsername = $"{baseUsername}{suffix++}";
            }
            dto.NombreUsuario = finalUsername;

            // Contraseña = legajo asignado
            dto.Clave = proximoLegajo.ToString();

            // Mapeamos de DTO a Entidad Usuario
            var usuario = new Usuario(0, dto.Clave, dto.Habilitado, dto.NombreUsuario, persona.ID);
            await usuarioRepository.AddAsync(usuario);

            // Actualizamos el ID del DTO con el que se generó en la base de datos
            dto.ID = usuario.ID;
            dto.IDPersona = persona.ID;
            dto.Persona.ID = persona.ID;

            return dto;
        }

        public async Task<bool> UpdateAsync(UsuarioDTO dto)
        {
            var existing = await usuarioRepository.GetAsync(dto.ID);
            if (existing == null)
                return false;

            // Actualizamos la entidad Usuario
            existing.SetUsuario(dto.NombreUsuario);
            existing.SetHabilitado(dto.Habilitado);
            if (!string.IsNullOrWhiteSpace(dto.Clave))
            {
                existing.SetClave(dto.Clave);
            }

            // Actualizamos la Persona asociada
            if (existing.Persona != null && dto.Persona != null)
            {
                if (Enum.TryParse<Persona.TiposPersonas>(dto.Persona.TipoPersona, true, out var tipo))
                {
                    existing.Persona.SetTipoPersona(tipo);
                }
                existing.Persona.SetNombre(dto.Persona.Nombre);
                existing.Persona.SetApellido(dto.Persona.Apellido);
                existing.Persona.SetDireccion(dto.Persona.Direccion);
                existing.Persona.SetEmail(dto.Persona.Email);
                existing.Persona.SetTelefono(dto.Persona.Telefono);
                existing.Persona.SetFechaNacimiento(dto.Persona.FechaNacimiento);
                existing.Persona.SetLegajo(dto.Persona.Legajo);
                existing.Persona.SetIDPlan(dto.Persona.IDPlan);

                await personaRepository.UpdateAsync(existing.Persona);
            }

            return await usuarioRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await usuarioRepository.GetAsync(id);
            if (usuario == null)
                return false;

            int idPersona = usuario.IDPersona;
            var deleted = await usuarioRepository.DeleteAsync(id);
            if (deleted && idPersona > 0)
            {
                await personaRepository.DeleteAsync(idPersona);
            }

            return deleted;
        }

        // Función para simplificar y reutilizar el mapeo de Entidad a DTO
        private static UsuarioDTO MapearADTO(Usuario u)
        {
            return new UsuarioDTO
            {
                ID = u.ID,
                NombreUsuario = u.NombreUsuario,
                Habilitado = u.Habilitado,
                IDPersona = u.IDPersona,
                Persona = new PersonaDTO
                {
                    ID = u.Persona?.ID ?? 0,
                    Nombre = u.Persona?.Nombre ?? string.Empty,
                    Apellido = u.Persona?.Apellido ?? string.Empty,
                    Direccion = u.Persona?.Direccion ?? string.Empty,
                    Email = u.Persona?.Email ?? string.Empty,
                    Telefono = u.Persona?.Telefono ?? string.Empty,
                    FechaNacimiento = u.Persona?.FechaNacimiento ?? DateTime.Today,
                    Legajo = u.Persona?.Legajo ?? 0,
                    TipoPersona = u.Persona?.TipoPersona.ToString() ?? "Alumno",
                    IDPlan = u.Persona?.IDPlan,
                    PlanDescripcion = u.Persona?.Plan?.Descripcion
                }
            };
        }
    }
}
