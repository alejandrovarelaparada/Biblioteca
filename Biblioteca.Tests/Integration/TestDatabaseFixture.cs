using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Tests.Integration
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BibliotecaDB_Tests;Trusted_Connection=True;TrustServerCertificate=True;";

        public BibliotecaDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<BibliotecaDbContext>().UseSqlServer(ConnectionString).Options;

            return new BibliotecaDbContext(options);
        }

        public TestDatabaseFixture()
        {
            using var setUpDatabase = CreateContext();

            setUpDatabase.Database.EnsureDeleted();
            setUpDatabase.Database.EnsureCreated();
        }
    }
}