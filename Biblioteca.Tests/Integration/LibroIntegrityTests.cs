using Biblioteca.Domain.Entities;
using Biblioteca.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Tests.Integration
{
    public class LibroIntegrityTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _databaseEnvironment;

        public LibroIntegrityTests(TestDatabaseFixture databaseEnvironment)
        {
            _databaseEnvironment = databaseEnvironment;
        }

        [Fact]
        public async Task InsertarLibro_SinAutorExistente_DebeLanzarExcepcion()
        {
            using var database = _databaseEnvironment.CreateContext();
            var libroRepo = new LibroRepository(database);

            var libroSinAutorExistente = new Libro
            {
                Titulo = "Libro sin autor",
                Sinopsis = "Debe lanzar una excepción.",
                AutorId = 999
            };
            
            Func<Task> accion = async () => await libroRepo.InsertarNuevoLibroAsync(libroSinAutorExistente);

            await accion.Should().ThrowAsync<DbUpdateException>();
        }

        [Fact]
        public async Task EliminarLibro_BorraLibrosAsociados()
        {
            using var database = _databaseEnvironment.CreateContext();
            var autorRepo = new AutorRepository(database);
            var libroRepo = new LibroRepository(database);

            var autor = new Autor
            {
                Nombre = "Autor para borrar",
                Nacionalidad = "Prueba"
            };
            await autorRepo.InsertarNuevoAutorAsync(autor);

            var libro = new Libro
            {
                Titulo = "Libro para borrar",
                Sinopsis = "Esto debe borrarse",
                AutorId = autor.Id
            };
            await libroRepo.InsertarNuevoLibroAsync(libro);

            database.Autores.Remove(autor);
            await database.SaveChangesAsync(TestContext.Current.CancellationToken);

            var libroEnDb = await database.Libros.FirstOrDefaultAsync(l => l.Titulo == "Libro para borrar", cancellationToken: TestContext.Current.CancellationToken);

            libroEnDb.Should().BeNull();
        }
    }
}