using reunionhistoriadores2025.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using reunionhistoriadores2025.Filters;
using Microsoft.Extensions.Options;
using reunionhistoriadores2025.Services.DataAnnotationsValidators;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Services.ActiveDirectoryManager;
using reunionhistoriadores2025.Services.Email;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Services.ErrorLog;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Cambiar la versión en appsettings y cambia el nombre de las tablas
string version = builder.Configuration["Version"];
// Obtiene la conexión a SQL Server
var connectionString = builder.Configuration.GetConnectionString(nameof(RegistroContext));

// ---
// Add services to the container.

// Soporte para SQL Server
builder.Services.AddDbContext<RegistroContext>(options => {
    options.UseSqlServer(connectionString,
        usesqlserveroptions => usesqlserveroptions.MigrationsHistoryTable($"{version}_MigrationHistory"));
    options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    });

// Soporte para globalización y localización
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var SupportedCultures = new List<CultureInfo>
    {
        new CultureInfo("es-MX")
    };

    options.DefaultRequestCulture = new RequestCulture("es-MX");
    options.SupportedCultures = SupportedCultures;
    options.SupportedUICultures = SupportedCultures;
});
// Soporte para globalización y localización

// Soporte para Identity
builder.Services.AddIdentity<CustomIdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<RegistroContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Admin/Account/AccessDenied";
    options.Cookie.Name = $".reunionhistoriadores{version}";
    options.LoginPath = "/Admin/Account/Login";
});
// Soporte para Identity

// Soporte para MVC
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ViewDataActionFilter>();
})
.AddViewLocalization()
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(DataAnnotationsSharedResource));
});

// Soporte para Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(nameof(AdminRolePolicies.AdministradoresPolicy),
         policy => policy.RequireRole(AdminRolePolicies.AdministradoresPolicy));

    options.AddPolicy(nameof(AdminRolePolicies.AdministradorGeneralPolicy),
         policy => policy.RequireRole(AdminRolePolicies.AdministradorGeneralPolicy));

    options.AddPolicy(nameof(AdminRolePolicies.CoordinadorMesaPolicy),
         policy => policy.RequireRole(AdminRolePolicies.CoordinadorMesaPolicy));

    options.AddPolicy(nameof(AdminRolePolicies.JuradosPolicy),
         policy => policy.RequireRole(AdminRolePolicies.JuradosPolicy));
});

// Mis servicios

// Soporte para mis validadores personalizados
builder.Services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
// Soporte para obtener las opciones desde la bd. Debes inyectar IOptionsSnapshot
builder.Services.AddScoped<IConfigureOptions<GlobalOptions>, GlobalConfigureOptions>();
// Soporte para Active Directory de UV
builder.Services.AddScoped<IActiveDirectorySignInManager<CustomIdentityUser>, ActiveDirectorySignInManager<CustomIdentityUser>>();
// Soporte para enviar correo
builder.Services.AddScoped<IEmail, Email>();
// Soporte para guardar los erroes
builder.Services.AddScoped<IErrorLog, ErrorLog>();


// ---
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
