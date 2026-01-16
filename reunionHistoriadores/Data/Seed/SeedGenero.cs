using reunionhistoriadores2025.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reunionhistoriadores2025.Data.Seed
{
    public class SeedGenero : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.HasData(
                new Genero
                {
                    Id = 1,
                    Nombre = "Mujer"
                },
                new Genero
                {
                    Id = 2,
                    Nombre = "Hombre"
                },
                new Genero
                {
                    Id = 3,
                    Nombre = "No binarie"
                },
                new Genero
                {
                    Id = 4,
                    Nombre = "Prefiero no decir"
                },
                new Genero
                {
                    Id = 5,
                    Nombre = "Sin definir"
                }
            );
        }
    }
}
