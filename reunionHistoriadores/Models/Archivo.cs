using System.ComponentModel.DataAnnotations;

namespace reunionhistoriadores2025.Models
{
    public class Archivo
    {
        public int Id { get; set; }
        public int? PonenciaId { get; set; }
        public int? MesaId { get; set; }
        public int? SimposioId { get; set; }
        public int ArchivoTipoId { get; set; }

        public string Nombre { get; set; }
        public string Mime { get; set; }

        [Display(Name = "Tamaño")]
        public long Size { get; set; }
        public DateTime Fecha { get; set; }
        public byte[]? Contenido { get; set; }

        public ArchivoTipo ArchivoTipo { get; set; }
        public Ponencia Ponencia { get; set; }
        public Mesa Mesa { get; set; }
        public Simposio Simposio { get; set; }
    }
}
