using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using DTOs;
using Entidades;

namespace Application.Services
{
    public class AlumnoInscripcionService : IAlumnoInscripcionService
    {
        private readonly IAlumnoInscripcionRepository _inscripcionRepo;
        private readonly ICursoRepository _cursoRepo;

        public AlumnoInscripcionService(
            IAlumnoInscripcionRepository inscripcionRepo,
            ICursoRepository cursoRepo)
        {
            _inscripcionRepo = inscripcionRepo;
            _cursoRepo = cursoRepo;
        }

        public async Task<AlumnoInscripcionDTO?> AddAsync(AlumnoInscripcionDTO dto)
        {
            // 1. Validar si ya está inscripto en este curso
            if (await _inscripcionRepo.ExisteInscripcionAsync(dto.IDAlumno, dto.IDCurso))
            {
                throw new InvalidOperationException("El alumno ya se encuentra inscripto en este curso.");
            }

            // 2. Validar que el curso exista y tenga cupo disponible
            var curso = await _cursoRepo.GetAsync(dto.IDCurso);
            if (curso == null)
            {
                throw new ArgumentException("El curso seleccionado no existe.");
            }

            var inscriptos = await _inscripcionRepo.CountInscriptosByCursoAsync(dto.IDCurso);
            if (inscriptos >= curso.Cupo)
            {
                throw new InvalidOperationException($"No hay cupo disponible. El curso alcanzó el límite máximo de {curso.Cupo} alumnos.");
            }

            // 3. Crear entidad y persistir
            var entidad = new AlumnoInscripcion(0, dto.IDAlumno, dto.IDCurso, dto.Condicion, dto.Nota);
            await _inscripcionRepo.AddAsync(entidad);

            dto.ID = entidad.ID;
            return dto;
        }

        public async Task<bool> UpdateAsync(AlumnoInscripcionDTO dto)
        {
            var existing = await _inscripcionRepo.GetAsync(dto.ID);
            if (existing == null) return false;

            // Si cambió el curso (transferencia / cambio de comisión)
            if (existing.IDCurso != dto.IDCurso)
            {
                if (await _inscripcionRepo.ExisteInscripcionAsync(dto.IDAlumno, dto.IDCurso))
                {
                    throw new InvalidOperationException("El alumno ya está inscripto en el curso de destino.");
                }

                var nuevoCurso = await _cursoRepo.GetAsync(dto.IDCurso);
                if (nuevoCurso == null)
                {
                    throw new ArgumentException("El curso de destino no existe.");
                }

                var inscriptos = await _inscripcionRepo.CountInscriptosByCursoAsync(dto.IDCurso);
                if (inscriptos >= nuevoCurso.Cupo)
                {
                    throw new InvalidOperationException($"El curso de destino no tiene cupo disponible (Cupo: {nuevoCurso.Cupo}).");
                }
            }

            var entidad = new AlumnoInscripcion(dto.ID, dto.IDAlumno, dto.IDCurso, dto.Condicion, dto.Nota);
            return await _inscripcionRepo.UpdateAsync(entidad);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _inscripcionRepo.DeleteAsync(id);
        }

        public async Task<AlumnoInscripcionDTO?> GetByIdAsync(int id)
        {
            var item = await _inscripcionRepo.GetAsync(id);
            return item == null ? null : MapToDTO(item);
        }

        public async Task<IEnumerable<AlumnoInscripcionDTO>> GetAllAsync()
        {
            var items = await _inscripcionRepo.GetAllAsync();
            return items.Select(MapToDTO).ToList();
        }

        public async Task<IEnumerable<AlumnoInscripcionDTO>> GetByCursoAsync(int idCurso)
        {
            var items = await _inscripcionRepo.GetByCursoAsync(idCurso);
            return items.Select(MapToDTO).ToList();
        }

        public async Task<IEnumerable<AlumnoInscripcionDTO>> GetByAlumnoAsync(int idAlumno)
        {
            var items = await _inscripcionRepo.GetByAlumnoAsync(idAlumno);
            return items.Select(MapToDTO).ToList();
        }

        public async Task<IEnumerable<AlumnoInscripcionDTO>> GetByCriteriaAsync(string criteria)
        {
            var items = await _inscripcionRepo.GetByCriteriaAsync(criteria);
            return items.Select(MapToDTO).ToList();
        }

        private static AlumnoInscripcionDTO MapToDTO(AlumnoInscripcion i)
        {
            return new AlumnoInscripcionDTO
            {
                ID = i.ID,
                IDAlumno = i.IDAlumno,
                IDCurso = i.IDCurso,
                Condicion = i.Condicion,
                Nota = i.Nota,
                AlumnoNombreCompleto = i.Alumno != null ? $"{i.Alumno.Apellido}, {i.Alumno.Nombre}" : "Sin Alumno",
                AlumnoLegajo = i.Alumno?.Legajo ?? 0,
                CursoDescripcion = i.Curso?.Descripcion ?? "Sin Curso",
                MateriaDescripcion = i.Curso?.Materia?.Descripcion ?? "Sin Materia",
                ComisionDescripcion = i.Curso?.Comision?.Descripcion ?? "Sin Comisión",
                AnioCalendario = i.Curso?.AnioCalendario ?? 0,
                Cupo = i.Curso?.Cupo ?? 0
            };
        }
    }
}
