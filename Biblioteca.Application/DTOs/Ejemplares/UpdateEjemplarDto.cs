namespace Biblioteca.Application.DTOs.Ejemplares
{
    public record UpdateEjemplarDto
    (
        int EjemplarId,
        string ISBN,
        int Edicion,
        string Estado,
        int LibroId
    );
}