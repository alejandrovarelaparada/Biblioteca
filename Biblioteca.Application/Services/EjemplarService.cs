using Biblioteca.Application.DTOs.Ejemplares;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class EjemplarService : IEjemplarService
    {
        private readonly IEjemplarRepository _ejemplarRepository;

        public EjemplarService(IEjemplarRepository ejemplarRepository)
        {
            _ejemplarRepository = ejemplarRepository;
        }

        public async Task<IEnumerable<EjemplarDto>> ListarEjemplaresAsync()
        {
            var ejemplares = await _ejemplarRepository.ObtenerEjemplaresAsync();

            return ejemplares.Select(ejemplar => new EjemplarDto(
                ejemplar.Id,
                ejemplar.ISBN,
                ejemplar.Edicion,
                ejemplar.Estado,
                ejemplar.LibroId,
                ejemplar.Libro?.Titulo ?? "Sin título"));
        }

        public async Task<EjemplarDto?> BuscarEjemplarPorIdAsync(int ejemplarId)
        {
            var ejemplar = await _ejemplarRepository.ObtenerEjemplarPorIdAsync(ejemplarId);
            EjemplarDto? ejemplarEncontrado = null;

            if (ejemplar != null)
            {
                ejemplarEncontrado = new EjemplarDto(
                    ejemplar.Id,
                    ejemplar.ISBN,
                    ejemplar.Edicion,
                    ejemplar.Estado,
                    ejemplar.LibroId,
                    ejemplar.Libro?.Titulo ?? "Sin título"
                );
            }

            return ejemplarEncontrado;
        }

        public async Task<EjemplarDto> RegistrarEjemplarAsync(CreateEjemplarDto ejemplarCreado)
        {
            var nuevoEjemplar = new Ejemplar
            {
                ISBN = ejemplarCreado.ISBN,
                Edicion = ejemplarCreado.Edicion,
                Estado = ejemplarCreado.Estado,
                LibroId = ejemplarCreado.LibroId,
            };

            await _ejemplarRepository.InsertarEjemplarAsync(nuevoEjemplar);

            return new EjemplarDto(
                nuevoEjemplar.Id,
                nuevoEjemplar.ISBN,
                nuevoEjemplar.Edicion,
                nuevoEjemplar.Estado,
                nuevoEjemplar.LibroId,
                nuevoEjemplar.Libro?.Titulo ?? "Sin título");
        }

        public async Task<bool> ModificarDatosEjemplarAsync(int ejemplarId, UpdateEjemplarDto datosNuevos)
        {
            var ejemplarExistente = await _ejemplarRepository.ObtenerEjemplarPorIdAsync(ejemplarId);
            bool modificado = false;

            if (ejemplarExistente != null)
            {
                ejemplarExistente.ISBN = datosNuevos.ISBN;
                ejemplarExistente.Edicion = datosNuevos.Edicion;
                ejemplarExistente.Estado = datosNuevos.Estado;
                ejemplarExistente.LibroId = datosNuevos.LibroId;

                await _ejemplarRepository.ActualizarEjemplarAsync(ejemplarExistente);

                modificado = true;
            }

            return modificado;
        }

        public async Task<bool> BorrarEjemplarAsync(int ejemplarId)
        {
            var ejemplarAEliminar = await _ejemplarRepository.ObtenerEjemplarPorIdAsync(ejemplarId);
            bool borrado = false;

            if (ejemplarAEliminar != null)
            {
                await _ejemplarRepository.EliminarEjemplarAsync(ejemplarAEliminar);

                borrado = true;
            }

            return borrado;
        }
    }
}