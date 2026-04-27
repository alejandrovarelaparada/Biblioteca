using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Services
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorResponseDto>> ListarAutoresAsync();
        Task<AutorResponseDto> RegistrarAutorAsync(AutorCreateDto nuevoAutor);
        Task<AutorResponseDto?> BuscarAutorPorIdAsync(int autorId);
        Task<bool> ModificarDatosAutorAsync(int autorId, AutorCreateDto datosNuevos);
        Task<bool> BorrarAutorAsync(int autorId);
    }
}