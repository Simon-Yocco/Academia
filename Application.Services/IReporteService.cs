using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IReporteService
    {
        Task<IEnumerable<CursoOcupacionDTO>> GetCursosOcupacionAsync();
        Task<IEnumerable<CursoRendimientoDTO>> GetCursosRendimientoAsync();
    }
}
