namespace Biblioteca.Domain.Entities
{
    public class Prestamo
    {
        public int Id { get; set; }
        public DateTime FechaPrestamo  { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public bool Devuelto => FechaDevolucion.HasValue;

        public int EjemplarId { get; set; }
        public virtual Ejemplar? Ejemplar { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public Prestamo()
        {
            FechaPrestamo = DateTime.Today;
            FechaVencimiento = FechaPrestamo.AddDays(21);
        }

        public int CalcularDiasRetraso(DateTime fechaEvaluacion)
        {
            DateTime fechaEfectiva;

            if (FechaDevolucion.HasValue)
            {
                fechaEfectiva = FechaDevolucion.Value;
            }
            else
            {
                fechaEfectiva = fechaEvaluacion;
            }

            int diasRetraso = (fechaEfectiva - FechaVencimiento).Days;

            return Math.Max(0, diasRetraso);
        }
    }
}