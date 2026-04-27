using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaDbContext _context;

        public AutorRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Autor>> ObtenerAutoresAsync()
        {
            return await _context.Autores.ToListAsync();
        }

        public async Task<Autor?> ObtenerAutorPorIdAsync(int autorId)
        {
            return await _context.Autores.FirstOrDefaultAsync(a => a.Id == autorId);
        }

        public async Task InsertarNuevoAutorAsync(Autor autor)
        {
            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAutorAsync(Autor autorExistente)
        {
            _context.Autores.Update(autorExistente);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAutorAsync(Autor autorAEliminar)
        {
            _context.Autores.Remove(autorAEliminar);
            await _context.SaveChangesAsync();
        }
    }
}