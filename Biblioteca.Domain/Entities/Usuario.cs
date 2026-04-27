namespace Biblioteca.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}