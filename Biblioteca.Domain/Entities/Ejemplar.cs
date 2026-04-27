namespace Biblioteca.Domain.Entities
{
    public class Ejemplar
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int Edicion { get; set; }
        public string Estado { get; set; } = "Disponible"; //Disponible, Prestado o Perdido

        public int LibroId { get; set; }
        public virtual Libro? Libro { get; set; }

        public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}