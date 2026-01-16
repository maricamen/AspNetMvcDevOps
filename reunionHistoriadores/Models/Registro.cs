namespace reunionhistoriadores2025.Models
{
    public class Registro
    {
        public int Id { get; set; }
        public int RegistroFolioId { get; set; }
        public int RegistroStatusId { get; set; }
        public int RegistroTipoId { get; set; }
        public string IpAddress { get; set; }
        public DateTime Fecha { get; set; }
        public string Correo { get; set; }
        public int ExposicionTipo {  get; set; }
        public RegistroFolio RegistroFolio { get; set; }
        public RegistroStatus RegistroStatus { get; set; }
        public RegistroTipo RegistroTipo { get; set; }

    }
}
