using reunionhistoriadores2025.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reunionhistoriadores2025.Data.Seed
{
    public class SeedArchivoTipo : IEntityTypeConfiguration<ArchivoTipo>
    {
        public void Configure(EntityTypeBuilder<ArchivoTipo> builder)
        {
            builder.HasData(
                new ArchivoTipo
                {
                    Id = TipoDocumento.Id,
                    Nombre = TipoDocumento.Nombre,
                    IconoCss = TipoDocumento.IconoCss,
                },
                new ArchivoTipo
                {
                    Id = TipoImagen.Id,
                    Nombre = TipoImagen.Nombre,
                    IconoCss = TipoImagen.IconoCss,
                },
                new ArchivoTipo
                {
                    Id = TipoPresentacion.Id,
                    Nombre = TipoPresentacion.Nombre,
                    IconoCss = TipoPresentacion.IconoCss,
                },
                new ArchivoTipo
                {
                    Id = TipoPago.Id,
                    Nombre = TipoPago.Nombre,
                    IconoCss = TipoPago.IconoCss,
                },
                new ArchivoTipo
                {
                    Id = TipoCartaAceptacion.Id,
                    Nombre = TipoCartaAceptacion.Nombre,
                    IconoCss = TipoCartaAceptacion.IconoCss,
                },
                new ArchivoTipo
                {
                    Id = TipoIdentificacion.Id,
                    Nombre = TipoIdentificacion.Nombre,
                    IconoCss = TipoIdentificacion.IconoCss,
                }
            );
        }
    }
}
