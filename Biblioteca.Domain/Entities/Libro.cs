namespace Biblioteca.Domain.Entities
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Sinopsis { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public virtual Autor? Autor { get; set; }

        public virtual ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();
    }
}