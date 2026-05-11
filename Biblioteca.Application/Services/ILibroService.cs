using Biblioteca.Application.DTOs.Libros;

namespace Biblioteca.Application.Services
{
    public interface ILibroService
    {
        Task<IEnumerable<LibroDto>> ListarLibrosAsync();
        Task<LibroDto?> BuscarLibroPorIdAsync(int libroId);
        Task<LibroDto> RegistrarLibroAsync(CreateLibroDto nuevoLibro);
        Task<bool> ModificarDatosLibroAsync(int libroId, UpdateLibroDto datosNuevos);
        Task<bool> BorrarLibroAsync(int libroId);
    }
}