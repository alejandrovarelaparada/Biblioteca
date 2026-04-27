namespace Biblioteca.Application.DTOs
{
    public class LibroCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Sinopsis { get; set; } = string.Empty;

        public int AutorId { get; set; }
    }
}