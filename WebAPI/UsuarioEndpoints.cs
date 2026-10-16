using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebAPI
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoints(this WebApplication app)
        {
            app.MapGet("/usuarios", async (IUsuarioService usuarioService) =>
            {
                var dtos = await usuarioService.GetAllAsync();
                return Results.Ok(dtos);
            })
            .WithName("GetAllUsuarios")
            .Produces<List<UsuarioDTO>>(StatusCodes.Status200OK)
            .WithOpenApi().RequireAuthorization();

            app.MapGet("/usuarios/proximo-legajo", async (IUsuarioService usuarioService) =>
            {
                var proximoLegajo = await usuarioService.GetProximoLegajoAsync();
                return Results.Ok(proximoLegajo);
            })
            .WithName("GetProximoLegajo")
            .Produces<int>(StatusCodes.Status200OK)
            .WithOpenApi().RequireAuthorization();

            app.MapGet("/usuarios/{id}", async (int id, IUsuarioService usuarioService) =>
            {
                UsuarioDTO? dto = await usuarioService.GetByIdAsync(id);
                if (dto != null)
                {
                    return Results.Ok(dto);
                }
                return Results.NotFound();
            })
            .WithName("GetUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi().RequireAuthorization();

            app.MapPost("/usuarios", async (UsuarioDTO dto, IUsuarioService usuarioService) =>
            {
                try
                {
                    UsuarioDTO? usuarioDTO = await usuarioService.AddAsync(dto);
                    return Results.Created($"/usuarios/{usuarioDTO?.ID}", usuarioDTO);
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
            .WithName("AddUsuario")
            .Produces<UsuarioDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization();

            app.MapPut("/usuarios", async (UsuarioDTO dto, IUsuarioService usuarioService) =>
            {
                try
                {
                    var found = await usuarioService.UpdateAsync(dto);
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
            .WithName("UpdateUsuario")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization();

            app.MapDelete("/usuarios/{id}", async (int id, IUsuarioService usuarioService) =>
            {
                try
                {
                    var deleted = await usuarioService.DeleteAsync(id);
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
            .WithName("DeleteUsuario")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi().RequireAuthorization();
        }
    }
}
