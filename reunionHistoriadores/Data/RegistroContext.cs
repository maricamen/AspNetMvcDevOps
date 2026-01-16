using System;
using System.Collections.Generic;
using reunionhistoriadores2025.Data.Seed;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace reunionhistoriadores2025.Data
{
    public class RegistroContext : IdentityDbContext<CustomIdentityUser>
    {
        private readonly IConfiguration Configuration;

        public RegistroContext(DbContextOptions<RegistroContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<LineaTematica> LineasTematicas { get; set; }
        public DbSet<Opciones> Opciones { get; set; }
        public DbSet<RegionUniversitaria> RegionesUniversitarias { get; set; }
        public DbSet<RegistroFolio> RegistroFolios { get; set; }
        public DbSet<RegistroStatus> RegistroStatus { get; set; }
        public DbSet<RegistroTipo> RegistroTipos { get; set; }
        public DbSet<Bitacora> Bitacora { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<Ponencia> Ponencias { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Simposio> Simposios { get; set; }
        public DbSet<Expositor> Expositores { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pais> Paises { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string version = Configuration["Version"];

            // Cambia el nombre de las tablas en BD
            modelBuilder.Entity<Archivo>().ToTable($"{Configuration["Version"]}_{nameof(Archivo)}");
            modelBuilder.Entity<ArchivoTipo>().ToTable($"{Configuration["Version"]}_{nameof(ArchivoTipo)}");
            modelBuilder.Entity<LineaTematica>().ToTable($"{Configuration["Version"]}_{nameof(LineaTematica)}");
            //modelBuilder.Entity<Modalidad>().ToTable($"{Configuration["Version"]}_{nameof(Modalidad)}");
            modelBuilder.Entity<RegionUniversitaria>().ToTable($"{Configuration["Version"]}_{nameof(RegionUniversitaria)}");
            modelBuilder.Entity<Opciones>().ToTable($"{Configuration["Version"]}_{nameof(Opciones)}");
            modelBuilder.Entity<RegistroFolio>().ToTable($"{Configuration["Version"]}_{nameof(RegistroFolio)}");
            modelBuilder.Entity<RegistroStatus>().ToTable($"{Configuration["Version"]}_{nameof(RegistroStatus)}");
            modelBuilder.Entity<RegistroTipo>().ToTable($"{Configuration["Version"]}_{nameof(RegistroTipo)}");
            modelBuilder.Entity<Bitacora>().ToTable($"{Configuration["Version"]}_{nameof(Bitacora)}");
            modelBuilder.Entity<Registro>().ToTable($"{Configuration["Version"]}_{nameof(Registro)}");
            modelBuilder.Entity<Ponencia>().ToTable($"{Configuration["Version"]}_{nameof(Ponencia)}");
            modelBuilder.Entity<Mesa>().ToTable($"{Configuration["Version"]}_{nameof(Mesa)}");
            modelBuilder.Entity<Simposio>().ToTable($"{Configuration["Version"]}_{nameof(Simposio)}");
            modelBuilder.Entity<Expositor>().ToTable($"{Configuration["Version"]}_{nameof(Expositor)}");
            modelBuilder.Entity<Genero>().ToTable($"{Configuration["Version"]}_{nameof(Genero)}");
            modelBuilder.Entity<Pais>().ToTable($"{Configuration["Version"]}_{nameof(Pais)}");


            /// Se deja solo Registros para usar herencia Table-Per-Hierarchy (TPH)
            //modelBuilder.Entity<Registro>().ToTable($"{Configuration["Version"]}_Registros");
            // Se comentan para crear solo una tabla con el Discriminator y usar herencia Table-Per-Hierarchy (TPH)
            // Si dejan, se crea una tabla para cada clase. Se llama Table-Per-Type (TPT)
            //modelBuilder.Entity<Registro1>().ToTable($"{Configuration["Version"]}{nameof(Registro1)}");
            //modelBuilder.Entity<Registro2>().ToTable($"{Configuration["Version"]}{nameof(Registro2)}");
            //modelBuilder.Entity<Registro3>().ToTable($"{Configuration["Version"]}{nameof(Registro3)}");



            /// Tablas de Identity
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable($"{version}_AspNetRoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable($"{version}_AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable($"{version}_AspNetUserLogins");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable($"{version}_AspNetUserRoles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable($"{version}_AspNetUserTokens");
            modelBuilder.Entity<IdentityRole>().ToTable($"{version}_AspNetRoles");
            modelBuilder.Entity<CustomIdentityUser>().ToTable($"{version}_AspNetUsers");



            //*  Seed datos
            modelBuilder.SeedUserData();
            modelBuilder.ApplyConfiguration(new SeedArchivoTipo());
            modelBuilder.ApplyConfiguration(new SeedLineaTematica());
            modelBuilder.ApplyConfiguration(new SeedOpciones());
            modelBuilder.ApplyConfiguration(new SeedRegionUniversitaria());
            modelBuilder.ApplyConfiguration(new SeedRegistroStatus());
            modelBuilder.ApplyConfiguration(new SeedRegistroTipo());
            modelBuilder.ApplyConfiguration(new SeedRegistroFolio(version));
            modelBuilder.ApplyConfiguration(new SeedGenero());

        }
    }
}
