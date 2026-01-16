using reunionhistoriadores2025.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reunionhistoriadores2025.Data.Seed
{
    public class SeedRegistroTipo : IEntityTypeConfiguration<RegistroTipo>
    {
        public void Configure(EntityTypeBuilder<RegistroTipo> builder)
        {
            int i = 1;

            builder.HasData(
                new RegistroTipo
                {
                    Id = i++,
                    Nombre = "Reunión Internacional",
                    Descripcion = "XVII Reunión Internacional de Historiadores de México 2025",
                    FechaCierre = Convert.ToDateTime("16/09/2026 23:59"),
                    ClaseCss = "text-white bg-morado",
                    IconoCss = "bi bi-people-fill",
                    MaxMesas = 3
                }
            );
        }
    }
}
