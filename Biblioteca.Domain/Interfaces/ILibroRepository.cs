using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces
{
    public interface ILibroRepository
    {
        Task<IEnumerable<Libro>> ObtenerLibrosAsync();
        Task<Libro?> ObtenerLibroPorIdAsync(int libroId);
        Task InsertarNuevoLibroAsync(Libro nuevoLibro);
        Task ActualizarLibroAsync(Libro libroExistente);
        Task EliminarLibroAsync(Libro libroAEliminar);
    }
}