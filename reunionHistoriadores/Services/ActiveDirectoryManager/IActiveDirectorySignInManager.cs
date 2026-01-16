using reunionhistoriadores2025.Models;
using Microsoft.AspNetCore.Identity;

namespace reunionhistoriadores2025.Services.ActiveDirectoryManager
{
    public interface IActiveDirectorySignInManager<TUser>
    {
        bool PasswordSignIn(string email, string password);
        public List<CustomIdentityUser> BuscaUsuariosByMail(string mail);
        public CustomIdentityUser BuscaUsuarioBySAMAccountName(string SAMAccountName);

        public CustomIdentityUser BuscaUsuarioByEmail(string email);
    }
}
