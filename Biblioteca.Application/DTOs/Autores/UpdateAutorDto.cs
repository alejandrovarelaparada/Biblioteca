namespace Biblioteca.Application.DTOs.Autores
{
    public record UpdateAutorDto
    (
        int AutorId,
        string Nombre,
        string Nacionalidad
    );
}