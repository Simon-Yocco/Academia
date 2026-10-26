using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebAPI
{
    public static class ReporteEndpoints
    {
        public static void MapReporteEndpoints(this WebApplication app)
        {
            // Reporte 1: Cursos y Ocupación
            app.MapGet("/reportes/cursos-ocupacion", async (IReporteService reporteService) =>
            {
                var reporte = await reporteService.GetCursosOcupacionAsync();
                return Results.Ok(reporte);
            })
            .WithName("GetReporteCursosOcupacion")
            .Produces<List<CursoOcupacionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("Admin");

            // Reporte 2: Rendimiento Académico por Curso (% Aprobados, Regulares, Libres)
            app.MapGet("/reportes/cursos-rendimiento", async (IReporteService reporteService) =>
            {
                var reporte = await reporteService.GetCursosRendimientoAsync();
                return Results.Ok(reporte);
            })
            .WithName("GetReporteCursosRendimiento")
            .Produces<List<CursoRendimientoDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("Admin");
        }
    }
}
