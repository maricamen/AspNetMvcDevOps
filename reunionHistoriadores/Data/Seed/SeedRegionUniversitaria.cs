using reunionhistoriadores2025.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reunionhistoriadores2025.Data.Seed
{
    public class SeedRegionUniversitaria : IEntityTypeConfiguration<RegionUniversitaria>
    {
        public void Configure(EntityTypeBuilder<RegionUniversitaria> builder)
        {
            builder.HasData(
                new RegionUniversitaria
                {
                    Id = 1,
                    Nombre = "Xalapa",
                    Orden = 1
                },
                new RegionUniversitaria
                {
                    Id = 2,
                    Nombre = "Veracruz",
                    Orden = 2
                },
                new RegionUniversitaria
                {
                    Id = 3,
                    Nombre = "Poza Rica - Tuxpan",
                    Orden = 3
                },
                new RegionUniversitaria
                {
                    Id = 4,
                    Nombre = "Orizaba - Córdoba",
                    Orden = 4
                },
                new RegionUniversitaria
                {
                    Id = 5,
                    Nombre = "Coatzacoalcos - Minatitlán",
                    Orden = 5
                }
            );
        }
    }
}
