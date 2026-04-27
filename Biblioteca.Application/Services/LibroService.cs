using Biblioteca.Application.DTOs;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;
        
        public LibroService(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public async Task<IEnumerable<LibroResponseDto>> ListarLibrosAsync()
        {
            var libros = await _libroRepository.ObtenerLibrosAsync();

            return libros.Select(libro => new LibroResponseDto
            {
                LibroId = libro.Id,
                Titulo = libro.Titulo,
                Sinopsis = libro.Sinopsis,
                AutorId = libro.AutorId,
                NombreAutor = libro.Autor?.Nombre ?? "Autor Desconocido"
            });
        }

        public async Task<LibroResponseDto> RegistrarLibroAsync(LibroCreateDto libroCreado)
        {
            var nuevoLibro = new Libro
            {
                Titulo = libroCreado.Titulo,
                Sinopsis = libroCreado.Sinopsis,
                AutorId = libroCreado.AutorId,
            };

            await _libroRepository.InsertarNuevoLibroAsync(nuevoLibro);

            return new LibroResponseDto
            {
                LibroId = nuevoLibro.Id,
                Titulo = nuevoLibro.Titulo,
                Sinopsis = nuevoLibro.Sinopsis,
                AutorId = nuevoLibro.AutorId,
                NombreAutor = nuevoLibro.Autor?.Nombre ?? "Autor Desconocido"
            };
        }

        public async Task<LibroResponseDto?> BuscarLibroPorIdAsync(int libroId)
        {
            var libro = await _libroRepository.ObtenerLibroPorIdAsync(libroId);
            LibroResponseDto? libroEncontrado = null;

            if (libro != null)
            {
                libroEncontrado = new LibroResponseDto
                {
                    LibroId = libro.Id,
                    Titulo = libro.Titulo,
                    Sinopsis = libro.Sinopsis,
                    NombreAutor = libro.Autor?.Nombre ?? "Desconocido"
                };
            }

            return libroEncontrado;
        }

        public async Task<bool> ModificarDatosLibroAsync(int libroId, LibroCreateDto datosNuevos)
        {
            var libroExistente = await _libroRepository.ObtenerLibroPorIdAsync(libroId);
            bool modificado = false;

            if (libroExistente != null)
            {
                libroExistente.Titulo = datosNuevos.Titulo;
                libroExistente.Sinopsis = datosNuevos.Sinopsis;
                libroExistente.AutorId = datosNuevos.AutorId;

                await _libroRepository.ActualizarLibroAsync(libroExistente);

                modificado = true;
            }

            return modificado;
        }

        public async Task<bool> BorrarLibroAsync(int libroId)
        {
            var libroAEliminar = await _libroRepository.ObtenerLibroPorIdAsync(libroId);
            bool borrado = false;

            if (libroAEliminar != null)
            {
                await _libroRepository.EliminarLibroAsync(libroAEliminar);

                borrado = true;
            }

            return borrado;
        }
    }
}