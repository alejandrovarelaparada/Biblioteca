using Biblioteca.Application.DTOs;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<IEnumerable<AutorResponseDto>> ListarAutoresAsync()
        {
            var autores = await _autorRepository.ObtenerAutoresAsync();

            return autores.Select(autor => new AutorResponseDto
            {
                AutorId = autor.Id,
                Nombre = autor.Nombre,
                Nacionalidad = autor.Nacionalidad
            });
        }

        public async Task<AutorResponseDto> RegistrarAutorAsync(AutorCreateDto autorCreado)
        {
            var nuevoAutor = new Autor
            {
                Nombre = autorCreado.Nombre,
                Nacionalidad = autorCreado.Nacionalidad
            };

            await _autorRepository.InsertarNuevoAutorAsync(nuevoAutor);

            return new AutorResponseDto
            {
                AutorId = nuevoAutor.Id,
                Nombre = nuevoAutor.Nombre,
                Nacionalidad = nuevoAutor.Nacionalidad
            };
        }

        public async Task<AutorResponseDto?> BuscarAutorPorIdAsync(int autorId)
        {
            var autor = await _autorRepository.ObtenerAutorPorIdAsync(autorId);
            AutorResponseDto? autorEncontrado = null;

            if (autor != null)
            {
                autorEncontrado = new AutorResponseDto
                {
                    AutorId = autor.Id,
                    Nombre = autor.Nombre,
                    Nacionalidad = autor.Nacionalidad
                };
            }

            return autorEncontrado;
        }

        public async Task<bool> ModificarDatosAutorAsync(int autorId, AutorCreateDto datosNuevos)
        {
            var autorExistente = await _autorRepository.ObtenerAutorPorIdAsync(autorId);
            bool modificado = false;

            if (autorExistente != null)
            {
                autorExistente.Nombre = datosNuevos.Nombre;
                autorExistente.Nacionalidad = datosNuevos.Nacionalidad;

                await _autorRepository.ActualizarAutorAsync(autorExistente);

                modificado = true;
            }

            return modificado;
        }

        public async Task<bool> BorrarAutorAsync(int autorId)
        {
            var autorAEliminar = await _autorRepository.ObtenerAutorPorIdAsync(autorId);
            bool borrado = false;

            if (autorAEliminar != null)
            {
                await _autorRepository.EliminarAutorAsync(autorAEliminar);

                borrado = true;
            }

            return borrado;
        }
    }
}