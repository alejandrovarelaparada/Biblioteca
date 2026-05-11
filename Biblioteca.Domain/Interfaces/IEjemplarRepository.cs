using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces
{
    public interface IEjemplarRepository
    {
        Task<IEnumerable<Ejemplar>> ObtenerEjemplaresAsync();
        Task<Ejemplar?> ObtenerEjemplarPorIdAsync(int ejemplarId);
        Task InsertarEjemplarAsync(Ejemplar nuevoEjemplar);
        Task ActualizarEjemplarAsync(Ejemplar ejemplarExistente);
        Task EliminarEjemplarAsync(Ejemplar ejemplarAEliminar);
    }
}