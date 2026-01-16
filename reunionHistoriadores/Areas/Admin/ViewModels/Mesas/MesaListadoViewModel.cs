namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Mesas
{
    public class MesaListadoViewModel
    {
        public int Id { get; set; }
        public string NombreMesa { get; set; }
        public string CoordinadorNombre { get; set; }
        public string CoordinadorCorreo { get; set; }
        public string LineaTematica { get; set; }
        public string ArchivoNombre { get; set; }
        public string Fecha { get; set; }

        public int? PonenciaId { get; set; }
        public int? ExpositorId { get; set; }
    }
}
