using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PlanRepository : IPlanRepository
    {
        private readonly TPIContext _context;

        public PlanRepository(TPIContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Plan plan)
        {
            _context.Planes.Add(plan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            // Include para traer también las materias y la especialidad (Maestro/Detalle completo)
            return await _context.Planes
                .Include(p => p.Especialidad)
                .Include(p => p.Materias)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetByCriteriaAsync(string texto)
        {
            return await _context.Planes
                .Include(p => p.Especialidad)
                .Include(p => p.Materias)
                .Where(p => p.Descripcion.Contains(texto) || (p.Especialidad != null && p.Especialidad.Descripcion.Contains(texto)))
                .ToListAsync();
        }

        public async Task<Plan?> GetAsync(int id)
        {
            return await _context.Planes
                .Include(p => p.Especialidad)
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<bool> UpdateAsync(Plan plan)
        {
            // 1. Buscamos el plan original en la BD con sus materias
            var existingPlan = await _context.Planes
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.ID == plan.ID);

            if (existingPlan == null) return false;

            //valido que las materias recibicdas no pertenezcan a otro plan
            var materiasDeOtroPlan = plan.Materias
                .Where(m => m.ID != 0 && !existingPlan.Materias.Any(em => em.ID == m.ID))
                .ToList();
            if (materiasDeOtroPlan.Any()) //si alguna materia pertenece a otro plan lanzamos una InvalidOperationException
            {
                throw new InvalidOperationException("Una o más materias enviadas no pertenecen a este plan de estudio.");
            }

            // 2. Actualizamos la cabecera
            existingPlan.SetDescripcion(plan.Descripcion);
            existingPlan.SetIDEspecialidad(plan.IDEspecialidad);

            // 3. Sincronizamos las líneas (Materias)
            
            // busco las materias que no estan en la lista recibida
            var materiasParaBorrar = existingPlan.Materias
                .Where(em => !plan.Materias.Any(m => m.ID == em.ID && m.ID != 0))
                .ToList();
            
            // elimino las materias que no estan en la lista
            foreach (var mat in materiasParaBorrar)
            {
                existingPlan.QuitarMateria(mat);
                _context.Materias.Remove(mat);
            }

            // agrego las materias nuevas que vienen con el id == o
            foreach (var matNueva in plan.Materias.Where(m => m.ID == 0))
            {
                existingPlan.AgregarMateria(matNueva);
            }

            // actualizamos las materias existentes, vienen con id != 0
            foreach (var matExistente in plan.Materias.Where(m => m.ID != 0))
            {
                var matOriginal = existingPlan.Materias.FirstOrDefault(m => m.ID == matExistente.ID);
                if (matOriginal != null)
                {
                    matOriginal.SetDescripcion(matExistente.Descripcion);
                    matOriginal.SetHSSemanales(matExistente.HSSemanales);
                    matOriginal.SetHSTotales(matExistente.HSTotales);
                }
            }
            

            // para que el plan no quede sin materias
            if (!existingPlan.Materias.Any())
            {
                throw new InvalidOperationException("El plan de estudio debe contener al menos una materia.");
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Buscar el plan con sus materias
            var plan = await _context.Planes
                .Include(p => p.Materias)
                .FirstOrDefaultAsync(p => p.ID == id);
                
            if (plan != null)
            {
                // Al remover el plan, EF Core borrara las materias si está configurado en cascada
                // o tirara error si no. Removemos manualmente por las dudas.
                _context.Materias.RemoveRange(plan.Materias);
                _context.Planes.Remove(plan);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
