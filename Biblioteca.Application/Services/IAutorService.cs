using Biblioteca.Application.DTOs.Autores;

namespace Biblioteca.Application.Services
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> ListarAutoresAsync();
        Task<AutorDto?> BuscarAutorPorIdAsync(int autorId);
        Task<AutorDto> RegistrarAutorAsync(CreateAutorDto nuevoAutor);
        Task<bool> ModificarDatosAutorAsync(int autorId, UpdateAutorDto datosNuevos);
        Task<bool> BorrarAutorAsync(int autorId);
    }
}