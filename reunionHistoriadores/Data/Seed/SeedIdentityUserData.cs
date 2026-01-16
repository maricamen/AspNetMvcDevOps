using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace reunionhistoriadores2025.Data.Seed
{
    public static class SeedIdentityUserData
    {
        public static void SeedUserData(this ModelBuilder modelBuilder)
        {
            var AdministradorGeneralRoleId = AdminRolePolicies.AdministradorGeneralRoleId;
            var AdministradorRoleId = AdminRolePolicies.AdministradorRoleId;
            //var CoordinadorMesaRoleId = AdminRolePolicies.CoordinadorMesaRoleId;
            //var JuradoRoleId = AdminRolePolicies.JuradoRoleId;

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = AdministradorGeneralRoleId,
                Name = AdminRolePolicies.AdministradorGeneralRole,
                NormalizedName = AdminRolePolicies.AdministradorGeneralRole.ToUpper()
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = AdministradorRoleId,
                Name = AdminRolePolicies.AdministradorRole,
                NormalizedName = AdminRolePolicies.AdministradorRole.ToUpper()
            });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Id = CoordinadorMesaRoleId,
            //    Name = AdminRolePolicies.CoordinadorMesaRole,
            //    NormalizedName = AdminRolePolicies.CoordinadorMesaRole.ToUpper()
            //});
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Id = JuradoRoleId,
            //    Name = AdminRolePolicies.JuradoRole,
            //    NormalizedName = AdminRolePolicies.JuradoRole.ToUpper()
            //});


            // Agrega usuario
            var AdUserId = "9c324a46-edad-4aa3-a02a-63269ac9ebc7";

            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = AdUserId, // primary key
                    UserName = "maricperez@uv.mx",
                    Email = "maricperez@uv.mx",
                    NormalizedEmail = "maricperez@uv.mx".ToUpper(),
                    Nombrecompleto = "Maricarmen Pérez Campos",
                    NormalizedUserName = "maricperez@uv.mx".ToUpper(),
                    //PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "P4ssw0rd"),
                    CardsViewEnabled = true,
                    ActiveDirectoryEnabled = true,
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = AdUserId
                }
            );
        }
    }
}
