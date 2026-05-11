namespace Biblioteca.Application.DTOs.Libros
{
    public record UpdateLibroDto
    (
        int LibroId,
        string Titulo,
        string Sinopsis,
        int AutorId
    );
}