namespace reunionhistoriadores2025.Models
{
    public class Ponencia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? LineaTematicaOtro { get; set; }
        public int? LineaTematicaId { get; set; }
        public int ExpositorId {  get; set; }
        public int RegistroId { get; set; }
        public int? MesaId { get; set; }
        public int? SimposioId { get; set; }
        public bool Visible { get; set; }
        public LineaTematica LineaTematica { get; set; }
        public Expositor Expositores { get; set; }
        public Registro Regitros { get; set; }
        public Mesa? Mesas { get; set; }
        public Simposio? Simposios { get; set; }
        public ICollection<Archivo>? Archivos { get; set; }

    }
}
