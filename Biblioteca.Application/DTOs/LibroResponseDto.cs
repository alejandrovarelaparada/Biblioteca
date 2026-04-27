namespace Biblioteca.Application.DTOs
{
    public class LibroResponseDto
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Sinopsis { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public string NombreAutor { get; set; } = string.Empty;

    }
}