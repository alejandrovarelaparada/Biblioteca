using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        public LibroRepository(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        public async Task<IEnumerable<Libro>> ObtenerLibrosAsync()
        {
            return await _bibliotecaDbContext.Libros.Include(l => l.Autor).ToListAsync();
        }

        public async Task<Libro?> ObtenerLibroPorIdAsync(int libroId)
        {
            return await _bibliotecaDbContext.Libros.Include(l => l.Autor).FirstOrDefaultAsync(l => l.Id == libroId);
        }

        public async Task InsertarNuevoLibroAsync(Libro libro)
        {
            await _bibliotecaDbContext.Libros.AddAsync(libro);
            await _bibliotecaDbContext.SaveChangesAsync();
        }

        public async Task ActualizarLibroAsync(Libro libroExistente)
        {
            _bibliotecaDbContext.Libros.Update(libroExistente);
            await _bibliotecaDbContext.SaveChangesAsync();
        }

        public async Task EliminarLibroAsync(Libro libroAEliminar)
        {
            _bibliotecaDbContext.Libros.Remove(libroAEliminar);
            await _bibliotecaDbContext.SaveChangesAsync();
        }
    }
}