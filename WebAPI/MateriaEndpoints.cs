using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebAPI
{
    public static class MateriaEndpoints
    {
        public static void MapMateriaEndpoints(this WebApplication app)
        {
            app.MapGet("/materias", async (IMateriaService s) =>
            {
                var dtos = await s.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllMaterias")
            .Produces<List<MateriaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi().RequireAuthorization();
        }
    }
}

