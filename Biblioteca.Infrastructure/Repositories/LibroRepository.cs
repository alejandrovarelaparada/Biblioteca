using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly BibliotecaDbContext _context;

        public LibroRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Libro>> ObtenerLibrosAsync()
        {
            return await _context.Libros.Include(l => l.Autor).ToListAsync();
        }

        public async Task<Libro?> ObtenerLibroPorIdAsync(int libroId)
        {
            return await _context.Libros.Include(l => l.Autor).FirstOrDefaultAsync(l => l.Id == libroId);
        }

        public async Task InsertarNuevoLibroAsync(Libro libro)
        {
            await _context.Libros.AddAsync(libro);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarLibroAsync(Libro libroExistente)
        {
            _context.Libros.Update(libroExistente);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarLibroAsync(Libro libroAEliminar)
        {
            _context.Libros.Remove(libroAEliminar);
            await _context.SaveChangesAsync();
        }
    }
}