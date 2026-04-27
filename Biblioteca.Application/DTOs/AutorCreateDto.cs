using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Application.DTOs
{
    public class AutorCreateDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
    }
}