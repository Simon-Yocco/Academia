using Data;
using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WebAPI
{
    public static class InscripcionEndpoints
    {
        public static void MapInscripcionEndpoints(this WebApplication app)
        {
            app.MapGet("/inscripciones", async (ClaimsPrincipal user, IAlumnoInscripcionService service) =>
            {
                var rol = user.FindFirst(ClaimTypes.Role)?.Value;
                var idPersonaStr = user.FindFirst("id_persona")?.Value;
                int.TryParse(idPersonaStr, out int idPersona);

                if (rol == "Administrador")
                {
                    var dtos = await service.GetAllAsync();
                    return Results.Ok(dtos);
                }
                else if (rol == "Docente")
                {
                    var dtos = await service.GetByDocenteAsync(idPersona);
                    return Results.Ok(dtos);
                }
                else if (rol == "Alumno")
                {
                    var dtos = await service.GetByAlumnoAsync(idPersona);
                    return Results.Ok(dtos);
                }

                return Results.Forbid();
            })
            .WithName("GetAllInscripciones")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("InscripcionesLeer");

            app.MapGet("/inscripciones/{id:int}", async (int id, ClaimsPrincipal user, IAlumnoInscripcionService service) =>
            {
                var dto = await service.GetByIdAsync(id);
                if (dto == null) return Results.NotFound();

                var rol = user.FindFirst(ClaimTypes.Role)?.Value;
                var idPersonaStr = user.FindFirst("id_persona")?.Value;
                int.TryParse(idPersonaStr, out int idPersona);

                if (rol == "Alumno" && dto.IDAlumno != idPersona)
                {
                    return Results.Forbid();
                }

                return Results.Ok(dto);
            })
            .WithName("GetInscripcion")
            .Produces<AlumnoInscripcionDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization("InscripcionesLeer");

            app.MapGet("/inscripciones/curso/{idCurso:int}", async (int idCurso, IAlumnoInscripcionService service) =>
            {
                var dtos = await service.GetByCursoAsync(idCurso);
                return Results.Ok(dtos);
            })
            .WithName("GetInscripcionesByCurso")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("InscripcionesLeer");

            app.MapGet("/inscripciones/alumno/{idAlumno:int}", async (int idAlumno, ClaimsPrincipal user, IAlumnoInscripcionService service) =>
            {
                var rol = user.FindFirst(ClaimTypes.Role)?.Value;
                var idPersonaStr = user.FindFirst("id_persona")?.Value;
                int.TryParse(idPersonaStr, out int idPersona);

                if (rol == "Alumno" && idPersona != idAlumno)
                {
                    return Results.Forbid();
                }

                var dtos = await service.GetByAlumnoAsync(idAlumno);
                return Results.Ok(dtos);
            })
            .WithName("GetInscripcionesByAlumno")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("InscripcionesLeer");

            app.MapGet("/inscripciones/buscar/{criteria}", async (string criteria, ClaimsPrincipal user, IAlumnoInscripcionService service) =>
            {
                var rol = user.FindFirst(ClaimTypes.Role)?.Value;
                var idPersonaStr = user.FindFirst("id_persona")?.Value;
                int.TryParse(idPersonaStr, out int idPersona);

                IEnumerable<AlumnoInscripcionDTO> dtos;
                if (rol == "Administrador")
                {
                    dtos = await service.GetByCriteriaAsync(criteria);
                }
                else if (rol == "Docente")
                {
                    var docentesInscripciones = await service.GetByDocenteAsync(idPersona);
                    dtos = docentesInscripciones.Where(i =>
                        i.AlumnoNombreCompleto.Contains(criteria, StringComparison.OrdinalIgnoreCase) ||
                        i.MateriaDescripcion.Contains(criteria, StringComparison.OrdinalIgnoreCase) ||
                        i.AlumnoLegajo.ToString().Contains(criteria));
                }
                else if (rol == "Alumno")
                {
                    var alumnoInscripciones = await service.GetByAlumnoAsync(idPersona);
                    dtos = alumnoInscripciones.Where(i =>
                        i.MateriaDescripcion.Contains(criteria, StringComparison.OrdinalIgnoreCase) ||
                        i.ComisionDescripcion.Contains(criteria, StringComparison.OrdinalIgnoreCase) ||
                        i.Condicion.Contains(criteria, StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    return Results.Forbid();
                }

                return Results.Ok(dtos);
            })
            .WithName("GetInscripcionesByCriteria")
            .Produces<List<AlumnoInscripcionDTO>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .RequireAuthorization("InscripcionesLeer");

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
            .RequireAuthorization("InscripcionesAgregar");

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
            .RequireAuthorization("InscripcionesActualizar");

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
            .RequireAuthorization("InscripcionesEliminar");
        }
    }
}
