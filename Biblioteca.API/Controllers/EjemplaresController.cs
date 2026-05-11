using Biblioteca.Application.DTOs.Ejemplares;
using Biblioteca.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EjemplaresController : ControllerBase
    {
        private readonly IEjemplarService _ejemplarService;

        public EjemplaresController(IEjemplarService ejemplarService)
        {
            _ejemplarService = ejemplarService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EjemplarDto>>> ObtenerEjemplares()
        {
            var ejemplares = await _ejemplarService.ListarEjemplaresAsync();
            return Ok(ejemplares);
        }

        [HttpGet("{ejemplarId}")]
        public async Task<ActionResult<EjemplarDto>> ObtenerEjemplarPorId(int ejemplarId)
        {
            var ejemplar = await _ejemplarService.BuscarEjemplarPorIdAsync(ejemplarId);
            if (ejemplar == null) return NotFound($"No se encontró el ejemplar con ID {ejemplarId}");

            return Ok(ejemplar);
        }

        [HttpPost]
        public async Task<ActionResult<EjemplarDto>> CrearEjemplar(CreateEjemplarDto ejemplarCreado)
        {
            var nuevoEjemplar = await _ejemplarService.RegistrarEjemplarAsync(ejemplarCreado);
            return CreatedAtAction(nameof(ObtenerEjemplares), new { id = nuevoEjemplar.EjemplarId }, nuevoEjemplar);
        }

        [HttpPut("{ejemplarId}")]
        public async Task<IActionResult> ActualizarEjemplar(int ejemplarId, UpdateEjemplarDto datosActualizados)
        {
            var ejemplarActualizado = await _ejemplarService.ModificarDatosEjemplarAsync(ejemplarId, datosActualizados);
            if (!ejemplarActualizado) return NotFound($"No se pudo actualizar porque el ejemplar no existe");

            return NoContent();
        }

        [HttpDelete("{ejemplarId}")]
        public async Task<IActionResult> BorrarEjemplar(int ejemplarId)
        {
            var ejemplarBorrado = await _ejemplarService.BorrarEjemplarAsync(ejemplarId);
            if (!ejemplarBorrado) return NotFound($"No se pudo eliminar porque el ejemplar no existe");

            return Ok(new { message = $"ejemplar con ID {ejemplarId} eliminado correctamente" });
        }
    }
}