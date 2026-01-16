using reunionhistoriadores2025.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reunionhistoriadores2025.Data.Seed
{
    public class SeedRegistroFolio : IEntityTypeConfiguration<RegistroFolio>
    {
        private readonly string _version;

        public SeedRegistroFolio(string Version)
        {
            _version = Version;
        }

        public void Configure(EntityTypeBuilder<RegistroFolio> builder)
        {
            builder.HasData(
                new RegistroFolio
                {
                    Id = 0,
                    Clave = $"POR ASIGNAR",
                    Usada = false
                }
            );

            for (int i = 1; i <= 1500; i++)
            {

                builder.HasData(
                new RegistroFolio
                {
                    Id = i,
                    Clave = $"{_version}{i:D4}",
                    Usada = false
                }
            );
            }
        }
    }
}
