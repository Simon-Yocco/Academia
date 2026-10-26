using Data;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IAlumnoInscripcionRepository _inscripcionRepository;

        public ReporteService(ICursoRepository cursoRepository, IAlumnoInscripcionRepository inscripcionRepository)
        {
            _cursoRepository = cursoRepository;
            _inscripcionRepository = inscripcionRepository;
        }

        public async Task<IEnumerable<CursoOcupacionDTO>> GetCursosOcupacionAsync()
        {
            var cursos = (await _cursoRepository.GetAllAsync())
                .OrderByDescending(c => c.AnioCalendario)
                .ThenBy(c => c.Materia?.Descripcion ?? string.Empty)
                .ToList();

            var inscripciones = await _inscripcionRepository.GetAllAsync();

            var inscriptosPorCurso = inscripciones
                .GroupBy(i => i.IDCurso)
                .ToDictionary(g => g.Key, g => g.Count());

            return cursos.Select(c => new CursoOcupacionDTO
            {
                IDCurso = c.ID,
                Materia = c.Materia?.Descripcion ?? "Sin Materia",
                Comision = c.Comision?.Descripcion ?? "Sin Comisión",
                AnioCalendario = c.AnioCalendario,
                Docente = c.Docente != null ? $"{c.Docente.Apellido}, {c.Docente.Nombre}" : "Sin Asignar",
                CupoTotal = c.Cupo,
                Inscriptos = inscriptosPorCurso.ContainsKey(c.ID) ? inscriptosPorCurso[c.ID] : 0
            }).ToList();
        }

        public async Task<IEnumerable<CursoRendimientoDTO>> GetCursosRendimientoAsync()
        {
            var cursos = (await _cursoRepository.GetAllAsync())
                .OrderByDescending(c => c.AnioCalendario)
                .ThenBy(c => c.Materia?.Descripcion ?? string.Empty)
                .ToList();

            var inscripciones = await _inscripcionRepository.GetAllAsync();

            var inscripcionesPorCurso = inscripciones
                .GroupBy(i => i.IDCurso)
                .ToDictionary(g => g.Key, g => g.ToList());

            return cursos.Select(c =>
            {
                var lista = inscripcionesPorCurso.ContainsKey(c.ID)
                    ? inscripcionesPorCurso[c.ID]
                    : new List<Entidades.AlumnoInscripcion>();

                int total = lista.Count;
                int aprobados = lista.Count(i => string.Equals(i.Condicion, "Aprobado", StringComparison.OrdinalIgnoreCase));
                int regulares = lista.Count(i => string.Equals(i.Condicion, "Regular", StringComparison.OrdinalIgnoreCase));
                int libres = lista.Count(i => string.Equals(i.Condicion, "Libre", StringComparison.OrdinalIgnoreCase));
                int cursando = lista.Count(i => string.Equals(i.Condicion, "Inscripto", StringComparison.OrdinalIgnoreCase)
                                               || (!string.Equals(i.Condicion, "Aprobado", StringComparison.OrdinalIgnoreCase)
                                                   && !string.Equals(i.Condicion, "Regular", StringComparison.OrdinalIgnoreCase)
                                                   && !string.Equals(i.Condicion, "Libre", StringComparison.OrdinalIgnoreCase)));

                var notas = lista.Where(i => i.Nota.HasValue).Select(i => i.Nota!.Value).ToList();
                double? promedio = notas.Count > 0 ? Math.Round(notas.Average(), 2) : null;

                return new CursoRendimientoDTO
                {
                    IDCurso = c.ID,
                    Materia = c.Materia?.Descripcion ?? "Sin Materia",
                    Comision = c.Comision?.Descripcion ?? "Sin Comisión",
                    AnioCalendario = c.AnioCalendario,
                    Docente = c.Docente != null ? $"{c.Docente.Apellido}, {c.Docente.Nombre}" : "Sin Asignar",
                    TotalInscriptos = total,
                    CantidadAprobados = aprobados,
                    CantidadRegulares = regulares,
                    CantidadLibres = libres,
                    CantidadCursando = cursando,
                    NotaPromedio = promedio
                };
            }).ToList();
        }
    }
}
