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
            app.MapGet("/comisiones", async (IComisionService comisionService) =>
            {
                var dtos = await comisionService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllComisiones")
            .Produces<List<ComisionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi().RequireAuthorization("Admin");

            app.MapGet("/comisiones/{id}", async (int id, IComisionService comisionService) =>
            {
                ComisionDTO? dto = await comisionService.GetByIdAsync(id);
                if (dto != null)
                {
                    return Results.Ok(dto);
                }
                return Results.NotFound();
            })
            .WithName("GetComision")
            .Produces<ComisionDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi().RequireAuthorization("Admin");

            app.MapGet("/comisiones/buscar/{criteria}", async (string criteria, IComisionService comisionService) =>
            {
                var dtos = await comisionService.GetByCriteriaAsync(criteria);
                return Results.Ok(dtos);
            })
            .WithName("GetComisionesByCriteria")
            .Produces<List<ComisionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi().RequireAuthorization("Admin");

            app.MapPost("/comisiones", async (ComisionDTO dto, IComisionService comisionService) =>
            {
                try
                {
                    ComisionDTO? comisionDTO = await comisionService.AddAsync(dto);
                    return Results.Created($"/comisiones/{comisionDTO?.ID}", comisionDTO);
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
            .WithName("AddComision")
            .Produces<ComisionDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization("Admin");

            app.MapPut("/comisiones", async (ComisionDTO dto, IComisionService comisionService) =>
            {
                try
                {
                    var found = await comisionService.UpdateAsync(dto);
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
            .WithName("UpdateComision")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization("Admin");

            app.MapDelete("/comisiones/{id}", async (int id, IComisionService comisionService) =>
            {
                try
                {
                    var deleted = await comisionService.DeleteAsync(id);
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
            .WithName("DeleteComision")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization("Admin");
        }
    }
}

