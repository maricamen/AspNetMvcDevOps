namespace reunionhistoriadores2025.Models
{
    public class Expositor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string GradoAcademico { get; set; }
        public int? GeneroId { get; set; }
        public int? PaisId { get; set; }
        public string Institucion { get; set; }
        public string Correo { get; set; }
        public Genero Genero { get; set; }
        public Pais Paises { get; set; }
        //public string? Autodescripcion { get; set;}
    }
}
