using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public async Task AddAsync(Usuario usuario)
        {
            using var context = CreateContext();
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Usuario?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            using var context = CreateContext();
            return await context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == username && u.Habilitado);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Usuarios.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            using var context = CreateContext();
            context.Usuarios.Update(usuario);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
