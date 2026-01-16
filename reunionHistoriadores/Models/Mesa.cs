namespace reunionhistoriadores2025.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CoordinadorNombre { get; set; }
        public string CoordinadorCorreo { get; set; }
        public int? LineaTematicaId { get; set; }
        public string? LineaTematicaOtro { get; set; }
        public int RegistroId { get; set; }
        public int? SimposioId { get; set; }//agregado
        public bool Visible { get; set; }
        public Registro Regitros { get; set; }
        public LineaTematica LineaTematica { get; set; }
        public ICollection<Archivo>? Archivos { get; set; }
        public Simposio? Simposios { get; set; } 
        public ICollection<Ponencia>? Ponencias { get; set; }
    }
}
