using Biblioteca.Application.DTOs.Libros;
using Biblioteca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService _libroService;

        public LibrosController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDto>>> ObtenerLibros()
        {
            var libros = await _libroService.ListarLibrosAsync();
            return Ok(libros);
        }

        [HttpPost]
        public async Task<ActionResult<LibroDto>> CrearLibro(CreateLibroDto libroCreado)
        {
            var nuevoLibro = await _libroService.RegistrarLibroAsync(libroCreado);
            return CreatedAtAction(nameof(ObtenerLibros), new { id = nuevoLibro.LibroId }, nuevoLibro);
        }

        [HttpGet("{libroId}")]
        public async Task<ActionResult<LibroDto>> ObtenerLibroPorId(int libroId)
        {
            var libro = await _libroService.BuscarLibroPorIdAsync(libroId);
            if (libro == null) return NotFound($"No se encontró el libro con ID {libroId}");

            return Ok(libro);
        }

        [HttpPut("{libroId}")]
        public async Task<IActionResult> ActualizarLibro(int libroId, UpdateLibroDto datosActualizados)
        {
            var libroActualizado = await _libroService.ModificarDatosLibroAsync(libroId, datosActualizados);
            if (!libroActualizado) return NotFound($"No se pudo actualizar porque el libro no existe");

            return NoContent();
        }

        [HttpDelete("{libroId}")]
        public async Task<IActionResult> BorrarLibro(int libroId)
        {
            var libroBorrado = await _libroService.BorrarLibroAsync(libroId);
            if (!libroBorrado) return NotFound($"No se pudo eliminar porque el libro no existe");

            return Ok(new { message = $"Libro con ID {libroId} eliminado correctamente"});
        }
    }
}