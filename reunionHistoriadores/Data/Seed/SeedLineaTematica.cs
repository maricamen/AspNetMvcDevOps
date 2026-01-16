using reunionhistoriadores2025.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reunionhistoriadores2025.Data.Seed
{
    public class SeedLineaTematica : IEntityTypeConfiguration<LineaTematica>
    {
        public void Configure(EntityTypeBuilder<LineaTematica> builder)
        {
            int i = 1;

            builder.HasData(
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Crisis económicas, fiscales y monetarias",
                    Orden = i++
                },
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Crisis políticas y estatales",
                    Orden = i++
                },
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Guerras, violencia y desplazamientos",
                    Orden = i++
                },
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Crisis ecológicas y climáticas",
                    Orden = i++
                },
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Salud, vivienda y vida cotidiana",
                    Orden = i++
                },
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Crisis en las comunidades indígenas y locales",
                    Orden = i++
                },
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Diversidad, género y sexualidades",
                    Orden = i++
                },
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Arte y cultura",
                    Orden = i++
                },
                new LineaTematica
                {
                    Id = i,
                    Nombre = "Otro",
                    Orden = i++
                }
            );
        }
    }
}
