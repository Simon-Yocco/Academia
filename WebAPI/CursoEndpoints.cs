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
            .WithOpenApi();

            app.MapGet("/cursos/{id}", async (int id, ICursoService cursoService) =>
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
            .WithOpenApi();

            app.MapPost("/cursos", async (CursoDTO dto, ICursoService cursoService) =>
            {
                CursoDTO? cursoDTO = await cursoService.AddAsync(dto);
                return Results.Created($"/cursos/{cursoDTO?.ID}", cursoDTO);
            })
            .WithName("AddCurso")
            .Produces<CursoDTO>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/cursos", async (CursoDTO dto, ICursoService cursoService) =>
            {
                var found = await cursoService.UpdateAsync(dto);

                if (!found)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();

            })
            .WithName("UpdateCurso")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .WithOpenApi();

            app.MapDelete("/cursos/{id}", async (int id, ICursoService cursoService) =>
            {
                var deleted = await cursoService.DeleteAsync(id);
                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteCurso")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .WithOpenApi();
        }
    }
}
