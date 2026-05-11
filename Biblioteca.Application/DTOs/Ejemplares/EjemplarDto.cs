namespace Biblioteca.Application.DTOs.Ejemplares
{
    public record EjemplarDto
    (
        int EjemplarId,
        string ISBN,
        int Edicion,
        string Estado,
        int LibroId,
        string TituloLibro
    );
}