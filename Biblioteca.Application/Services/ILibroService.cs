using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Services
{
    public interface ILibroService
    {
        Task<IEnumerable<LibroResponseDto>> ListarLibrosAsync();
        Task<LibroResponseDto> RegistrarLibroAsync(LibroCreateDto nuevoLibro);
        Task<LibroResponseDto?> BuscarLibroPorIdAsync(int libroId);
        Task<bool> ModificarDatosLibroAsync(int libroId, LibroCreateDto datosNuevos);
        Task<bool> BorrarLibroAsync(int libroId);
    }
}