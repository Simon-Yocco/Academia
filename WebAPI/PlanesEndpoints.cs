using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebAPI
{
    public static class PlanesEndpoints
    {
        public static void MapPlanEndpoints(this WebApplication app)
        {
            app.MapGet("/planes", async (IPlanService planService) =>
            {
                var dtos = await planService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllPlanes")
            .Produces<List<PlanDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("Admin");

            app.MapGet("/planes/{id}", async (int id, IPlanService planService) =>
            {
                PlanDTO? dto = await planService.GetByIdAsync(id);
                if (dto != null)
                {
                    return Results.Ok(dto);
                }
                return Results.NotFound();
            })
            .WithName("GetPlan")
            .Produces<PlanDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization("Admin");

            app.MapGet("/planes/buscar/{criteria}", async (string criteria, IPlanService planService) =>
            {
                var dtos = await planService.GetByCriteriaAsync(criteria);
                return Results.Ok(dtos);
            })
            .WithName("GetPlanesByCriteria")
            .Produces<List<PlanDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("Admin");

            app.MapPost("/planes", async (PlanDTO dto, IPlanService planService) =>
            {
                try
                {
                    PlanDTO? planCreado = await planService.AddAsync(dto);
                    return Results.Created($"/planes/{planCreado?.ID}", planCreado);
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
            .WithName("AddPlan")
            .Produces<PlanDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("Admin");

            app.MapPut("/planes", async (PlanDTO dto, IPlanService planService) =>
            {
                try
                {
                    var found = await planService.UpdateAsync(dto);
                    if (!found)
                    {
                        return Results.NotFound();
                    }
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    // devuelvo HTTP 400 si intentan poner materias de otro plan o dejar el plan vacio
                    return Results.BadRequest(new { error = ex.Message });
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
            .WithName("UpdatePlan")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("Admin");

            app.MapDelete("/planes/{id}", async (int id, IPlanService planService) =>
            {
                try
                {
                    var deleted = await planService.DeleteAsync(id);
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
            .WithName("DeletePlan")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("Admin");
        }
    }
}
