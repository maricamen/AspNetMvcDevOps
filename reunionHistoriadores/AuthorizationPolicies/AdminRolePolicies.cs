using Microsoft.AspNetCore.Identity;

namespace reunionhistoriadores2025.AuthorizationPolicies
{
    public static class AdminRolePolicies
    {
        public const string AdministradorGeneralRoleId = "a5f262fd-3650-4da3-bcfe-5fd05f0ef421";
        public const string AdministradorRoleId = "c2e5ce65-6337-4307-b4cc-ea1cc44a23ac";
        public const string CoordinadorMesaRoleId = "56d6e236-09f5-4161-ae3d-7e50de1a7c67";
        public const string JuradoRoleId = "39dae307-9131-4a60-83e1-1aeb761e55ff";

        public const string AdministradorGeneralRole = "Administrador General";
        public const string AdministradorRole = "Administrador";
        //public const string CoordinadorMesaRole = "Coordinador de Tema";
        //public const string JuradoRole = "Jurado";

        public static readonly string[] AdministradoresPolicy = new string[] {
            AdministradorRole,
            AdministradorGeneralRole
        };

        public static readonly string[] AdministradorGeneralPolicy = new string[] {
            AdministradorGeneralRole
        };

        public static readonly string[] CoordinadorMesaPolicy = new string[] {
            //CoordinadorMesaRole,
            AdministradorRole,
            AdministradorGeneralRole
        };

        public static readonly string[] JuradosPolicy = new string[] {
            //JuradoRole,
            //CoordinadorMesaRole,
            AdministradorRole,
            AdministradorGeneralRole
        };
    }
}
