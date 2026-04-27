using Biblioteca.Application.DTOs;
using Biblioteca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorResponseDto>>> ObtenerAutores()
        {
            var autores = await _autorService.ListarAutoresAsync();
            return Ok(autores);
        }

        [HttpPost]
        public async Task<ActionResult<AutorResponseDto>> CrearAutor(AutorCreateDto autorCreado)
        {
            var nuevoAutor = await _autorService.RegistrarAutorAsync(autorCreado);
            return CreatedAtAction(nameof(ObtenerAutores), new { id = nuevoAutor.AutorId }, nuevoAutor);
        }

        [HttpGet("{autorId}")]
        public async Task<ActionResult<AutorResponseDto>> ObtenerAutorPorId(int autorId)
        {
            var autor = await _autorService.BuscarAutorPorIdAsync(autorId);
            if (autor == null) return NotFound($"No se encontró el autor con ID {autorId}");

            return Ok(autor);
        }

        [HttpPut("{autorId}")]
        public async Task<IActionResult> ActualizarAutor(int autorId, AutorCreateDto datosActualizados)
        {
            var autorActualizado = await _autorService.ModificarDatosAutorAsync(autorId, datosActualizados);
            if (!autorActualizado) return NotFound($"No se pudo actualizar porque el autor no existe");

            return NoContent();
        }

        [HttpDelete("{autorId}")]
        public async Task<IActionResult> BorrarAutor(int autorId)
        {
            var autorBorrado = await _autorService.BorrarAutorAsync(autorId);
            if (!autorBorrado) return NotFound($"No se pudo eliminar porque el autor no existe");

            return Ok(new { message = $"Autor con ID {autorId} eliminado correctamente" });
        }
    }
}