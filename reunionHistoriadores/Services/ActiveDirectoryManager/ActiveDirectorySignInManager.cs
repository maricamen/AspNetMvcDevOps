using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.Services.ErrorLog;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace reunionhistoriadores2025.Services.ActiveDirectoryManager
{
    public class ActiveDirectorySignInManager<TUser> : IActiveDirectorySignInManager<TUser> where TUser : IdentityUser
    {
        private readonly IErrorLog _errorlog;
        private readonly GlobalOptions _opciones;

        public ActiveDirectorySignInManager(IErrorLog errorlog, IOptionsSnapshot<GlobalOptions> opciones)
        {
            _errorlog = errorlog;
            _opciones = opciones.Value;
        }

        public bool PasswordSignIn(string email, string password)
        {
            bool res = false;
            //email = (email).Split('@')[0]; // you are get here username.
            try
            {
                //string dominiousuario = domainName + @"\" + email;
#pragma warning disable CA1416 // Validate platform compatibility
                DirectoryEntry Entry = new(_opciones.LdapUrlString, email, password, AuthenticationTypes.None);
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
                object nativeObject = Entry.NativeObject;
#pragma warning restore CA1416 // Validate platform compatibility
                res = true;
            }
            catch
            {
                // Algo sucedió
            }
            return res;
        }

        public CustomIdentityUser BuscaUsuarioBySAMAccountName(string SAMAccountName)
        {
            CustomIdentityUser user = null;
            try
            {
                DirectoryEntry Entry = new DirectoryEntry(_opciones.LdapUrlString + ":3268", _opciones.LdapUser, _opciones.LdapPwd, AuthenticationTypes.None);
                DirectorySearcher Search = new DirectorySearcher(Entry)
                {
                    Filter = "(&(objectCategory=person)(SAMAccountName=" + (SAMAccountName).Split('@')[0] + "))"
            };
                Search.PropertiesToLoad.Add("SAMAccountName");
                Search.PropertiesToLoad.Add("cn");
                Search.PropertiesToLoad.Add("mail");
                Search.SizeLimit = 20;
                Search.ServerTimeLimit = DateTime.Now.AddSeconds(1) - DateTime.Now;
                SearchResult result = Search.FindOne();
                if (result != null)
                {
                    user = new CustomIdentityUser();
                    user.ActiveDirectoryEnabled = true;
                    user.UserName = (string)result.Properties["SAMAccountName"][0];
                    if (result.Properties["cn"].Count > 0)
                        user.Nombrecompleto = (string)result.Properties["cn"][0];
                    if (result.Properties["mail"].Count > 0)
                        user.Email = (string)result.Properties["mail"][0];
                }
            }
            catch
            {
                // Error al buscar
            }
            return user;
        }

        public CustomIdentityUser BuscaUsuarioByEmail(string email)
        {
            CustomIdentityUser user = null;
            try
            {
                DirectoryEntry Entry = new DirectoryEntry(_opciones.LdapUrlString + ":3268", _opciones.LdapUser, _opciones.LdapPwd, AuthenticationTypes.None);
                DirectorySearcher Search = new DirectorySearcher(Entry)
                {
                    Filter = "(&(objectCategory=person)(mail=" + email + "))"
                };
                Search.PropertiesToLoad.Add("SAMAccountName");
                Search.PropertiesToLoad.Add("cn");
                Search.PropertiesToLoad.Add("mail");
                Search.SizeLimit = 20;
                Search.ServerTimeLimit = DateTime.Now.AddSeconds(1) - DateTime.Now;
                SearchResult result = Search.FindOne();
                if (result != null)
                {
                    user = new CustomIdentityUser();
                    user.ActiveDirectoryEnabled = true;
                    user.UserName = (string)result.Properties["SAMAccountName"][0];
                    if (result.Properties["cn"].Count > 0)
                        user.Nombrecompleto = (string)result.Properties["cn"][0];
                    if (result.Properties["mail"].Count > 0)
                        user.Email = (string)result.Properties["mail"][0];
                }
            }
            catch
            {
                // Error al buscar
            }
            return user;
        }

        public List<CustomIdentityUser> BuscaUsuariosByMail(string email)
        {
            List<CustomIdentityUser> users = new();
            try
            {
                DirectoryEntry Entry = new DirectoryEntry(_opciones.LdapUrlString + ":3268", _opciones.LdapUser, _opciones.LdapPwd, AuthenticationTypes.None);
                DirectorySearcher Search = new DirectorySearcher(Entry)
                {
                    Filter = "(&(objectCategory=person)(mail=" + email + "*))"
                };
                Search.PropertiesToLoad.Add("SAMAccountName");
                Search.PropertiesToLoad.Add("cn");
                Search.PropertiesToLoad.Add("mail");
                Search.SizeLimit = 20;
                Search.ServerTimeLimit = DateTime.Now.AddSeconds(1) - DateTime.Now;
                SearchResultCollection results = Search.FindAll();
                if (results != null)
                {
                    foreach (SearchResult result in results)
                    {
                        CustomIdentityUser user = new CustomIdentityUser();
                        user.ActiveDirectoryEnabled = true;
                        user.UserName = (string)result.Properties["SAMAccountName"][0];
                        if (result.Properties["cn"].Count > 0)
                            user.Nombrecompleto = (string)result.Properties["cn"][0];
                        if (result.Properties["mail"].Count > 0)
                            user.Email = (string)result.Properties["mail"][0];
                        users.Add(user);
                    }
                }
            }
            catch
            {
                // Error al buscar
            }
            return users;
        }
    }
}
