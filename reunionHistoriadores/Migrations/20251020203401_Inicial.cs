using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace reunionHistoriadores2025.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RIH25_ArchivoTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconoCss = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_ArchivoTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombrecompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adscripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveDirectoryEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CardsViewEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Bitacora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Bitacora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_LineaTematica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_LineaTematica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Opciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Opciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Pais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_RegionUniversitaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_RegionUniversitaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_RegistroFolio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_RegistroFolio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_RegistroStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreLargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaseCss = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_RegistroStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_RegistroTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaseCss = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconoCss = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxMesas = table.Column<int>(type: "int", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_RegistroTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIH25_AspNetRoleClaims_RIH25_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RIH25_AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIH25_AspNetUserClaims_RIH25_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RIH25_AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_RIH25_AspNetUserLogins_RIH25_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RIH25_AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RIH25_AspNetUserRoles_RIH25_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RIH25_AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RIH25_AspNetUserRoles_RIH25_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RIH25_AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_RIH25_AspNetUserTokens_RIH25_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "RIH25_AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Expositor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradoAcademico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: true),
                    PaisId = table.Column<int>(type: "int", nullable: true),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autodescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Expositor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIH25_Expositor_RIH25_Genero_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "RIH25_Genero",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RIH25_Expositor_RIH25_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "RIH25_Pais",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Registro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistroFolioId = table.Column<int>(type: "int", nullable: false),
                    RegistroStatusId = table.Column<int>(type: "int", nullable: false),
                    RegistroTipoId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExposicionTipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Registro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIH25_Registro_RIH25_RegistroFolio_RegistroFolioId",
                        column: x => x.RegistroFolioId,
                        principalTable: "RIH25_RegistroFolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RIH25_Registro_RIH25_RegistroStatus_RegistroStatusId",
                        column: x => x.RegistroStatusId,
                        principalTable: "RIH25_RegistroStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RIH25_Registro_RIH25_RegistroTipo_RegistroTipoId",
                        column: x => x.RegistroTipoId,
                        principalTable: "RIH25_RegistroTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Simposio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinadorNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinadorGradoAcademico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinadorInstitucion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinadorCorreo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroId = table.Column<int>(type: "int", nullable: false),
                    LineaTematicaId = table.Column<int>(type: "int", nullable: true),
                    LineaTematicaOtro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Simposio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIH25_Simposio_RIH25_LineaTematica_LineaTematicaId",
                        column: x => x.LineaTematicaId,
                        principalTable: "RIH25_LineaTematica",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RIH25_Simposio_RIH25_Registro_RegistroId",
                        column: x => x.RegistroId,
                        principalTable: "RIH25_Registro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Mesa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinadorNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinadorCorreo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineaTematicaId = table.Column<int>(type: "int", nullable: true),
                    LineaTematicaOtro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistroId = table.Column<int>(type: "int", nullable: false),
                    SimposioId = table.Column<int>(type: "int", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Mesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIH25_Mesa_RIH25_LineaTematica_LineaTematicaId",
                        column: x => x.LineaTematicaId,
                        principalTable: "RIH25_LineaTematica",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RIH25_Mesa_RIH25_Registro_RegistroId",
                        column: x => x.RegistroId,
                        principalTable: "RIH25_Registro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RIH25_Mesa_RIH25_Simposio_SimposioId",
                        column: x => x.SimposioId,
                        principalTable: "RIH25_Simposio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Ponencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineaTematicaOtro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineaTematicaId = table.Column<int>(type: "int", nullable: true),
                    ExpositorId = table.Column<int>(type: "int", nullable: false),
                    RegistroId = table.Column<int>(type: "int", nullable: false),
                    MesaId = table.Column<int>(type: "int", nullable: true),
                    SimposioId = table.Column<int>(type: "int", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Ponencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIH25_Ponencia_RIH25_Expositor_ExpositorId",
                        column: x => x.ExpositorId,
                        principalTable: "RIH25_Expositor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RIH25_Ponencia_RIH25_LineaTematica_LineaTematicaId",
                        column: x => x.LineaTematicaId,
                        principalTable: "RIH25_LineaTematica",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RIH25_Ponencia_RIH25_Mesa_MesaId",
                        column: x => x.MesaId,
                        principalTable: "RIH25_Mesa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RIH25_Ponencia_RIH25_Registro_RegistroId",
                        column: x => x.RegistroId,
                        principalTable: "RIH25_Registro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RIH25_Ponencia_RIH25_Simposio_SimposioId",
                        column: x => x.SimposioId,
                        principalTable: "RIH25_Simposio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RIH25_Archivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PonenciaId = table.Column<int>(type: "int", nullable: true),
                    MesaId = table.Column<int>(type: "int", nullable: true),
                    SimposioId = table.Column<int>(type: "int", nullable: true),
                    ArchivoTipoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contenido = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RIH25_Archivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RIH25_Archivo_RIH25_ArchivoTipo_ArchivoTipoId",
                        column: x => x.ArchivoTipoId,
                        principalTable: "RIH25_ArchivoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RIH25_Archivo_RIH25_Mesa_MesaId",
                        column: x => x.MesaId,
                        principalTable: "RIH25_Mesa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RIH25_Archivo_RIH25_Ponencia_PonenciaId",
                        column: x => x.PonenciaId,
                        principalTable: "RIH25_Ponencia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RIH25_Archivo_RIH25_Simposio_SimposioId",
                        column: x => x.SimposioId,
                        principalTable: "RIH25_Simposio",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "RIH25_ArchivoTipo",
                columns: new[] { "Id", "IconoCss", "Nombre" },
                values: new object[,]
                {
                    { 1, "bi bi-file-earmark-text", "Participación" },
                    { 2, "bi bi-file-earmark-text", "Fotografía" },
                    { 3, "bi bi-file-earmark-text", "Presentación" },
                    { 4, "bi bi-cash-coin", "Pago" },
                    { 5, "bi bi-file-earmark-medical", "Aceptación" },
                    { 6, "bi bi-postcard", "Identificación" }
                });

            migrationBuilder.InsertData(
                table: "RIH25_AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a5f262fd-3650-4da3-bcfe-5fd05f0ef421", null, "Administrador General", "ADMINISTRADOR GENERAL" },
                    { "c2e5ce65-6337-4307-b4cc-ea1cc44a23ac", null, "Administrador", "ADMINISTRADOR" }
                });

            migrationBuilder.InsertData(
                table: "RIH25_AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ActiveDirectoryEnabled", "Adscripcion", "CardsViewEnabled", "ConcurrencyStamp", "Deleted", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nombrecompleto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9c324a46-edad-4aa3-a02a-63269ac9ebc7", 0, true, null, true, "e43f4364-4c46-4bd7-9333-523d2ac344f7", false, "maricperez@uv.mx", false, false, null, "Maricarmen Pérez Campos", "MARICPEREZ@UV.MX", "MARICPEREZ@UV.MX", null, null, false, "9e56e832-6135-4331-8589-8b835d4dd3a5", false, "maricperez@uv.mx" });

            migrationBuilder.InsertData(
                table: "RIH25_Genero",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Mujer" },
                    { 2, "Hombre" },
                    { 3, "No binarie" },
                    { 4, "Prefiero no decir" },
                    { 5, "Sin definir" }
                });

            migrationBuilder.InsertData(
                table: "RIH25_LineaTematica",
                columns: new[] { "Id", "Nombre", "Orden" },
                values: new object[,]
                {
                    { 1, "Crisis económicas, fiscales y monetarias", 1 },
                    { 2, "Crisis políticas y estatales", 2 },
                    { 3, "Guerras, violencia y desplazamientos", 3 },
                    { 4, "Crisis ecológicas y climáticas", 4 },
                    { 5, "Salud, vivienda y vida cotidiana", 5 },
                    { 6, "Crisis en las comunidades indígenas y locales", 6 },
                    { 7, "Diversidad, género y sexualidades", 7 },
                    { 8, "Arte y cultura", 8 },
                    { 9, "Otro", 9 }
                });

            migrationBuilder.InsertData(
                table: "RIH25_Opciones",
                columns: new[] { "Id", "Nombre", "Valor" },
                values: new object[,]
                {
                    { 1, "SiteTitle", "XVII Reunión Internacional de Historiadores de México 2025" },
                    { 2, "SiteSubTitle", "Reunión Internacional" },
                    { 3, "SiteShortTitle", "La crisis en la historia de México" },
                    { 4, "CorreoEvento", "comiteiihs@uv.mx" },
                    { 5, "HabilitaProduccion", "false" },
                    { 6, "HabilitaJurado", "true" },
                    { 7, "HabilitaJuradoLineaTematica", "false" },
                    { 8, "HabilitaIdioma", "false" },
                    { 9, "HabilitaArchivoAdicional", "false" },
                    { 10, "HabilitaPago", "false" },
                    { 11, "UrlPortal", "https://www.uv.mx/xvii-reunion-historiadores/" },
                    { 12, "FechaCierre", "16/01/2026 23:59" },
                    { 13, "LdapUrlString", "LDAP://148.226.12.10" },
                    { 14, "LdapUser", "portales@uv.mx" },
                    { 15, "LdapPwd", "kfam732" },
                    { 16, "SmtpServer", "smtp.office365.com" },
                    { 17, "SmtpUser", "portales@uv.mx" },
                    { 18, "SmtpPwd", "kfam732" },
                    { 19, "PaginacionSize", "20" }
                });

            migrationBuilder.InsertData(
                table: "RIH25_RegionUniversitaria",
                columns: new[] { "Id", "Nombre", "Orden" },
                values: new object[,]
                {
                    { 1, "Xalapa", 1 },
                    { 2, "Veracruz", 2 },
                    { 3, "Poza Rica - Tuxpan", 3 },
                    { 4, "Orizaba - Córdoba", 4 },
                    { 5, "Coatzacoalcos - Minatitlán", 5 }
                });

            migrationBuilder.InsertData(
                table: "RIH25_RegistroFolio",
                columns: new[] { "Id", "Clave", "Usada" },
                values: new object[,]
                {
                    { 0, "POR ASIGNAR", false },
                    { 1, "RIH250001", false },
                    { 2, "RIH250002", false },
                    { 3, "RIH250003", false },
                    { 4, "RIH250004", false },
                    { 5, "RIH250005", false },
                    { 6, "RIH250006", false },
                    { 7, "RIH250007", false },
                    { 8, "RIH250008", false },
                    { 9, "RIH250009", false },
                    { 10, "RIH250010", false },
                    { 11, "RIH250011", false },
                    { 12, "RIH250012", false },
                    { 13, "RIH250013", false },
                    { 14, "RIH250014", false },
                    { 15, "RIH250015", false },
                    { 16, "RIH250016", false },
                    { 17, "RIH250017", false },
                    { 18, "RIH250018", false },
                    { 19, "RIH250019", false },
                    { 20, "RIH250020", false },
                    { 21, "RIH250021", false },
                    { 22, "RIH250022", false },
                    { 23, "RIH250023", false },
                    { 24, "RIH250024", false },
                    { 25, "RIH250025", false },
                    { 26, "RIH250026", false },
                    { 27, "RIH250027", false },
                    { 28, "RIH250028", false },
                    { 29, "RIH250029", false },
                    { 30, "RIH250030", false },
                    { 31, "RIH250031", false },
                    { 32, "RIH250032", false },
                    { 33, "RIH250033", false },
                    { 34, "RIH250034", false },
                    { 35, "RIH250035", false },
                    { 36, "RIH250036", false },
                    { 37, "RIH250037", false },
                    { 38, "RIH250038", false },
                    { 39, "RIH250039", false },
                    { 40, "RIH250040", false },
                    { 41, "RIH250041", false },
                    { 42, "RIH250042", false },
                    { 43, "RIH250043", false },
                    { 44, "RIH250044", false },
                    { 45, "RIH250045", false },
                    { 46, "RIH250046", false },
                    { 47, "RIH250047", false },
                    { 48, "RIH250048", false },
                    { 49, "RIH250049", false },
                    { 50, "RIH250050", false },
                    { 51, "RIH250051", false },
                    { 52, "RIH250052", false },
                    { 53, "RIH250053", false },
                    { 54, "RIH250054", false },
                    { 55, "RIH250055", false },
                    { 56, "RIH250056", false },
                    { 57, "RIH250057", false },
                    { 58, "RIH250058", false },
                    { 59, "RIH250059", false },
                    { 60, "RIH250060", false },
                    { 61, "RIH250061", false },
                    { 62, "RIH250062", false },
                    { 63, "RIH250063", false },
                    { 64, "RIH250064", false },
                    { 65, "RIH250065", false },
                    { 66, "RIH250066", false },
                    { 67, "RIH250067", false },
                    { 68, "RIH250068", false },
                    { 69, "RIH250069", false },
                    { 70, "RIH250070", false },
                    { 71, "RIH250071", false },
                    { 72, "RIH250072", false },
                    { 73, "RIH250073", false },
                    { 74, "RIH250074", false },
                    { 75, "RIH250075", false },
                    { 76, "RIH250076", false },
                    { 77, "RIH250077", false },
                    { 78, "RIH250078", false },
                    { 79, "RIH250079", false },
                    { 80, "RIH250080", false },
                    { 81, "RIH250081", false },
                    { 82, "RIH250082", false },
                    { 83, "RIH250083", false },
                    { 84, "RIH250084", false },
                    { 85, "RIH250085", false },
                    { 86, "RIH250086", false },
                    { 87, "RIH250087", false },
                    { 88, "RIH250088", false },
                    { 89, "RIH250089", false },
                    { 90, "RIH250090", false },
                    { 91, "RIH250091", false },
                    { 92, "RIH250092", false },
                    { 93, "RIH250093", false },
                    { 94, "RIH250094", false },
                    { 95, "RIH250095", false },
                    { 96, "RIH250096", false },
                    { 97, "RIH250097", false },
                    { 98, "RIH250098", false },
                    { 99, "RIH250099", false },
                    { 100, "RIH250100", false },
                    { 101, "RIH250101", false },
                    { 102, "RIH250102", false },
                    { 103, "RIH250103", false },
                    { 104, "RIH250104", false },
                    { 105, "RIH250105", false },
                    { 106, "RIH250106", false },
                    { 107, "RIH250107", false },
                    { 108, "RIH250108", false },
                    { 109, "RIH250109", false },
                    { 110, "RIH250110", false },
                    { 111, "RIH250111", false },
                    { 112, "RIH250112", false },
                    { 113, "RIH250113", false },
                    { 114, "RIH250114", false },
                    { 115, "RIH250115", false },
                    { 116, "RIH250116", false },
                    { 117, "RIH250117", false },
                    { 118, "RIH250118", false },
                    { 119, "RIH250119", false },
                    { 120, "RIH250120", false },
                    { 121, "RIH250121", false },
                    { 122, "RIH250122", false },
                    { 123, "RIH250123", false },
                    { 124, "RIH250124", false },
                    { 125, "RIH250125", false },
                    { 126, "RIH250126", false },
                    { 127, "RIH250127", false },
                    { 128, "RIH250128", false },
                    { 129, "RIH250129", false },
                    { 130, "RIH250130", false },
                    { 131, "RIH250131", false },
                    { 132, "RIH250132", false },
                    { 133, "RIH250133", false },
                    { 134, "RIH250134", false },
                    { 135, "RIH250135", false },
                    { 136, "RIH250136", false },
                    { 137, "RIH250137", false },
                    { 138, "RIH250138", false },
                    { 139, "RIH250139", false },
                    { 140, "RIH250140", false },
                    { 141, "RIH250141", false },
                    { 142, "RIH250142", false },
                    { 143, "RIH250143", false },
                    { 144, "RIH250144", false },
                    { 145, "RIH250145", false },
                    { 146, "RIH250146", false },
                    { 147, "RIH250147", false },
                    { 148, "RIH250148", false },
                    { 149, "RIH250149", false },
                    { 150, "RIH250150", false },
                    { 151, "RIH250151", false },
                    { 152, "RIH250152", false },
                    { 153, "RIH250153", false },
                    { 154, "RIH250154", false },
                    { 155, "RIH250155", false },
                    { 156, "RIH250156", false },
                    { 157, "RIH250157", false },
                    { 158, "RIH250158", false },
                    { 159, "RIH250159", false },
                    { 160, "RIH250160", false },
                    { 161, "RIH250161", false },
                    { 162, "RIH250162", false },
                    { 163, "RIH250163", false },
                    { 164, "RIH250164", false },
                    { 165, "RIH250165", false },
                    { 166, "RIH250166", false },
                    { 167, "RIH250167", false },
                    { 168, "RIH250168", false },
                    { 169, "RIH250169", false },
                    { 170, "RIH250170", false },
                    { 171, "RIH250171", false },
                    { 172, "RIH250172", false },
                    { 173, "RIH250173", false },
                    { 174, "RIH250174", false },
                    { 175, "RIH250175", false },
                    { 176, "RIH250176", false },
                    { 177, "RIH250177", false },
                    { 178, "RIH250178", false },
                    { 179, "RIH250179", false },
                    { 180, "RIH250180", false },
                    { 181, "RIH250181", false },
                    { 182, "RIH250182", false },
                    { 183, "RIH250183", false },
                    { 184, "RIH250184", false },
                    { 185, "RIH250185", false },
                    { 186, "RIH250186", false },
                    { 187, "RIH250187", false },
                    { 188, "RIH250188", false },
                    { 189, "RIH250189", false },
                    { 190, "RIH250190", false },
                    { 191, "RIH250191", false },
                    { 192, "RIH250192", false },
                    { 193, "RIH250193", false },
                    { 194, "RIH250194", false },
                    { 195, "RIH250195", false },
                    { 196, "RIH250196", false },
                    { 197, "RIH250197", false },
                    { 198, "RIH250198", false },
                    { 199, "RIH250199", false },
                    { 200, "RIH250200", false },
                    { 201, "RIH250201", false },
                    { 202, "RIH250202", false },
                    { 203, "RIH250203", false },
                    { 204, "RIH250204", false },
                    { 205, "RIH250205", false },
                    { 206, "RIH250206", false },
                    { 207, "RIH250207", false },
                    { 208, "RIH250208", false },
                    { 209, "RIH250209", false },
                    { 210, "RIH250210", false },
                    { 211, "RIH250211", false },
                    { 212, "RIH250212", false },
                    { 213, "RIH250213", false },
                    { 214, "RIH250214", false },
                    { 215, "RIH250215", false },
                    { 216, "RIH250216", false },
                    { 217, "RIH250217", false },
                    { 218, "RIH250218", false },
                    { 219, "RIH250219", false },
                    { 220, "RIH250220", false },
                    { 221, "RIH250221", false },
                    { 222, "RIH250222", false },
                    { 223, "RIH250223", false },
                    { 224, "RIH250224", false },
                    { 225, "RIH250225", false },
                    { 226, "RIH250226", false },
                    { 227, "RIH250227", false },
                    { 228, "RIH250228", false },
                    { 229, "RIH250229", false },
                    { 230, "RIH250230", false },
                    { 231, "RIH250231", false },
                    { 232, "RIH250232", false },
                    { 233, "RIH250233", false },
                    { 234, "RIH250234", false },
                    { 235, "RIH250235", false },
                    { 236, "RIH250236", false },
                    { 237, "RIH250237", false },
                    { 238, "RIH250238", false },
                    { 239, "RIH250239", false },
                    { 240, "RIH250240", false },
                    { 241, "RIH250241", false },
                    { 242, "RIH250242", false },
                    { 243, "RIH250243", false },
                    { 244, "RIH250244", false },
                    { 245, "RIH250245", false },
                    { 246, "RIH250246", false },
                    { 247, "RIH250247", false },
                    { 248, "RIH250248", false },
                    { 249, "RIH250249", false },
                    { 250, "RIH250250", false },
                    { 251, "RIH250251", false },
                    { 252, "RIH250252", false },
                    { 253, "RIH250253", false },
                    { 254, "RIH250254", false },
                    { 255, "RIH250255", false },
                    { 256, "RIH250256", false },
                    { 257, "RIH250257", false },
                    { 258, "RIH250258", false },
                    { 259, "RIH250259", false },
                    { 260, "RIH250260", false },
                    { 261, "RIH250261", false },
                    { 262, "RIH250262", false },
                    { 263, "RIH250263", false },
                    { 264, "RIH250264", false },
                    { 265, "RIH250265", false },
                    { 266, "RIH250266", false },
                    { 267, "RIH250267", false },
                    { 268, "RIH250268", false },
                    { 269, "RIH250269", false },
                    { 270, "RIH250270", false },
                    { 271, "RIH250271", false },
                    { 272, "RIH250272", false },
                    { 273, "RIH250273", false },
                    { 274, "RIH250274", false },
                    { 275, "RIH250275", false },
                    { 276, "RIH250276", false },
                    { 277, "RIH250277", false },
                    { 278, "RIH250278", false },
                    { 279, "RIH250279", false },
                    { 280, "RIH250280", false },
                    { 281, "RIH250281", false },
                    { 282, "RIH250282", false },
                    { 283, "RIH250283", false },
                    { 284, "RIH250284", false },
                    { 285, "RIH250285", false },
                    { 286, "RIH250286", false },
                    { 287, "RIH250287", false },
                    { 288, "RIH250288", false },
                    { 289, "RIH250289", false },
                    { 290, "RIH250290", false },
                    { 291, "RIH250291", false },
                    { 292, "RIH250292", false },
                    { 293, "RIH250293", false },
                    { 294, "RIH250294", false },
                    { 295, "RIH250295", false },
                    { 296, "RIH250296", false },
                    { 297, "RIH250297", false },
                    { 298, "RIH250298", false },
                    { 299, "RIH250299", false },
                    { 300, "RIH250300", false },
                    { 301, "RIH250301", false },
                    { 302, "RIH250302", false },
                    { 303, "RIH250303", false },
                    { 304, "RIH250304", false },
                    { 305, "RIH250305", false },
                    { 306, "RIH250306", false },
                    { 307, "RIH250307", false },
                    { 308, "RIH250308", false },
                    { 309, "RIH250309", false },
                    { 310, "RIH250310", false },
                    { 311, "RIH250311", false },
                    { 312, "RIH250312", false },
                    { 313, "RIH250313", false },
                    { 314, "RIH250314", false },
                    { 315, "RIH250315", false },
                    { 316, "RIH250316", false },
                    { 317, "RIH250317", false },
                    { 318, "RIH250318", false },
                    { 319, "RIH250319", false },
                    { 320, "RIH250320", false },
                    { 321, "RIH250321", false },
                    { 322, "RIH250322", false },
                    { 323, "RIH250323", false },
                    { 324, "RIH250324", false },
                    { 325, "RIH250325", false },
                    { 326, "RIH250326", false },
                    { 327, "RIH250327", false },
                    { 328, "RIH250328", false },
                    { 329, "RIH250329", false },
                    { 330, "RIH250330", false },
                    { 331, "RIH250331", false },
                    { 332, "RIH250332", false },
                    { 333, "RIH250333", false },
                    { 334, "RIH250334", false },
                    { 335, "RIH250335", false },
                    { 336, "RIH250336", false },
                    { 337, "RIH250337", false },
                    { 338, "RIH250338", false },
                    { 339, "RIH250339", false },
                    { 340, "RIH250340", false },
                    { 341, "RIH250341", false },
                    { 342, "RIH250342", false },
                    { 343, "RIH250343", false },
                    { 344, "RIH250344", false },
                    { 345, "RIH250345", false },
                    { 346, "RIH250346", false },
                    { 347, "RIH250347", false },
                    { 348, "RIH250348", false },
                    { 349, "RIH250349", false },
                    { 350, "RIH250350", false },
                    { 351, "RIH250351", false },
                    { 352, "RIH250352", false },
                    { 353, "RIH250353", false },
                    { 354, "RIH250354", false },
                    { 355, "RIH250355", false },
                    { 356, "RIH250356", false },
                    { 357, "RIH250357", false },
                    { 358, "RIH250358", false },
                    { 359, "RIH250359", false },
                    { 360, "RIH250360", false },
                    { 361, "RIH250361", false },
                    { 362, "RIH250362", false },
                    { 363, "RIH250363", false },
                    { 364, "RIH250364", false },
                    { 365, "RIH250365", false },
                    { 366, "RIH250366", false },
                    { 367, "RIH250367", false },
                    { 368, "RIH250368", false },
                    { 369, "RIH250369", false },
                    { 370, "RIH250370", false },
                    { 371, "RIH250371", false },
                    { 372, "RIH250372", false },
                    { 373, "RIH250373", false },
                    { 374, "RIH250374", false },
                    { 375, "RIH250375", false },
                    { 376, "RIH250376", false },
                    { 377, "RIH250377", false },
                    { 378, "RIH250378", false },
                    { 379, "RIH250379", false },
                    { 380, "RIH250380", false },
                    { 381, "RIH250381", false },
                    { 382, "RIH250382", false },
                    { 383, "RIH250383", false },
                    { 384, "RIH250384", false },
                    { 385, "RIH250385", false },
                    { 386, "RIH250386", false },
                    { 387, "RIH250387", false },
                    { 388, "RIH250388", false },
                    { 389, "RIH250389", false },
                    { 390, "RIH250390", false },
                    { 391, "RIH250391", false },
                    { 392, "RIH250392", false },
                    { 393, "RIH250393", false },
                    { 394, "RIH250394", false },
                    { 395, "RIH250395", false },
                    { 396, "RIH250396", false },
                    { 397, "RIH250397", false },
                    { 398, "RIH250398", false },
                    { 399, "RIH250399", false },
                    { 400, "RIH250400", false },
                    { 401, "RIH250401", false },
                    { 402, "RIH250402", false },
                    { 403, "RIH250403", false },
                    { 404, "RIH250404", false },
                    { 405, "RIH250405", false },
                    { 406, "RIH250406", false },
                    { 407, "RIH250407", false },
                    { 408, "RIH250408", false },
                    { 409, "RIH250409", false },
                    { 410, "RIH250410", false },
                    { 411, "RIH250411", false },
                    { 412, "RIH250412", false },
                    { 413, "RIH250413", false },
                    { 414, "RIH250414", false },
                    { 415, "RIH250415", false },
                    { 416, "RIH250416", false },
                    { 417, "RIH250417", false },
                    { 418, "RIH250418", false },
                    { 419, "RIH250419", false },
                    { 420, "RIH250420", false },
                    { 421, "RIH250421", false },
                    { 422, "RIH250422", false },
                    { 423, "RIH250423", false },
                    { 424, "RIH250424", false },
                    { 425, "RIH250425", false },
                    { 426, "RIH250426", false },
                    { 427, "RIH250427", false },
                    { 428, "RIH250428", false },
                    { 429, "RIH250429", false },
                    { 430, "RIH250430", false },
                    { 431, "RIH250431", false },
                    { 432, "RIH250432", false },
                    { 433, "RIH250433", false },
                    { 434, "RIH250434", false },
                    { 435, "RIH250435", false },
                    { 436, "RIH250436", false },
                    { 437, "RIH250437", false },
                    { 438, "RIH250438", false },
                    { 439, "RIH250439", false },
                    { 440, "RIH250440", false },
                    { 441, "RIH250441", false },
                    { 442, "RIH250442", false },
                    { 443, "RIH250443", false },
                    { 444, "RIH250444", false },
                    { 445, "RIH250445", false },
                    { 446, "RIH250446", false },
                    { 447, "RIH250447", false },
                    { 448, "RIH250448", false },
                    { 449, "RIH250449", false },
                    { 450, "RIH250450", false },
                    { 451, "RIH250451", false },
                    { 452, "RIH250452", false },
                    { 453, "RIH250453", false },
                    { 454, "RIH250454", false },
                    { 455, "RIH250455", false },
                    { 456, "RIH250456", false },
                    { 457, "RIH250457", false },
                    { 458, "RIH250458", false },
                    { 459, "RIH250459", false },
                    { 460, "RIH250460", false },
                    { 461, "RIH250461", false },
                    { 462, "RIH250462", false },
                    { 463, "RIH250463", false },
                    { 464, "RIH250464", false },
                    { 465, "RIH250465", false },
                    { 466, "RIH250466", false },
                    { 467, "RIH250467", false },
                    { 468, "RIH250468", false },
                    { 469, "RIH250469", false },
                    { 470, "RIH250470", false },
                    { 471, "RIH250471", false },
                    { 472, "RIH250472", false },
                    { 473, "RIH250473", false },
                    { 474, "RIH250474", false },
                    { 475, "RIH250475", false },
                    { 476, "RIH250476", false },
                    { 477, "RIH250477", false },
                    { 478, "RIH250478", false },
                    { 479, "RIH250479", false },
                    { 480, "RIH250480", false },
                    { 481, "RIH250481", false },
                    { 482, "RIH250482", false },
                    { 483, "RIH250483", false },
                    { 484, "RIH250484", false },
                    { 485, "RIH250485", false },
                    { 486, "RIH250486", false },
                    { 487, "RIH250487", false },
                    { 488, "RIH250488", false },
                    { 489, "RIH250489", false },
                    { 490, "RIH250490", false },
                    { 491, "RIH250491", false },
                    { 492, "RIH250492", false },
                    { 493, "RIH250493", false },
                    { 494, "RIH250494", false },
                    { 495, "RIH250495", false },
                    { 496, "RIH250496", false },
                    { 497, "RIH250497", false },
                    { 498, "RIH250498", false },
                    { 499, "RIH250499", false },
                    { 500, "RIH250500", false },
                    { 501, "RIH250501", false },
                    { 502, "RIH250502", false },
                    { 503, "RIH250503", false },
                    { 504, "RIH250504", false },
                    { 505, "RIH250505", false },
                    { 506, "RIH250506", false },
                    { 507, "RIH250507", false },
                    { 508, "RIH250508", false },
                    { 509, "RIH250509", false },
                    { 510, "RIH250510", false },
                    { 511, "RIH250511", false },
                    { 512, "RIH250512", false },
                    { 513, "RIH250513", false },
                    { 514, "RIH250514", false },
                    { 515, "RIH250515", false },
                    { 516, "RIH250516", false },
                    { 517, "RIH250517", false },
                    { 518, "RIH250518", false },
                    { 519, "RIH250519", false },
                    { 520, "RIH250520", false },
                    { 521, "RIH250521", false },
                    { 522, "RIH250522", false },
                    { 523, "RIH250523", false },
                    { 524, "RIH250524", false },
                    { 525, "RIH250525", false },
                    { 526, "RIH250526", false },
                    { 527, "RIH250527", false },
                    { 528, "RIH250528", false },
                    { 529, "RIH250529", false },
                    { 530, "RIH250530", false },
                    { 531, "RIH250531", false },
                    { 532, "RIH250532", false },
                    { 533, "RIH250533", false },
                    { 534, "RIH250534", false },
                    { 535, "RIH250535", false },
                    { 536, "RIH250536", false },
                    { 537, "RIH250537", false },
                    { 538, "RIH250538", false },
                    { 539, "RIH250539", false },
                    { 540, "RIH250540", false },
                    { 541, "RIH250541", false },
                    { 542, "RIH250542", false },
                    { 543, "RIH250543", false },
                    { 544, "RIH250544", false },
                    { 545, "RIH250545", false },
                    { 546, "RIH250546", false },
                    { 547, "RIH250547", false },
                    { 548, "RIH250548", false },
                    { 549, "RIH250549", false },
                    { 550, "RIH250550", false },
                    { 551, "RIH250551", false },
                    { 552, "RIH250552", false },
                    { 553, "RIH250553", false },
                    { 554, "RIH250554", false },
                    { 555, "RIH250555", false },
                    { 556, "RIH250556", false },
                    { 557, "RIH250557", false },
                    { 558, "RIH250558", false },
                    { 559, "RIH250559", false },
                    { 560, "RIH250560", false },
                    { 561, "RIH250561", false },
                    { 562, "RIH250562", false },
                    { 563, "RIH250563", false },
                    { 564, "RIH250564", false },
                    { 565, "RIH250565", false },
                    { 566, "RIH250566", false },
                    { 567, "RIH250567", false },
                    { 568, "RIH250568", false },
                    { 569, "RIH250569", false },
                    { 570, "RIH250570", false },
                    { 571, "RIH250571", false },
                    { 572, "RIH250572", false },
                    { 573, "RIH250573", false },
                    { 574, "RIH250574", false },
                    { 575, "RIH250575", false },
                    { 576, "RIH250576", false },
                    { 577, "RIH250577", false },
                    { 578, "RIH250578", false },
                    { 579, "RIH250579", false },
                    { 580, "RIH250580", false },
                    { 581, "RIH250581", false },
                    { 582, "RIH250582", false },
                    { 583, "RIH250583", false },
                    { 584, "RIH250584", false },
                    { 585, "RIH250585", false },
                    { 586, "RIH250586", false },
                    { 587, "RIH250587", false },
                    { 588, "RIH250588", false },
                    { 589, "RIH250589", false },
                    { 590, "RIH250590", false },
                    { 591, "RIH250591", false },
                    { 592, "RIH250592", false },
                    { 593, "RIH250593", false },
                    { 594, "RIH250594", false },
                    { 595, "RIH250595", false },
                    { 596, "RIH250596", false },
                    { 597, "RIH250597", false },
                    { 598, "RIH250598", false },
                    { 599, "RIH250599", false },
                    { 600, "RIH250600", false },
                    { 601, "RIH250601", false },
                    { 602, "RIH250602", false },
                    { 603, "RIH250603", false },
                    { 604, "RIH250604", false },
                    { 605, "RIH250605", false },
                    { 606, "RIH250606", false },
                    { 607, "RIH250607", false },
                    { 608, "RIH250608", false },
                    { 609, "RIH250609", false },
                    { 610, "RIH250610", false },
                    { 611, "RIH250611", false },
                    { 612, "RIH250612", false },
                    { 613, "RIH250613", false },
                    { 614, "RIH250614", false },
                    { 615, "RIH250615", false },
                    { 616, "RIH250616", false },
                    { 617, "RIH250617", false },
                    { 618, "RIH250618", false },
                    { 619, "RIH250619", false },
                    { 620, "RIH250620", false },
                    { 621, "RIH250621", false },
                    { 622, "RIH250622", false },
                    { 623, "RIH250623", false },
                    { 624, "RIH250624", false },
                    { 625, "RIH250625", false },
                    { 626, "RIH250626", false },
                    { 627, "RIH250627", false },
                    { 628, "RIH250628", false },
                    { 629, "RIH250629", false },
                    { 630, "RIH250630", false },
                    { 631, "RIH250631", false },
                    { 632, "RIH250632", false },
                    { 633, "RIH250633", false },
                    { 634, "RIH250634", false },
                    { 635, "RIH250635", false },
                    { 636, "RIH250636", false },
                    { 637, "RIH250637", false },
                    { 638, "RIH250638", false },
                    { 639, "RIH250639", false },
                    { 640, "RIH250640", false },
                    { 641, "RIH250641", false },
                    { 642, "RIH250642", false },
                    { 643, "RIH250643", false },
                    { 644, "RIH250644", false },
                    { 645, "RIH250645", false },
                    { 646, "RIH250646", false },
                    { 647, "RIH250647", false },
                    { 648, "RIH250648", false },
                    { 649, "RIH250649", false },
                    { 650, "RIH250650", false },
                    { 651, "RIH250651", false },
                    { 652, "RIH250652", false },
                    { 653, "RIH250653", false },
                    { 654, "RIH250654", false },
                    { 655, "RIH250655", false },
                    { 656, "RIH250656", false },
                    { 657, "RIH250657", false },
                    { 658, "RIH250658", false },
                    { 659, "RIH250659", false },
                    { 660, "RIH250660", false },
                    { 661, "RIH250661", false },
                    { 662, "RIH250662", false },
                    { 663, "RIH250663", false },
                    { 664, "RIH250664", false },
                    { 665, "RIH250665", false },
                    { 666, "RIH250666", false },
                    { 667, "RIH250667", false },
                    { 668, "RIH250668", false },
                    { 669, "RIH250669", false },
                    { 670, "RIH250670", false },
                    { 671, "RIH250671", false },
                    { 672, "RIH250672", false },
                    { 673, "RIH250673", false },
                    { 674, "RIH250674", false },
                    { 675, "RIH250675", false },
                    { 676, "RIH250676", false },
                    { 677, "RIH250677", false },
                    { 678, "RIH250678", false },
                    { 679, "RIH250679", false },
                    { 680, "RIH250680", false },
                    { 681, "RIH250681", false },
                    { 682, "RIH250682", false },
                    { 683, "RIH250683", false },
                    { 684, "RIH250684", false },
                    { 685, "RIH250685", false },
                    { 686, "RIH250686", false },
                    { 687, "RIH250687", false },
                    { 688, "RIH250688", false },
                    { 689, "RIH250689", false },
                    { 690, "RIH250690", false },
                    { 691, "RIH250691", false },
                    { 692, "RIH250692", false },
                    { 693, "RIH250693", false },
                    { 694, "RIH250694", false },
                    { 695, "RIH250695", false },
                    { 696, "RIH250696", false },
                    { 697, "RIH250697", false },
                    { 698, "RIH250698", false },
                    { 699, "RIH250699", false },
                    { 700, "RIH250700", false },
                    { 701, "RIH250701", false },
                    { 702, "RIH250702", false },
                    { 703, "RIH250703", false },
                    { 704, "RIH250704", false },
                    { 705, "RIH250705", false },
                    { 706, "RIH250706", false },
                    { 707, "RIH250707", false },
                    { 708, "RIH250708", false },
                    { 709, "RIH250709", false },
                    { 710, "RIH250710", false },
                    { 711, "RIH250711", false },
                    { 712, "RIH250712", false },
                    { 713, "RIH250713", false },
                    { 714, "RIH250714", false },
                    { 715, "RIH250715", false },
                    { 716, "RIH250716", false },
                    { 717, "RIH250717", false },
                    { 718, "RIH250718", false },
                    { 719, "RIH250719", false },
                    { 720, "RIH250720", false },
                    { 721, "RIH250721", false },
                    { 722, "RIH250722", false },
                    { 723, "RIH250723", false },
                    { 724, "RIH250724", false },
                    { 725, "RIH250725", false },
                    { 726, "RIH250726", false },
                    { 727, "RIH250727", false },
                    { 728, "RIH250728", false },
                    { 729, "RIH250729", false },
                    { 730, "RIH250730", false },
                    { 731, "RIH250731", false },
                    { 732, "RIH250732", false },
                    { 733, "RIH250733", false },
                    { 734, "RIH250734", false },
                    { 735, "RIH250735", false },
                    { 736, "RIH250736", false },
                    { 737, "RIH250737", false },
                    { 738, "RIH250738", false },
                    { 739, "RIH250739", false },
                    { 740, "RIH250740", false },
                    { 741, "RIH250741", false },
                    { 742, "RIH250742", false },
                    { 743, "RIH250743", false },
                    { 744, "RIH250744", false },
                    { 745, "RIH250745", false },
                    { 746, "RIH250746", false },
                    { 747, "RIH250747", false },
                    { 748, "RIH250748", false },
                    { 749, "RIH250749", false },
                    { 750, "RIH250750", false },
                    { 751, "RIH250751", false },
                    { 752, "RIH250752", false },
                    { 753, "RIH250753", false },
                    { 754, "RIH250754", false },
                    { 755, "RIH250755", false },
                    { 756, "RIH250756", false },
                    { 757, "RIH250757", false },
                    { 758, "RIH250758", false },
                    { 759, "RIH250759", false },
                    { 760, "RIH250760", false },
                    { 761, "RIH250761", false },
                    { 762, "RIH250762", false },
                    { 763, "RIH250763", false },
                    { 764, "RIH250764", false },
                    { 765, "RIH250765", false },
                    { 766, "RIH250766", false },
                    { 767, "RIH250767", false },
                    { 768, "RIH250768", false },
                    { 769, "RIH250769", false },
                    { 770, "RIH250770", false },
                    { 771, "RIH250771", false },
                    { 772, "RIH250772", false },
                    { 773, "RIH250773", false },
                    { 774, "RIH250774", false },
                    { 775, "RIH250775", false },
                    { 776, "RIH250776", false },
                    { 777, "RIH250777", false },
                    { 778, "RIH250778", false },
                    { 779, "RIH250779", false },
                    { 780, "RIH250780", false },
                    { 781, "RIH250781", false },
                    { 782, "RIH250782", false },
                    { 783, "RIH250783", false },
                    { 784, "RIH250784", false },
                    { 785, "RIH250785", false },
                    { 786, "RIH250786", false },
                    { 787, "RIH250787", false },
                    { 788, "RIH250788", false },
                    { 789, "RIH250789", false },
                    { 790, "RIH250790", false },
                    { 791, "RIH250791", false },
                    { 792, "RIH250792", false },
                    { 793, "RIH250793", false },
                    { 794, "RIH250794", false },
                    { 795, "RIH250795", false },
                    { 796, "RIH250796", false },
                    { 797, "RIH250797", false },
                    { 798, "RIH250798", false },
                    { 799, "RIH250799", false },
                    { 800, "RIH250800", false },
                    { 801, "RIH250801", false },
                    { 802, "RIH250802", false },
                    { 803, "RIH250803", false },
                    { 804, "RIH250804", false },
                    { 805, "RIH250805", false },
                    { 806, "RIH250806", false },
                    { 807, "RIH250807", false },
                    { 808, "RIH250808", false },
                    { 809, "RIH250809", false },
                    { 810, "RIH250810", false },
                    { 811, "RIH250811", false },
                    { 812, "RIH250812", false },
                    { 813, "RIH250813", false },
                    { 814, "RIH250814", false },
                    { 815, "RIH250815", false },
                    { 816, "RIH250816", false },
                    { 817, "RIH250817", false },
                    { 818, "RIH250818", false },
                    { 819, "RIH250819", false },
                    { 820, "RIH250820", false },
                    { 821, "RIH250821", false },
                    { 822, "RIH250822", false },
                    { 823, "RIH250823", false },
                    { 824, "RIH250824", false },
                    { 825, "RIH250825", false },
                    { 826, "RIH250826", false },
                    { 827, "RIH250827", false },
                    { 828, "RIH250828", false },
                    { 829, "RIH250829", false },
                    { 830, "RIH250830", false },
                    { 831, "RIH250831", false },
                    { 832, "RIH250832", false },
                    { 833, "RIH250833", false },
                    { 834, "RIH250834", false },
                    { 835, "RIH250835", false },
                    { 836, "RIH250836", false },
                    { 837, "RIH250837", false },
                    { 838, "RIH250838", false },
                    { 839, "RIH250839", false },
                    { 840, "RIH250840", false },
                    { 841, "RIH250841", false },
                    { 842, "RIH250842", false },
                    { 843, "RIH250843", false },
                    { 844, "RIH250844", false },
                    { 845, "RIH250845", false },
                    { 846, "RIH250846", false },
                    { 847, "RIH250847", false },
                    { 848, "RIH250848", false },
                    { 849, "RIH250849", false },
                    { 850, "RIH250850", false },
                    { 851, "RIH250851", false },
                    { 852, "RIH250852", false },
                    { 853, "RIH250853", false },
                    { 854, "RIH250854", false },
                    { 855, "RIH250855", false },
                    { 856, "RIH250856", false },
                    { 857, "RIH250857", false },
                    { 858, "RIH250858", false },
                    { 859, "RIH250859", false },
                    { 860, "RIH250860", false },
                    { 861, "RIH250861", false },
                    { 862, "RIH250862", false },
                    { 863, "RIH250863", false },
                    { 864, "RIH250864", false },
                    { 865, "RIH250865", false },
                    { 866, "RIH250866", false },
                    { 867, "RIH250867", false },
                    { 868, "RIH250868", false },
                    { 869, "RIH250869", false },
                    { 870, "RIH250870", false },
                    { 871, "RIH250871", false },
                    { 872, "RIH250872", false },
                    { 873, "RIH250873", false },
                    { 874, "RIH250874", false },
                    { 875, "RIH250875", false },
                    { 876, "RIH250876", false },
                    { 877, "RIH250877", false },
                    { 878, "RIH250878", false },
                    { 879, "RIH250879", false },
                    { 880, "RIH250880", false },
                    { 881, "RIH250881", false },
                    { 882, "RIH250882", false },
                    { 883, "RIH250883", false },
                    { 884, "RIH250884", false },
                    { 885, "RIH250885", false },
                    { 886, "RIH250886", false },
                    { 887, "RIH250887", false },
                    { 888, "RIH250888", false },
                    { 889, "RIH250889", false },
                    { 890, "RIH250890", false },
                    { 891, "RIH250891", false },
                    { 892, "RIH250892", false },
                    { 893, "RIH250893", false },
                    { 894, "RIH250894", false },
                    { 895, "RIH250895", false },
                    { 896, "RIH250896", false },
                    { 897, "RIH250897", false },
                    { 898, "RIH250898", false },
                    { 899, "RIH250899", false },
                    { 900, "RIH250900", false },
                    { 901, "RIH250901", false },
                    { 902, "RIH250902", false },
                    { 903, "RIH250903", false },
                    { 904, "RIH250904", false },
                    { 905, "RIH250905", false },
                    { 906, "RIH250906", false },
                    { 907, "RIH250907", false },
                    { 908, "RIH250908", false },
                    { 909, "RIH250909", false },
                    { 910, "RIH250910", false },
                    { 911, "RIH250911", false },
                    { 912, "RIH250912", false },
                    { 913, "RIH250913", false },
                    { 914, "RIH250914", false },
                    { 915, "RIH250915", false },
                    { 916, "RIH250916", false },
                    { 917, "RIH250917", false },
                    { 918, "RIH250918", false },
                    { 919, "RIH250919", false },
                    { 920, "RIH250920", false },
                    { 921, "RIH250921", false },
                    { 922, "RIH250922", false },
                    { 923, "RIH250923", false },
                    { 924, "RIH250924", false },
                    { 925, "RIH250925", false },
                    { 926, "RIH250926", false },
                    { 927, "RIH250927", false },
                    { 928, "RIH250928", false },
                    { 929, "RIH250929", false },
                    { 930, "RIH250930", false },
                    { 931, "RIH250931", false },
                    { 932, "RIH250932", false },
                    { 933, "RIH250933", false },
                    { 934, "RIH250934", false },
                    { 935, "RIH250935", false },
                    { 936, "RIH250936", false },
                    { 937, "RIH250937", false },
                    { 938, "RIH250938", false },
                    { 939, "RIH250939", false },
                    { 940, "RIH250940", false },
                    { 941, "RIH250941", false },
                    { 942, "RIH250942", false },
                    { 943, "RIH250943", false },
                    { 944, "RIH250944", false },
                    { 945, "RIH250945", false },
                    { 946, "RIH250946", false },
                    { 947, "RIH250947", false },
                    { 948, "RIH250948", false },
                    { 949, "RIH250949", false },
                    { 950, "RIH250950", false },
                    { 951, "RIH250951", false },
                    { 952, "RIH250952", false },
                    { 953, "RIH250953", false },
                    { 954, "RIH250954", false },
                    { 955, "RIH250955", false },
                    { 956, "RIH250956", false },
                    { 957, "RIH250957", false },
                    { 958, "RIH250958", false },
                    { 959, "RIH250959", false },
                    { 960, "RIH250960", false },
                    { 961, "RIH250961", false },
                    { 962, "RIH250962", false },
                    { 963, "RIH250963", false },
                    { 964, "RIH250964", false },
                    { 965, "RIH250965", false },
                    { 966, "RIH250966", false },
                    { 967, "RIH250967", false },
                    { 968, "RIH250968", false },
                    { 969, "RIH250969", false },
                    { 970, "RIH250970", false },
                    { 971, "RIH250971", false },
                    { 972, "RIH250972", false },
                    { 973, "RIH250973", false },
                    { 974, "RIH250974", false },
                    { 975, "RIH250975", false },
                    { 976, "RIH250976", false },
                    { 977, "RIH250977", false },
                    { 978, "RIH250978", false },
                    { 979, "RIH250979", false },
                    { 980, "RIH250980", false },
                    { 981, "RIH250981", false },
                    { 982, "RIH250982", false },
                    { 983, "RIH250983", false },
                    { 984, "RIH250984", false },
                    { 985, "RIH250985", false },
                    { 986, "RIH250986", false },
                    { 987, "RIH250987", false },
                    { 988, "RIH250988", false },
                    { 989, "RIH250989", false },
                    { 990, "RIH250990", false },
                    { 991, "RIH250991", false },
                    { 992, "RIH250992", false },
                    { 993, "RIH250993", false },
                    { 994, "RIH250994", false },
                    { 995, "RIH250995", false },
                    { 996, "RIH250996", false },
                    { 997, "RIH250997", false },
                    { 998, "RIH250998", false },
                    { 999, "RIH250999", false },
                    { 1000, "RIH251000", false },
                    { 1001, "RIH251001", false },
                    { 1002, "RIH251002", false },
                    { 1003, "RIH251003", false },
                    { 1004, "RIH251004", false },
                    { 1005, "RIH251005", false },
                    { 1006, "RIH251006", false },
                    { 1007, "RIH251007", false },
                    { 1008, "RIH251008", false },
                    { 1009, "RIH251009", false },
                    { 1010, "RIH251010", false },
                    { 1011, "RIH251011", false },
                    { 1012, "RIH251012", false },
                    { 1013, "RIH251013", false },
                    { 1014, "RIH251014", false },
                    { 1015, "RIH251015", false },
                    { 1016, "RIH251016", false },
                    { 1017, "RIH251017", false },
                    { 1018, "RIH251018", false },
                    { 1019, "RIH251019", false },
                    { 1020, "RIH251020", false },
                    { 1021, "RIH251021", false },
                    { 1022, "RIH251022", false },
                    { 1023, "RIH251023", false },
                    { 1024, "RIH251024", false },
                    { 1025, "RIH251025", false },
                    { 1026, "RIH251026", false },
                    { 1027, "RIH251027", false },
                    { 1028, "RIH251028", false },
                    { 1029, "RIH251029", false },
                    { 1030, "RIH251030", false },
                    { 1031, "RIH251031", false },
                    { 1032, "RIH251032", false },
                    { 1033, "RIH251033", false },
                    { 1034, "RIH251034", false },
                    { 1035, "RIH251035", false },
                    { 1036, "RIH251036", false },
                    { 1037, "RIH251037", false },
                    { 1038, "RIH251038", false },
                    { 1039, "RIH251039", false },
                    { 1040, "RIH251040", false },
                    { 1041, "RIH251041", false },
                    { 1042, "RIH251042", false },
                    { 1043, "RIH251043", false },
                    { 1044, "RIH251044", false },
                    { 1045, "RIH251045", false },
                    { 1046, "RIH251046", false },
                    { 1047, "RIH251047", false },
                    { 1048, "RIH251048", false },
                    { 1049, "RIH251049", false },
                    { 1050, "RIH251050", false },
                    { 1051, "RIH251051", false },
                    { 1052, "RIH251052", false },
                    { 1053, "RIH251053", false },
                    { 1054, "RIH251054", false },
                    { 1055, "RIH251055", false },
                    { 1056, "RIH251056", false },
                    { 1057, "RIH251057", false },
                    { 1058, "RIH251058", false },
                    { 1059, "RIH251059", false },
                    { 1060, "RIH251060", false },
                    { 1061, "RIH251061", false },
                    { 1062, "RIH251062", false },
                    { 1063, "RIH251063", false },
                    { 1064, "RIH251064", false },
                    { 1065, "RIH251065", false },
                    { 1066, "RIH251066", false },
                    { 1067, "RIH251067", false },
                    { 1068, "RIH251068", false },
                    { 1069, "RIH251069", false },
                    { 1070, "RIH251070", false },
                    { 1071, "RIH251071", false },
                    { 1072, "RIH251072", false },
                    { 1073, "RIH251073", false },
                    { 1074, "RIH251074", false },
                    { 1075, "RIH251075", false },
                    { 1076, "RIH251076", false },
                    { 1077, "RIH251077", false },
                    { 1078, "RIH251078", false },
                    { 1079, "RIH251079", false },
                    { 1080, "RIH251080", false },
                    { 1081, "RIH251081", false },
                    { 1082, "RIH251082", false },
                    { 1083, "RIH251083", false },
                    { 1084, "RIH251084", false },
                    { 1085, "RIH251085", false },
                    { 1086, "RIH251086", false },
                    { 1087, "RIH251087", false },
                    { 1088, "RIH251088", false },
                    { 1089, "RIH251089", false },
                    { 1090, "RIH251090", false },
                    { 1091, "RIH251091", false },
                    { 1092, "RIH251092", false },
                    { 1093, "RIH251093", false },
                    { 1094, "RIH251094", false },
                    { 1095, "RIH251095", false },
                    { 1096, "RIH251096", false },
                    { 1097, "RIH251097", false },
                    { 1098, "RIH251098", false },
                    { 1099, "RIH251099", false },
                    { 1100, "RIH251100", false },
                    { 1101, "RIH251101", false },
                    { 1102, "RIH251102", false },
                    { 1103, "RIH251103", false },
                    { 1104, "RIH251104", false },
                    { 1105, "RIH251105", false },
                    { 1106, "RIH251106", false },
                    { 1107, "RIH251107", false },
                    { 1108, "RIH251108", false },
                    { 1109, "RIH251109", false },
                    { 1110, "RIH251110", false },
                    { 1111, "RIH251111", false },
                    { 1112, "RIH251112", false },
                    { 1113, "RIH251113", false },
                    { 1114, "RIH251114", false },
                    { 1115, "RIH251115", false },
                    { 1116, "RIH251116", false },
                    { 1117, "RIH251117", false },
                    { 1118, "RIH251118", false },
                    { 1119, "RIH251119", false },
                    { 1120, "RIH251120", false },
                    { 1121, "RIH251121", false },
                    { 1122, "RIH251122", false },
                    { 1123, "RIH251123", false },
                    { 1124, "RIH251124", false },
                    { 1125, "RIH251125", false },
                    { 1126, "RIH251126", false },
                    { 1127, "RIH251127", false },
                    { 1128, "RIH251128", false },
                    { 1129, "RIH251129", false },
                    { 1130, "RIH251130", false },
                    { 1131, "RIH251131", false },
                    { 1132, "RIH251132", false },
                    { 1133, "RIH251133", false },
                    { 1134, "RIH251134", false },
                    { 1135, "RIH251135", false },
                    { 1136, "RIH251136", false },
                    { 1137, "RIH251137", false },
                    { 1138, "RIH251138", false },
                    { 1139, "RIH251139", false },
                    { 1140, "RIH251140", false },
                    { 1141, "RIH251141", false },
                    { 1142, "RIH251142", false },
                    { 1143, "RIH251143", false },
                    { 1144, "RIH251144", false },
                    { 1145, "RIH251145", false },
                    { 1146, "RIH251146", false },
                    { 1147, "RIH251147", false },
                    { 1148, "RIH251148", false },
                    { 1149, "RIH251149", false },
                    { 1150, "RIH251150", false },
                    { 1151, "RIH251151", false },
                    { 1152, "RIH251152", false },
                    { 1153, "RIH251153", false },
                    { 1154, "RIH251154", false },
                    { 1155, "RIH251155", false },
                    { 1156, "RIH251156", false },
                    { 1157, "RIH251157", false },
                    { 1158, "RIH251158", false },
                    { 1159, "RIH251159", false },
                    { 1160, "RIH251160", false },
                    { 1161, "RIH251161", false },
                    { 1162, "RIH251162", false },
                    { 1163, "RIH251163", false },
                    { 1164, "RIH251164", false },
                    { 1165, "RIH251165", false },
                    { 1166, "RIH251166", false },
                    { 1167, "RIH251167", false },
                    { 1168, "RIH251168", false },
                    { 1169, "RIH251169", false },
                    { 1170, "RIH251170", false },
                    { 1171, "RIH251171", false },
                    { 1172, "RIH251172", false },
                    { 1173, "RIH251173", false },
                    { 1174, "RIH251174", false },
                    { 1175, "RIH251175", false },
                    { 1176, "RIH251176", false },
                    { 1177, "RIH251177", false },
                    { 1178, "RIH251178", false },
                    { 1179, "RIH251179", false },
                    { 1180, "RIH251180", false },
                    { 1181, "RIH251181", false },
                    { 1182, "RIH251182", false },
                    { 1183, "RIH251183", false },
                    { 1184, "RIH251184", false },
                    { 1185, "RIH251185", false },
                    { 1186, "RIH251186", false },
                    { 1187, "RIH251187", false },
                    { 1188, "RIH251188", false },
                    { 1189, "RIH251189", false },
                    { 1190, "RIH251190", false },
                    { 1191, "RIH251191", false },
                    { 1192, "RIH251192", false },
                    { 1193, "RIH251193", false },
                    { 1194, "RIH251194", false },
                    { 1195, "RIH251195", false },
                    { 1196, "RIH251196", false },
                    { 1197, "RIH251197", false },
                    { 1198, "RIH251198", false },
                    { 1199, "RIH251199", false },
                    { 1200, "RIH251200", false },
                    { 1201, "RIH251201", false },
                    { 1202, "RIH251202", false },
                    { 1203, "RIH251203", false },
                    { 1204, "RIH251204", false },
                    { 1205, "RIH251205", false },
                    { 1206, "RIH251206", false },
                    { 1207, "RIH251207", false },
                    { 1208, "RIH251208", false },
                    { 1209, "RIH251209", false },
                    { 1210, "RIH251210", false },
                    { 1211, "RIH251211", false },
                    { 1212, "RIH251212", false },
                    { 1213, "RIH251213", false },
                    { 1214, "RIH251214", false },
                    { 1215, "RIH251215", false },
                    { 1216, "RIH251216", false },
                    { 1217, "RIH251217", false },
                    { 1218, "RIH251218", false },
                    { 1219, "RIH251219", false },
                    { 1220, "RIH251220", false },
                    { 1221, "RIH251221", false },
                    { 1222, "RIH251222", false },
                    { 1223, "RIH251223", false },
                    { 1224, "RIH251224", false },
                    { 1225, "RIH251225", false },
                    { 1226, "RIH251226", false },
                    { 1227, "RIH251227", false },
                    { 1228, "RIH251228", false },
                    { 1229, "RIH251229", false },
                    { 1230, "RIH251230", false },
                    { 1231, "RIH251231", false },
                    { 1232, "RIH251232", false },
                    { 1233, "RIH251233", false },
                    { 1234, "RIH251234", false },
                    { 1235, "RIH251235", false },
                    { 1236, "RIH251236", false },
                    { 1237, "RIH251237", false },
                    { 1238, "RIH251238", false },
                    { 1239, "RIH251239", false },
                    { 1240, "RIH251240", false },
                    { 1241, "RIH251241", false },
                    { 1242, "RIH251242", false },
                    { 1243, "RIH251243", false },
                    { 1244, "RIH251244", false },
                    { 1245, "RIH251245", false },
                    { 1246, "RIH251246", false },
                    { 1247, "RIH251247", false },
                    { 1248, "RIH251248", false },
                    { 1249, "RIH251249", false },
                    { 1250, "RIH251250", false },
                    { 1251, "RIH251251", false },
                    { 1252, "RIH251252", false },
                    { 1253, "RIH251253", false },
                    { 1254, "RIH251254", false },
                    { 1255, "RIH251255", false },
                    { 1256, "RIH251256", false },
                    { 1257, "RIH251257", false },
                    { 1258, "RIH251258", false },
                    { 1259, "RIH251259", false },
                    { 1260, "RIH251260", false },
                    { 1261, "RIH251261", false },
                    { 1262, "RIH251262", false },
                    { 1263, "RIH251263", false },
                    { 1264, "RIH251264", false },
                    { 1265, "RIH251265", false },
                    { 1266, "RIH251266", false },
                    { 1267, "RIH251267", false },
                    { 1268, "RIH251268", false },
                    { 1269, "RIH251269", false },
                    { 1270, "RIH251270", false },
                    { 1271, "RIH251271", false },
                    { 1272, "RIH251272", false },
                    { 1273, "RIH251273", false },
                    { 1274, "RIH251274", false },
                    { 1275, "RIH251275", false },
                    { 1276, "RIH251276", false },
                    { 1277, "RIH251277", false },
                    { 1278, "RIH251278", false },
                    { 1279, "RIH251279", false },
                    { 1280, "RIH251280", false },
                    { 1281, "RIH251281", false },
                    { 1282, "RIH251282", false },
                    { 1283, "RIH251283", false },
                    { 1284, "RIH251284", false },
                    { 1285, "RIH251285", false },
                    { 1286, "RIH251286", false },
                    { 1287, "RIH251287", false },
                    { 1288, "RIH251288", false },
                    { 1289, "RIH251289", false },
                    { 1290, "RIH251290", false },
                    { 1291, "RIH251291", false },
                    { 1292, "RIH251292", false },
                    { 1293, "RIH251293", false },
                    { 1294, "RIH251294", false },
                    { 1295, "RIH251295", false },
                    { 1296, "RIH251296", false },
                    { 1297, "RIH251297", false },
                    { 1298, "RIH251298", false },
                    { 1299, "RIH251299", false },
                    { 1300, "RIH251300", false },
                    { 1301, "RIH251301", false },
                    { 1302, "RIH251302", false },
                    { 1303, "RIH251303", false },
                    { 1304, "RIH251304", false },
                    { 1305, "RIH251305", false },
                    { 1306, "RIH251306", false },
                    { 1307, "RIH251307", false },
                    { 1308, "RIH251308", false },
                    { 1309, "RIH251309", false },
                    { 1310, "RIH251310", false },
                    { 1311, "RIH251311", false },
                    { 1312, "RIH251312", false },
                    { 1313, "RIH251313", false },
                    { 1314, "RIH251314", false },
                    { 1315, "RIH251315", false },
                    { 1316, "RIH251316", false },
                    { 1317, "RIH251317", false },
                    { 1318, "RIH251318", false },
                    { 1319, "RIH251319", false },
                    { 1320, "RIH251320", false },
                    { 1321, "RIH251321", false },
                    { 1322, "RIH251322", false },
                    { 1323, "RIH251323", false },
                    { 1324, "RIH251324", false },
                    { 1325, "RIH251325", false },
                    { 1326, "RIH251326", false },
                    { 1327, "RIH251327", false },
                    { 1328, "RIH251328", false },
                    { 1329, "RIH251329", false },
                    { 1330, "RIH251330", false },
                    { 1331, "RIH251331", false },
                    { 1332, "RIH251332", false },
                    { 1333, "RIH251333", false },
                    { 1334, "RIH251334", false },
                    { 1335, "RIH251335", false },
                    { 1336, "RIH251336", false },
                    { 1337, "RIH251337", false },
                    { 1338, "RIH251338", false },
                    { 1339, "RIH251339", false },
                    { 1340, "RIH251340", false },
                    { 1341, "RIH251341", false },
                    { 1342, "RIH251342", false },
                    { 1343, "RIH251343", false },
                    { 1344, "RIH251344", false },
                    { 1345, "RIH251345", false },
                    { 1346, "RIH251346", false },
                    { 1347, "RIH251347", false },
                    { 1348, "RIH251348", false },
                    { 1349, "RIH251349", false },
                    { 1350, "RIH251350", false },
                    { 1351, "RIH251351", false },
                    { 1352, "RIH251352", false },
                    { 1353, "RIH251353", false },
                    { 1354, "RIH251354", false },
                    { 1355, "RIH251355", false },
                    { 1356, "RIH251356", false },
                    { 1357, "RIH251357", false },
                    { 1358, "RIH251358", false },
                    { 1359, "RIH251359", false },
                    { 1360, "RIH251360", false },
                    { 1361, "RIH251361", false },
                    { 1362, "RIH251362", false },
                    { 1363, "RIH251363", false },
                    { 1364, "RIH251364", false },
                    { 1365, "RIH251365", false },
                    { 1366, "RIH251366", false },
                    { 1367, "RIH251367", false },
                    { 1368, "RIH251368", false },
                    { 1369, "RIH251369", false },
                    { 1370, "RIH251370", false },
                    { 1371, "RIH251371", false },
                    { 1372, "RIH251372", false },
                    { 1373, "RIH251373", false },
                    { 1374, "RIH251374", false },
                    { 1375, "RIH251375", false },
                    { 1376, "RIH251376", false },
                    { 1377, "RIH251377", false },
                    { 1378, "RIH251378", false },
                    { 1379, "RIH251379", false },
                    { 1380, "RIH251380", false },
                    { 1381, "RIH251381", false },
                    { 1382, "RIH251382", false },
                    { 1383, "RIH251383", false },
                    { 1384, "RIH251384", false },
                    { 1385, "RIH251385", false },
                    { 1386, "RIH251386", false },
                    { 1387, "RIH251387", false },
                    { 1388, "RIH251388", false },
                    { 1389, "RIH251389", false },
                    { 1390, "RIH251390", false },
                    { 1391, "RIH251391", false },
                    { 1392, "RIH251392", false },
                    { 1393, "RIH251393", false },
                    { 1394, "RIH251394", false },
                    { 1395, "RIH251395", false },
                    { 1396, "RIH251396", false },
                    { 1397, "RIH251397", false },
                    { 1398, "RIH251398", false },
                    { 1399, "RIH251399", false },
                    { 1400, "RIH251400", false },
                    { 1401, "RIH251401", false },
                    { 1402, "RIH251402", false },
                    { 1403, "RIH251403", false },
                    { 1404, "RIH251404", false },
                    { 1405, "RIH251405", false },
                    { 1406, "RIH251406", false },
                    { 1407, "RIH251407", false },
                    { 1408, "RIH251408", false },
                    { 1409, "RIH251409", false },
                    { 1410, "RIH251410", false },
                    { 1411, "RIH251411", false },
                    { 1412, "RIH251412", false },
                    { 1413, "RIH251413", false },
                    { 1414, "RIH251414", false },
                    { 1415, "RIH251415", false },
                    { 1416, "RIH251416", false },
                    { 1417, "RIH251417", false },
                    { 1418, "RIH251418", false },
                    { 1419, "RIH251419", false },
                    { 1420, "RIH251420", false },
                    { 1421, "RIH251421", false },
                    { 1422, "RIH251422", false },
                    { 1423, "RIH251423", false },
                    { 1424, "RIH251424", false },
                    { 1425, "RIH251425", false },
                    { 1426, "RIH251426", false },
                    { 1427, "RIH251427", false },
                    { 1428, "RIH251428", false },
                    { 1429, "RIH251429", false },
                    { 1430, "RIH251430", false },
                    { 1431, "RIH251431", false },
                    { 1432, "RIH251432", false },
                    { 1433, "RIH251433", false },
                    { 1434, "RIH251434", false },
                    { 1435, "RIH251435", false },
                    { 1436, "RIH251436", false },
                    { 1437, "RIH251437", false },
                    { 1438, "RIH251438", false },
                    { 1439, "RIH251439", false },
                    { 1440, "RIH251440", false },
                    { 1441, "RIH251441", false },
                    { 1442, "RIH251442", false },
                    { 1443, "RIH251443", false },
                    { 1444, "RIH251444", false },
                    { 1445, "RIH251445", false },
                    { 1446, "RIH251446", false },
                    { 1447, "RIH251447", false },
                    { 1448, "RIH251448", false },
                    { 1449, "RIH251449", false },
                    { 1450, "RIH251450", false },
                    { 1451, "RIH251451", false },
                    { 1452, "RIH251452", false },
                    { 1453, "RIH251453", false },
                    { 1454, "RIH251454", false },
                    { 1455, "RIH251455", false },
                    { 1456, "RIH251456", false },
                    { 1457, "RIH251457", false },
                    { 1458, "RIH251458", false },
                    { 1459, "RIH251459", false },
                    { 1460, "RIH251460", false },
                    { 1461, "RIH251461", false },
                    { 1462, "RIH251462", false },
                    { 1463, "RIH251463", false },
                    { 1464, "RIH251464", false },
                    { 1465, "RIH251465", false },
                    { 1466, "RIH251466", false },
                    { 1467, "RIH251467", false },
                    { 1468, "RIH251468", false },
                    { 1469, "RIH251469", false },
                    { 1470, "RIH251470", false },
                    { 1471, "RIH251471", false },
                    { 1472, "RIH251472", false },
                    { 1473, "RIH251473", false },
                    { 1474, "RIH251474", false },
                    { 1475, "RIH251475", false },
                    { 1476, "RIH251476", false },
                    { 1477, "RIH251477", false },
                    { 1478, "RIH251478", false },
                    { 1479, "RIH251479", false },
                    { 1480, "RIH251480", false },
                    { 1481, "RIH251481", false },
                    { 1482, "RIH251482", false },
                    { 1483, "RIH251483", false },
                    { 1484, "RIH251484", false },
                    { 1485, "RIH251485", false },
                    { 1486, "RIH251486", false },
                    { 1487, "RIH251487", false },
                    { 1488, "RIH251488", false },
                    { 1489, "RIH251489", false },
                    { 1490, "RIH251490", false },
                    { 1491, "RIH251491", false },
                    { 1492, "RIH251492", false },
                    { 1493, "RIH251493", false },
                    { 1494, "RIH251494", false },
                    { 1495, "RIH251495", false },
                    { 1496, "RIH251496", false },
                    { 1497, "RIH251497", false },
                    { 1498, "RIH251498", false },
                    { 1499, "RIH251499", false },
                    { 1500, "RIH251500", false }
                });

            migrationBuilder.InsertData(
                table: "RIH25_RegistroStatus",
                columns: new[] { "Id", "ClaseCss", "Nombre", "NombreLargo", "Visible" },
                values: new object[,]
                {
                    { 1, "bg-warning text-body", "Recibido", "Recibidos", true },
                    { 2, "bg-success text-white", "Aceptado", "Aceptados", true },
                    { 3, "bg-danger text-white", "Rechazado", "Rechazados", true },
                    { 4, "bg-secondary text-white", "Eliminado", "Eliminados", true },
                    { 5, "bg-dark text-white", "Eliminado definitivamente", "Eliminado definitivamente", false }
                });

            migrationBuilder.InsertData(
                table: "RIH25_RegistroTipo",
                columns: new[] { "Id", "ClaseCss", "Descripcion", "FechaCierre", "IconoCss", "MaxMesas", "Nombre" },
                values: new object[] { 1, "text-white bg-morado", "XVII Reunión Internacional de Historiadores de México 2025", new DateTime(2026, 1, 16, 23, 59, 0, 0, DateTimeKind.Unspecified), "bi bi-people-fill", 3, "Reunión Internacional" });

            migrationBuilder.InsertData(
                table: "RIH25_AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a5f262fd-3650-4da3-bcfe-5fd05f0ef421", "9c324a46-edad-4aa3-a02a-63269ac9ebc7" });

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Archivo_ArchivoTipoId",
                table: "RIH25_Archivo",
                column: "ArchivoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Archivo_MesaId",
                table: "RIH25_Archivo",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Archivo_PonenciaId",
                table: "RIH25_Archivo",
                column: "PonenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Archivo_SimposioId",
                table: "RIH25_Archivo",
                column: "SimposioId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_AspNetRoleClaims_RoleId",
                table: "RIH25_AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "RIH25_AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_AspNetUserClaims_UserId",
                table: "RIH25_AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_AspNetUserLogins_UserId",
                table: "RIH25_AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_AspNetUserRoles_RoleId",
                table: "RIH25_AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "RIH25_AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "RIH25_AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Expositor_GeneroId",
                table: "RIH25_Expositor",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Expositor_PaisId",
                table: "RIH25_Expositor",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Mesa_LineaTematicaId",
                table: "RIH25_Mesa",
                column: "LineaTematicaId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Mesa_RegistroId",
                table: "RIH25_Mesa",
                column: "RegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Mesa_SimposioId",
                table: "RIH25_Mesa",
                column: "SimposioId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Ponencia_ExpositorId",
                table: "RIH25_Ponencia",
                column: "ExpositorId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Ponencia_LineaTematicaId",
                table: "RIH25_Ponencia",
                column: "LineaTematicaId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Ponencia_MesaId",
                table: "RIH25_Ponencia",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Ponencia_RegistroId",
                table: "RIH25_Ponencia",
                column: "RegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Ponencia_SimposioId",
                table: "RIH25_Ponencia",
                column: "SimposioId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Registro_RegistroFolioId",
                table: "RIH25_Registro",
                column: "RegistroFolioId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Registro_RegistroStatusId",
                table: "RIH25_Registro",
                column: "RegistroStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Registro_RegistroTipoId",
                table: "RIH25_Registro",
                column: "RegistroTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Simposio_LineaTematicaId",
                table: "RIH25_Simposio",
                column: "LineaTematicaId");

            migrationBuilder.CreateIndex(
                name: "IX_RIH25_Simposio_RegistroId",
                table: "RIH25_Simposio",
                column: "RegistroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RIH25_Archivo");

            migrationBuilder.DropTable(
                name: "RIH25_AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "RIH25_AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "RIH25_AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "RIH25_AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "RIH25_AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "RIH25_Bitacora");

            migrationBuilder.DropTable(
                name: "RIH25_Opciones");

            migrationBuilder.DropTable(
                name: "RIH25_RegionUniversitaria");

            migrationBuilder.DropTable(
                name: "RIH25_ArchivoTipo");

            migrationBuilder.DropTable(
                name: "RIH25_Ponencia");

            migrationBuilder.DropTable(
                name: "RIH25_AspNetRoles");

            migrationBuilder.DropTable(
                name: "RIH25_AspNetUsers");

            migrationBuilder.DropTable(
                name: "RIH25_Expositor");

            migrationBuilder.DropTable(
                name: "RIH25_Mesa");

            migrationBuilder.DropTable(
                name: "RIH25_Genero");

            migrationBuilder.DropTable(
                name: "RIH25_Pais");

            migrationBuilder.DropTable(
                name: "RIH25_Simposio");

            migrationBuilder.DropTable(
                name: "RIH25_LineaTematica");

            migrationBuilder.DropTable(
                name: "RIH25_Registro");

            migrationBuilder.DropTable(
                name: "RIH25_RegistroFolio");

            migrationBuilder.DropTable(
                name: "RIH25_RegistroStatus");

            migrationBuilder.DropTable(
                name: "RIH25_RegistroTipo");
        }
    }
}
