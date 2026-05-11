namespace Biblioteca.Application.DTOs.Libros
{
    public record LibroDto
    (
        int LibroId,
        string Titulo,
        string Sinopsis,
        int AutorId,
        string NombreAutor
    );
}