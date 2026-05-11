using Biblioteca.Application.DTOs.Libros;
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

        public async Task<IEnumerable<LibroDto>> ListarLibrosAsync()
        {
            var libros = await _libroRepository.ObtenerLibrosAsync();

            return libros.Select(libro => new LibroDto(
                libro.Id,
                libro.Titulo,
                libro.Sinopsis,
                libro.AutorId,
                libro.Autor?.Nombre ?? "Autor desconocido"));
        }

        public async Task<LibroDto?> BuscarLibroPorIdAsync(int libroId)
        {
            var libro = await _libroRepository.ObtenerLibroPorIdAsync(libroId);
            LibroDto? libroEncontrado = null;

            if (libro != null)
            {
                libroEncontrado = new LibroDto(
                    libro.Id, 
                    libro.Titulo, 
                    libro.Sinopsis,
                    libro.AutorId,
                    libro.Autor?.Nombre ?? "Autor desconocido");
            }

            return libroEncontrado;
        }

        public async Task<LibroDto> RegistrarLibroAsync(CreateLibroDto libroCreado)
        {
            var nuevoLibro = new Libro
            {
                Titulo = libroCreado.Titulo,
                Sinopsis = libroCreado.Sinopsis,
                AutorId = libroCreado.AutorId,
            };

            await _libroRepository.InsertarNuevoLibroAsync(nuevoLibro);

            return new LibroDto(nuevoLibro.Id, nuevoLibro.Titulo, nuevoLibro.Sinopsis, nuevoLibro.AutorId, nuevoLibro.Autor?.Nombre ?? "Autor desconocido");
        }

        public async Task<bool> ModificarDatosLibroAsync(int libroId, UpdateLibroDto datosNuevos)
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