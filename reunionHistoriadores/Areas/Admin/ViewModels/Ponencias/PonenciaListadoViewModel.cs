namespace reunionhistoriadores2025.Areas.Admin.ViewModels.Ponencias
{
    public class PonenciaListadoViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ExpositorNombre { get; set; }
        public string ExpositorCorreo { get; set; }
        public string LineaTematica { get; set; }
        public string ArchivoNombre { get; set; }
        public string Fecha { get; set; }
        public string Folio { get; set; }
        public int? ExpositorId { get; set; }
    }
}
