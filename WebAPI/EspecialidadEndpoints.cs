using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class EspecialidadEndpoints
    {
        public static void MapEspecialidadEndpoints(this WebApplication app)
        {
            app.MapGet("/especialidades", async (IEspecialidadService especialidadService) =>
            {
                var dtos = await especialidadService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllEspecialidades") // Le asigna un nombre único interno a este endpoint
            .Produces<List<EspecialidadDTO>>(StatusCodes.Status200OK) // Codigo para Swagger
            .WithOpenApi();

            app.MapGet("/especialidades/{id}", async (int id, IEspecialidadService especialidadService) =>
            {
                EspecialidadDTO? dto = await especialidadService.GetByIdAsync(id);
                if (dto != null)
                {
                    return Results.Ok(dto);
                }
                return Results.NotFound();
            })
            .WithName("GetEspecialidad")
            .Produces<EspecialidadDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/especialidades", async (EspecialidadDTO dto, IEspecialidadService especialidadService) =>
            {
                EspecialidadDTO especialidadDTO = await especialidadService.AddAsync(dto);
                return Results.Created($"/especialidades/{especialidadDTO.ID}", especialidadDTO);
            })
            .WithName("AddEspecialidad")
            .Produces<EspecialidadDTO>(StatusCodes.Status201Created)
            .WithOpenApi();

            app.MapPut("/especialidades", async (EspecialidadDTO dto, IEspecialidadService especialidadService) =>
            {
                var found = await especialidadService.UpdateAsync(dto);

                if (!found)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();

            })
            .WithName("UpdateEspecialidad")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .WithOpenApi();

            app.MapDelete("/especialidades/{id}", async (int id, IEspecialidadService especialidadService) =>
            {
                var deleted = await especialidadService.DeleteAsync(id);
                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteEspecialidad")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .WithOpenApi();
        }
    }
}
