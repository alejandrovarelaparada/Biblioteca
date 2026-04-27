using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ObtenerAutoresAsync();
        Task<Autor?> ObtenerAutorPorIdAsync(int autorId);
        Task InsertarNuevoAutorAsync(Autor nuevoAutor);
        Task ActualizarAutorAsync(Autor autorExistente);
        Task EliminarAutorAsync(Autor autorAEliminar);
    }
}