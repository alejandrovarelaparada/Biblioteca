using Biblioteca.Application.DTOs.Autores;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<IEnumerable<AutorDto>> ListarAutoresAsync()
        {
            var autores = await _autorRepository.ObtenerAutoresAsync();

            return autores.Select(autor => new AutorDto(
                autor.Id,
                autor.Nombre,
                autor.Nacionalidad
            ));
        }

        public async Task<AutorDto?> BuscarAutorPorIdAsync(int autorId)
        {
            var autor = await _autorRepository.ObtenerAutorPorIdAsync(autorId);
            AutorDto? autorEncontrado = null;

            if (autor != null)
            {
                autorEncontrado = new AutorDto(
                    autor.Id,
                    autor.Nombre,
                    autor.Nacionalidad
                );
            }

            return autorEncontrado;
        }

        public async Task<AutorDto> RegistrarAutorAsync(CreateAutorDto autorCreado)
        {
            var nuevoAutor = new Autor
            {
                Nombre = autorCreado.Nombre,
                Nacionalidad = autorCreado.Nacionalidad
            };

            await _autorRepository.InsertarNuevoAutorAsync(nuevoAutor);

            return new AutorDto(nuevoAutor.Id, nuevoAutor.Nombre, nuevoAutor.Nacionalidad); 
        }

        public async Task<bool> ModificarDatosAutorAsync(int autorId, UpdateAutorDto datosNuevos)
        {
            var autorExistente = await _autorRepository.ObtenerAutorPorIdAsync(autorId);
            bool modificado = false;

            if (autorExistente != null)
            {
                autorExistente.Nombre = datosNuevos.Nombre;
                autorExistente.Nacionalidad = datosNuevos.Nacionalidad;

                await _autorRepository.ActualizarAutorAsync(autorExistente);

                modificado = true;
            }

            return modificado;
        }

        public async Task<bool> BorrarAutorAsync(int autorId)
        {
            var autorAEliminar = await _autorRepository.ObtenerAutorPorIdAsync(autorId);
            bool borrado = false;

            if (autorAEliminar != null)
            {
                await _autorRepository.EliminarAutorAsync(autorAEliminar);

                borrado = true;
            }

            return borrado;
        }
    }
}