using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebAPI
{
    public static class ComisionEndpoints
    {
        public static void MapComisionEndpoints(this WebApplication app)
        {
            app.MapGet("/comisiones", async (IComisionService s) => Results.Ok(await s.GetAllAsync()))
               .WithName("GetAllComisiones").Produces<List<ComisionDTO>>(StatusCodes.Status200OK).WithOpenApi().RequireAuthorization();
        }
    }
}

