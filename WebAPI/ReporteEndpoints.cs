using Data;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this WebApplication app)
        {
            app.MapGet("/reportes/cursos-ocupacion", async (TPIContext context) =>
            {
                var cursos = await context.Cursos
                    .Include(c => c.Materia)
                    .Include(c => c.Comision)
                    .Include(c => c.Docente)
                    .OrderByDescending(c => c.AnioCalendario)
                    .ThenBy(c => c.Materia != null ? c.Materia.Descripcion : "")
                    .ToListAsync();

                // Conteo de inscriptos por curso
                var inscriptosPorCurso = await context.AlumnosInscripciones
                    .GroupBy(i => i.IDCurso)
                    .Select(g => new { IDCurso = g.Key, Cantidad = g.Count() })
                    .ToDictionaryAsync(x => x.IDCurso, x => x.Cantidad);

                var reporte = cursos.Select(c => new CursoOcupacionDTO
                {
                    IDCurso = c.ID,
                    Materia = c.Materia?.Descripcion ?? "Sin Materia",
                    Comision = c.Comision?.Descripcion ?? "Sin Comisión",
                    AnioCalendario = c.AnioCalendario,
                    Docente = c.Docente != null ? $"{c.Docente.Apellido}, {c.Docente.Nombre}" : "Sin Asignar",
                    CupoTotal = c.Cupo,
                    Inscriptos = inscriptosPorCurso.ContainsKey(c.ID) ? inscriptosPorCurso[c.ID] : 0
                }).ToList();

                return Results.Ok(reporte);
            })
            .WithName("GetReporteCursosOcupacion")
            .Produces<List<CursoOcupacionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("CursosLeer");
        }
    }
}
