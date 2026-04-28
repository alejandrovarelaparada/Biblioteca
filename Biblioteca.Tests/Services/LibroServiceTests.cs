using Biblioteca.Application.Services;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Biblioteca.Tests.Services
{
    public class LibroServiceTests
    {
        private readonly Mock<ILibroRepository> _libroRepoMock;
        private readonly LibroService _libroService;

        public LibroServiceTests()
        {
            _libroRepoMock = new Mock<ILibroRepository>();
            _libroService = new LibroService(_libroRepoMock.Object);
        }

        [Fact]
        public async Task BuscarLibroPorIdAsync_SiElLibroExiste_DebeDevolverLibroResponseDto()
        {
            var libroId = 1;
            var libroPrueba = new Libro
            {
                Id = libroId,
                Titulo = "Libro de prueba",
                Sinopsis = "Esto es una prueba",
                Autor = new Autor { Nombre = "Autor de prueba" }
            };

            _libroRepoMock.Setup(repo => repo.ObtenerLibroPorIdAsync(libroId)).ReturnsAsync(libroPrueba);

            var resultado = await _libroService.BuscarLibroPorIdAsync(libroId);

            resultado.Should().NotBeNull();
            resultado.Titulo.Should().Be("Libro de prueba");
            resultado.NombreAutor.Should().Be("Autor de prueba");

            _libroRepoMock.Verify(repo => repo.ObtenerLibroPorIdAsync(libroId), Times.Once);
        }

        [Fact]
        public async Task BuscarLibroPorIdAsync_SiElLibroNoExiste_DebeDevolverNull()
        {
            _libroRepoMock.Setup(repo => repo.ObtenerLibroPorIdAsync(It.IsAny<int>())).ReturnsAsync((Libro?)null);

            var resultado = await _libroService.BuscarLibroPorIdAsync(999);

            resultado.Should().BeNull();
        }

        [Fact]
        public async Task BorrarLibroAsync_SiElLibroNoExiste_DebeDevolverFalse()
        {
            int idInexistente = 99;

            _libroRepoMock.Setup(repo => repo.ObtenerLibroPorIdAsync(idInexistente)).ReturnsAsync((Libro?)null);

            var resultado = await _libroService.BorrarLibroAsync(idInexistente);

            resultado.Should().BeFalse();

            _libroRepoMock.Verify(repo => repo.EliminarLibroAsync(It.IsAny<Libro>()), Times.Never);
        }
    }
}
