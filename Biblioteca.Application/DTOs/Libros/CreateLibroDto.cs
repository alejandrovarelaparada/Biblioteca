namespace Biblioteca.Application.DTOs.Libros
{
    public record CreateLibroDto
    (
        string Titulo,
        string Sinopsis,
        int AutorId
    );
}