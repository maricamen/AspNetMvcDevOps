using reunionhistoriadores2025.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reunionhistoriadores2025.Data.Seed
{
    public class SeedRegistroStatus : IEntityTypeConfiguration<RegistroStatus>
    {
        public void Configure(EntityTypeBuilder<RegistroStatus> builder)
        {
            builder.HasData(
                new RegistroStatus
                {
                    Id = (int)RegistroStatusId.RECIBIDO,
                    Nombre = "Recibido",
                    NombreLargo = "Recibidos",
                    ClaseCss = "bg-warning text-body",
                    Visible = true
                },
                new RegistroStatus
                {
                    Id = (int)RegistroStatusId.ACEPTADO,
                    Nombre = "Aceptado",
                    NombreLargo = "Aceptados",
                    ClaseCss = "bg-success text-white",
                    Visible = true
                },
                new RegistroStatus
                {
                    Id = (int)RegistroStatusId.RECHAZADO,
                    Nombre = "Rechazado",
                    NombreLargo = "Rechazados",
                    ClaseCss = "bg-danger text-white",
                    Visible = true
                },
                new RegistroStatus
                {
                    Id = (int)RegistroStatusId.ELIMINADO,
                    Nombre = "Eliminado",
                    NombreLargo = "Eliminados",
                    ClaseCss = "bg-secondary text-white",
                    Visible = true
                },
                new RegistroStatus
                {
                    Id = (int)RegistroStatusId.ELIMINADODEFINITIVAMENTE,
                    Nombre = "Eliminado definitivamente",
                    NombreLargo = "Eliminado definitivamente",
                    ClaseCss = "bg-dark text-white",
                    Visible = false
                }
            );
        }
    }
}
