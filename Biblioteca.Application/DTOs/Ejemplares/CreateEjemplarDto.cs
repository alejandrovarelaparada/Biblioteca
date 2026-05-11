namespace Biblioteca.Application.DTOs.Ejemplares
{
    public record CreateEjemplarDto
    (
        string ISBN,
        int Edicion,
        string Estado,
        int LibroId
    );
}