using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories
{
    public class EjemplarRepository : IEjemplarRepository
    {
        private readonly BibliotecaDbContext _bibliotecaDbContext;

        public EjemplarRepository(BibliotecaDbContext bibliotecaDbContext)
        {
            _bibliotecaDbContext = bibliotecaDbContext;
        }

        public async Task<IEnumerable<Ejemplar>> ObtenerEjemplaresAsync()
        {
            return await _bibliotecaDbContext.Ejemplares.Include(e => e.Libro).ToListAsync();
        }

        public async Task<Ejemplar?> ObtenerEjemplarPorIdAsync(int ejemplarId)
        {
            return await _bibliotecaDbContext.Ejemplares.Include(e => e.Libro).FirstOrDefaultAsync(ejemplar => ejemplar.Id == ejemplarId);
        }

        public async Task InsertarEjemplarAsync(Ejemplar ejemplar)
        {
            await _bibliotecaDbContext.Ejemplares.AddAsync(ejemplar);
            await _bibliotecaDbContext.SaveChangesAsync();
        }

        public async Task ActualizarEjemplarAsync(Ejemplar ejemplarExistente)
        {
            _bibliotecaDbContext.Ejemplares.Update(ejemplarExistente);
            await _bibliotecaDbContext.SaveChangesAsync();
        }

        public async Task EliminarEjemplarAsync(Ejemplar ejemplarAEliminar)
        {
            _bibliotecaDbContext.Ejemplares.Remove(ejemplarAEliminar);
            await _bibliotecaDbContext.SaveChangesAsync();
        }
    }
}