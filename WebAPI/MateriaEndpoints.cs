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

            app.MapGet("/materias/{id}", async (int id, IMateriaService s) =>
            {
                var dto = await s.GetByIdAsync(id);
                if (dto != null)
                {
                    return Results.Ok(dto);
                }
                return Results.NotFound();
            })
            .WithName("GetMateria")
            .Produces<MateriaDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi().RequireAuthorization();

            app.MapGet("/materias/buscar/{criteria}", async (string criteria, IMateriaService s) =>
            {
                var dtos = await s.GetByCriteriaAsync(criteria);
                return Results.Ok(dtos);
            })
            .WithName("GetMateriasByCriteria")
            .Produces<List<MateriaDTO>>(StatusCodes.Status200OK)
            .WithOpenApi().RequireAuthorization();

            app.MapPost("/materias", async (MateriaDTO dto, IMateriaService s) =>
            {
                try
                {
                    var materiaDTO = await s.AddAsync(dto);
                    return Results.Created($"/materias/{materiaDTO?.ID}", materiaDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("AddMateria")
            .Produces<MateriaDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization();

            app.MapPut("/materias", async (MateriaDTO dto, IMateriaService s) =>
            {
                try
                {
                    var found = await s.UpdateAsync(dto);
                    if (!found)
                    {
                        return Results.NotFound();
                    }
                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("UpdateMateria")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization();

            app.MapDelete("/materias/{id}", async (int id, IMateriaService s) =>
            {
                try
                {
                    var deleted = await s.DeleteAsync(id);
                    if (!deleted)
                    {
                        return Results.NotFound();
                    }
                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("DeleteMateria")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization();
        }
    }
}

