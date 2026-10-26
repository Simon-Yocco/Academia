using Data;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this WebApplication app)
        {
            // Reporte 1: Cursos y Ocupación
            app.MapGet("/reportes/cursos-ocupacion", async (TPIContext context) =>
            {
                var cursos = await context.Cursos
                    .Include(c => c.Materia)
                    .Include(c => c.Comision)
                    .Include(c => c.Docente)
                    .OrderByDescending(c => c.AnioCalendario)
                    .ThenBy(c => c.Materia != null ? c.Materia.Descripcion : "")
                    .ToListAsync();

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

            // Reporte 2: Rendimiento Académico por Curso (% Aprobados, Regulares, Libres)
            app.MapGet("/reportes/cursos-rendimiento", async (TPIContext context) =>
            {
                var cursos = await context.Cursos
                    .Include(c => c.Materia)
                    .Include(c => c.Comision)
                    .Include(c => c.Docente)
                    .OrderByDescending(c => c.AnioCalendario)
                    .ThenBy(c => c.Materia != null ? c.Materia.Descripcion : "")
                    .ToListAsync();

                var inscripciones = await context.AlumnosInscripciones.ToListAsync();

                var inscripcionesPorCurso = inscripciones
                    .GroupBy(i => i.IDCurso)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var reporte = cursos.Select(c =>
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

                return Results.Ok(reporte);
            })
            .WithName("GetReporteCursosRendimiento")
            .Produces<List<CursoRendimientoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("CursosLeer");
        }
    }
}
