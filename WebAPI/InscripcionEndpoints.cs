using Data;
using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebAPI
{
    public static class InscripcionEndpoints
    {
        public static void MapInscripcionEndpoints(this WebApplication app)
        {
            app.MapGet("/inscripciones", async (IAlumnoInscripcionService service) =>
            {
                var dtos = await service.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllInscripciones")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization();

            app.MapGet("/inscripciones/{id:int}", async (int id, IAlumnoInscripcionService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                return dto != null ? Results.Ok(dto) : Results.NotFound();
            })
            .WithName("GetInscripcion")
            .Produces<AlumnoInscripcionDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization();

            app.MapGet("/inscripciones/curso/{idCurso:int}", async (int idCurso, IAlumnoInscripcionService service) =>
            {
                var dtos = await service.GetByCursoAsync(idCurso);
                return Results.Ok(dtos);
            })
            .WithName("GetInscripcionesByCurso")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization();

            app.MapGet("/inscripciones/alumno/{idAlumno:int}", async (int idAlumno, IAlumnoInscripcionService service) =>
            {
                var dtos = await service.GetByAlumnoAsync(idAlumno);
                return Results.Ok(dtos);
            })
            .WithName("GetInscripcionesByAlumno")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization();

            app.MapGet("/inscripciones/buscar/{criteria}", async (string criteria, IAlumnoInscripcionService service) =>
            {
                var dtos = await service.GetByCriteriaAsync(criteria);
                return Results.Ok(dtos);
            })
            .WithName("GetInscripcionesByCriteria")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization();

            app.MapPost("/inscripciones", async (AlumnoInscripcionDTO dto, IAlumnoInscripcionService service) =>
            {
                try
                {
                    var creada = await service.AddAsync(dto);
                    return Results.Created($"/inscripciones/{creada?.ID}", creada);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("AddInscripcion")
            .Produces<AlumnoInscripcionDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization();

            app.MapPut("/inscripciones", async (AlumnoInscripcionDTO dto, IAlumnoInscripcionService service) =>
            {
                try
                {
                    var ok = await service.UpdateAsync(dto);
                    if (!ok) return Results.NotFound();
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("UpdateInscripcion")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization();

            app.MapDelete("/inscripciones/{id:int}", async (int id, IAlumnoInscripcionService service) =>
            {
                try
                {
                    var ok = await service.DeleteAsync(id);
                    return ok ? Results.NoContent() : Results.NotFound();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("DeleteInscripcion")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization();
        }
    }
}
