using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        public AutorRepository(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        public async Task<IEnumerable<Autor>> ObtenerAutoresAsync()
        {
            return await _bibliotecaDbContext.Autores.ToListAsync();
        }

        public async Task<Autor?> ObtenerAutorPorIdAsync(int autorId)
        {
            return await _bibliotecaDbContext.Autores.FirstOrDefaultAsync(a => a.Id == autorId);
        }

        public async Task InsertarNuevoAutorAsync(Autor autor)
        {
            await _bibliotecaDbContext.Autores.AddAsync(autor);
            await _bibliotecaDbContext.SaveChangesAsync();
        }

        public async Task ActualizarAutorAsync(Autor autorExistente)
        {
            _bibliotecaDbContext.Autores.Update(autorExistente);
            await _bibliotecaDbContext.SaveChangesAsync();
        }

        public async Task EliminarAutorAsync(Autor autorAEliminar)
        {
            _bibliotecaDbContext.Autores.Remove(autorAEliminar);
            await _bibliotecaDbContext.SaveChangesAsync();
        }
    }
}