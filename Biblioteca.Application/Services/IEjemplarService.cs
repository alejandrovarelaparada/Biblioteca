using Biblioteca.Application.DTOs.Ejemplares;

namespace Biblioteca.Application.Services
{
    public interface IEjemplarService
    {
        Task<IEnumerable<EjemplarDto>> ListarEjemplaresAsync();
        Task<EjemplarDto?> BuscarEjemplarPorIdAsync(int ejemplarId);
        Task<EjemplarDto> RegistrarEjemplarAsync(CreateEjemplarDto nuevoEjemplar);
        Task<bool> ModificarDatosEjemplarAsync(int ejemplarId, UpdateEjemplarDto datosNuevos);
        Task<bool> BorrarEjemplarAsync(int ejemplarId);
    }
}