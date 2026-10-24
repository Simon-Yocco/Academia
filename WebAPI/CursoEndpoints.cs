using Data;
using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class CursoEndpoints
    {
        public static void MapCursoEndpoints(this WebApplication app)
        {
            app.MapGet("/cursos", async (ICursoService cursoService) =>
            {
                var dtos = await cursoService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllCursos") // Le asigna un nombre único interno a este endpoint
            .Produces<List<CursoDTO>>(StatusCodes.Status200OK) // Codigo para Swagger
            .WithOpenApi()
            .RequireAuthorization("CursosLeer");

            app.MapGet("/cursos/{id:int}", async (int id, ICursoService cursoService) =>
            {
                CursoDTO? dto = await cursoService.GetByIdAsync(id);
                if (dto != null)
                {
                    return Results.Ok(dto);
                }
                return Results.NotFound();
            })
            .WithName("GetCurso")
            .Produces<CursoDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi()
            .RequireAuthorization("CursosLeer");

            app.MapGet("/cursos/buscar/{criteria}", async (string criteria, ICursoService cursoService) =>
            {
                var dtos = await cursoService.GetByCriteriaAsync(criteria);
                return Results.Ok(dtos);
            })
            .WithName("GetByCriteria") // Le asigna un nombre único interno a este endpoint
            .Produces<List<CursoDTO>>(StatusCodes.Status200OK) // Codigo para Swagger
            .WithOpenApi()
            .RequireAuthorization("CursosLeer");

            app.MapPost("/cursos", async (CursoDTO dto, ICursoService cursoService) =>
            {
                try
                {
                    CursoDTO? cursoDTO = await cursoService.AddAsync(dto);
                    return Results.Created($"/cursos/{cursoDTO?.ID}", cursoDTO);
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
            .WithName("AddCurso")
            .Produces<CursoDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("Admin");

            app.MapPut("/cursos", async (CursoDTO dto, ICursoService cursoService) =>
            {
                try
                {
                    var found = await cursoService.UpdateAsync(dto);

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
            .WithName("UpdateCurso")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("Admin");

            app.MapDelete("/cursos/{id}", async (int id, ICursoService cursoService) =>
            {
                try
                {
                    var deleted = await cursoService.DeleteAsync(id);
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
            .WithName("DeleteCurso")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi()
            .RequireAuthorization("Admin");
        }
    }
}
