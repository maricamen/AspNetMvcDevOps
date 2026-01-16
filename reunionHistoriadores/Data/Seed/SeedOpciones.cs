using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace reunionhistoriadores2025.Data.Seed
{
    public class SeedOpciones : IEntityTypeConfiguration<Opciones>
    {
        public void Configure(EntityTypeBuilder<Opciones> builder)
        {
            GlobalOptions options;

            int i = 1;
            builder.HasData(

                // Generales
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.SiteTitle),
                    Valor = "XVII Reunión Internacional de Historiadores de México 2025"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.SiteSubTitle),
                    Valor = "Reunión Internacional"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.SiteShortTitle),
                    Valor = "La crisis en la historia de México"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.CorreoEvento),
                    Valor = "comiteiihs@uv.mx"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.HabilitaProduccion),
                    Valor = "false"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.HabilitaJurado),
                    Valor = "true"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.HabilitaJuradoLineaTematica),
                    Valor = "false"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.HabilitaIdioma),
                    Valor = "false"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.HabilitaArchivoAdicional),
                    Valor = "false"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.HabilitaPago),
                    Valor = "false"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.UrlPortal),
                    Valor = "https://www.uv.mx/xvii-reunion-historiadores/convocatoria-call-for-papers/"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.FechaCierre),
                    Valor = "16/09/2026 23:59"
                },

                // AD y Correo
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.LdapUrlString),
                    Valor = "LDAP://148.226.12.10"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.LdapUser),
                    Valor = "portales@uv.mx"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.LdapPwd),
                    Valor = "kfam732"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.SmtpServer),
                    Valor = "smtp.office365.com"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.SmtpUser),
                    Valor = "portales@uv.mx"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.SmtpPwd),
                    Valor = "kfam732"
                },
                new Opciones
                {
                    Id = i++,
                    Nombre = nameof(options.PaginacionSize),
                    Valor = "20"
                }
            );
        }
    }
}
