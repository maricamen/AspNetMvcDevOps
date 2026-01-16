using reunionhistoriadores2025.Areas.Admin.ViewModels.Usuarios;
using reunionhistoriadores2025.AuthorizationPolicies;
using reunionhistoriadores2025.Data;
using reunionhistoriadores2025.Helpers;
using reunionhistoriadores2025.Localization;
using reunionhistoriadores2025.Models;
using reunionhistoriadores2025.Services.ActiveDirectoryManager;
using reunionhistoriadores2025.Services.ErrorLog;
using reunionhistoriadores2025.Services.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace reunionhistoriadores2025.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = nameof(AdminRolePolicies.AdministradoresPolicy))]
    public class UsuariosController : Controller
    {
        private readonly IErrorLog _errorlog;
        private readonly RegistroContext _context;
        private readonly GlobalOptions _opciones;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly IActiveDirectorySignInManager<CustomIdentityUser> _activeDirectorySignInManager;

        public UsuariosController(IErrorLog errorlog, RegistroContext context, IOptionsSnapshot<GlobalOptions> opciones, IStringLocalizer<HomeController> localizer, IStringLocalizer<SharedResource> sharedLocalizer, UserManager<CustomIdentityUser> userManager, IActiveDirectorySignInManager<CustomIdentityUser> activeDirectorySignInManager)
        {
            _errorlog = errorlog;
            _context = context;
            _opciones = opciones.Value;
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _userManager = userManager;
            _activeDirectorySignInManager = activeDirectorySignInManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Users.Where(u => u.Deleted == false).OrderBy(u => u.UserName).AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> DetallesAsync(string id)
        {
            var model = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            //PopulateAssignedLineasTematicas(model);

            return View(model);
        }

        [HttpPost]
        public JsonResult BuscaUsuario(string phrase, string dataType)
        {
            return Json(_activeDirectorySignInManager.BuscaUsuariosByMail(phrase));
        }

        public async Task<IActionResult> CrearAsync()
        {
            ViewData["RolesSelect"] = await GetRolesAsync();
            ViewData["ControlBoleano"] = PopulateItems.GetBoleanoSelect();
            PopulateAssignedLineasTematicas();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearAsync(CrearViewModel model, string[] selectedLineaTematicas)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ActiveDirectoryEnabled && _activeDirectorySignInManager.BuscaUsuarioByEmail(model.UserName) == null)
                    {
                        ModelState.AddModelError("", $"El usuario {model.UserName} no existe en la Universidad Veracruzana.");
                    }
                    else
                    {
                        // Para crear nuevos usuarios
                        var usuario = await _context.Users.FirstOrDefaultAsync(r => r.UserName == model.UserName);
                        if (usuario == null)
                        {
                            var usuarioToCreate = new CustomIdentityUser
                            {
                                UserName = model.UserName,
                                Email = model.Email,
                                NormalizedEmail = model.Email.ToUpper(),
                                Nombrecompleto = model.Nombrecompleto,
                                NormalizedUserName = model.UserName.ToUpper(),
                                Adscripcion = model.Adscripcion,
                                //PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "P4ssw0rd"),
                                CardsViewEnabled = model.CardsViewEnabled,
                                ActiveDirectoryEnabled = model.ActiveDirectoryEnabled
                            };

                            //if (Convert.ToBoolean(_opciones.HabilitaJurado) && Convert.ToBoolean(_opciones.HabilitaJuradoLineaTematica))
                            //    UpdateUserLineasTematicas(selectedLineaTematicas, usuarioToCreate);

                            IdentityResult result;
                            if (model.ActiveDirectoryEnabled)
                                result = await _userManager.CreateAsync(usuarioToCreate);
                            else
                                result = await _userManager.CreateAsync(usuarioToCreate, model.Password);

                            if (result.Succeeded)
                            {
                                await _userManager.AddToRoleAsync(usuarioToCreate, model.Rol);

                                // Bitacora
                                _context.Bitacora.Add(new Bitacora() { Message = $"Crea usuario con Id: {usuarioToCreate.Id} y Correo: {usuarioToCreate.Email}", TimeStamp = DateTime.Now, UserName = User.Identity.Name });
                                await _context.SaveChangesAsync();

                                return RedirectToAction(nameof(Index));
                            }
                            ModelState.AddModelError("", result.Errors.ToString());
                        }
                        else if (usuario.Deleted)
                        {
                            // El usuario ya existe previamente, solo lo activamos
                            usuario.Nombrecompleto = model.Nombrecompleto;
                            usuario.Adscripcion = model.Adscripcion;
                            usuario.Email = model.Email;
                            usuario.CardsViewEnabled = model.CardsViewEnabled;
                            usuario.ActiveDirectoryEnabled = model.ActiveDirectoryEnabled;
                            usuario.Deleted = false;

                            //if (Convert.ToBoolean(_opciones.HabilitaJurado) && Convert.ToBoolean(_opciones.HabilitaJuradoLineaTematica))
                            //    UpdateUserLineasTematicas(selectedLineaTematicas, usuario);

                            foreach (var rol in await _context.Roles.ToListAsync())
                            {
                                await _userManager.RemoveFromRoleAsync(usuario, rol.Name);
                            }
                            await _userManager.AddToRoleAsync(usuario, model.Rol);

                            if (!model.ActiveDirectoryEnabled)
                            {
                                await _userManager.RemovePasswordAsync(usuario);
                                await _userManager.AddPasswordAsync(usuario, model.Password);
                            }

                            // Bitacora
                            _context.Bitacora.Add(new Bitacora() { Message = $"Crea usuario con Id: {usuario.Id} y Correo: {usuario.Email}", TimeStamp = DateTime.Now, UserName = User.Identity.Name });
                            await _context.SaveChangesAsync();

                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError("", $"El usuario {usuario.UserName} ya existe en el sistema.");
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    await _errorlog.ErrorLogAsync(ex.Message);
                    ModelState.AddModelError("", _localizer["No ha sido posible crear el usuario. Inténtelo nuevamente."]);
                }
            }

            // Se inicia el modelo en caso de error
            ViewData["RolesSelect"] = await GetRolesAsync();
            ViewData["ControlBoleano"] = PopulateItems.GetBoleanoSelect();
            PopulateAssignedLineasTematicas();

            return View();
        }

        public async Task<IActionResult> EditarAsync(string id)
        {
            var userToEdit = await _context.Users.FirstOrDefaultAsync(r => r.Id == id && r.Deleted == false);
            if (userToEdit == null)
            {
                return NotFound();
            }

            EditarViewModel usuario = new EditarViewModel()
            {
                Id = userToEdit.Id,
                Email = userToEdit.Email,
                Nombrecompleto = userToEdit.Nombrecompleto,
                Adscripcion = userToEdit.Adscripcion,
                UserName = userToEdit.UserName,
                ActiveDirectoryEnabled = userToEdit.ActiveDirectoryEnabled,
                CardsViewEnabled = userToEdit.CardsViewEnabled,
                Rol = (await _userManager.GetRolesAsync(userToEdit)).FirstOrDefault()
            };

            ViewData["RolesSelect"] = new SelectList(await _context.Roles.OrderBy(r => r.Name).AsNoTracking().ToListAsync(), "Name", "Name", usuario.Rol);
            ViewData["ControlBoleano"] = PopulateItems.GetBoleanoSelect();
            //PopulateAssignedLineasTematicas(userToEdit);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarAsync(string id, EditarViewModel model, string[] selectedLineaTematicas)
        {
            var userToEdit = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
            if (userToEdit == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userToEdit.Email = model.Email;
                    userToEdit.Nombrecompleto = model.Nombrecompleto;
                    userToEdit.Adscripcion = model.Adscripcion;
                    userToEdit.CardsViewEnabled = model.CardsViewEnabled;

                    //if (Convert.ToBoolean(_opciones.HabilitaJurado) && Convert.ToBoolean(_opciones.HabilitaJuradoLineaTematica))
                    //    UpdateUserLineasTematicas(selectedLineaTematicas, userToEdit);

                    foreach (var rol in await _context.Roles.ToListAsync())
                    {
                        await _userManager.RemoveFromRoleAsync(userToEdit, rol.Name);
                    }
                    await _userManager.AddToRoleAsync(userToEdit, model.Rol);
                    // Bitacora
                    _context.Bitacora.Add(new Bitacora() { Message = $"Crea usuario con Id: {userToEdit.Id} y Correo: {userToEdit.Email}", TimeStamp = DateTime.Now, UserName = User.Identity.Name });

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    await _errorlog.ErrorLogAsync(ex.Message);
                    ModelState.AddModelError("", _localizer["No ha sido posible editar el usuario. Inténtelo nuevamente."]);
                }
            }

            // Se inicia el modelo en caso de error
            EditarViewModel usuario = new EditarViewModel()
            {
                Id = userToEdit.Id,
                Email = userToEdit.Email,
                Nombrecompleto = userToEdit.Nombrecompleto,
                Adscripcion = userToEdit.Adscripcion,
                UserName = userToEdit.UserName,
                ActiveDirectoryEnabled = userToEdit.ActiveDirectoryEnabled,
                CardsViewEnabled = userToEdit.CardsViewEnabled,
                Rol = (await _userManager.GetRolesAsync(userToEdit)).FirstOrDefault()
            };

            ViewData["RolesSelect"] = GetRolesAsync(usuario.Rol);
            ViewData["ControlBoleano"] = PopulateItems.GetBoleanoSelect();
            //PopulateAssignedLineasTematicas(userToEdit);

            return View(usuario);
        }

        public async Task<IActionResult> CambiarPasswordAsync(string id)
        {
            var usuarioToEdit = await _context.Users.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (usuarioToEdit == null)
            {
                return NotFound();
            }

            CambiarPasswordViewModel usuario = new CambiarPasswordViewModel()
            {
                Id = usuarioToEdit.Id,
                Email = usuarioToEdit.Email,
                Nombrecompleto = usuarioToEdit.Nombrecompleto,
                UserName = usuarioToEdit.UserName,
                ActiveDirectoryEnabled = usuarioToEdit.ActiveDirectoryEnabled,
                CardsViewEnabled = usuarioToEdit.CardsViewEnabled,
                Rol = (await _userManager.GetRolesAsync(usuarioToEdit)).FirstOrDefault()
            };

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarPasswordAsync(string id, CambiarPasswordViewModel model)
        {
            var userToEdit = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
            if (userToEdit == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.RemovePasswordAsync(userToEdit);
                    await _userManager.AddPasswordAsync(userToEdit, model.NewPassword);
                    // Bitacora
                    _context.Bitacora.Add(new Bitacora() { Message = $"Cambia contraseña con usuario con Id: {userToEdit.Id} y Correo: {userToEdit.Email}", TimeStamp = DateTime.Now, UserName = User.Identity.Name });

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    await _errorlog.ErrorLogAsync(ex.Message);
                    ModelState.AddModelError("", _localizer["No ha sido posible cambiar contraseña de usuario. Inténtelo nuevamente."]);
                }
            }

            // Se inicia el modelo en caso de error
            CambiarPasswordViewModel usuario = new CambiarPasswordViewModel()
            {
                Id = userToEdit.Id,
                Email = userToEdit.Email,
                Nombrecompleto = userToEdit.Nombrecompleto,
                UserName = userToEdit.UserName,
                ActiveDirectoryEnabled = userToEdit.ActiveDirectoryEnabled,
                CardsViewEnabled = userToEdit.CardsViewEnabled,
                Rol = (await _userManager.GetRolesAsync(userToEdit)).FirstOrDefault()
            };

            return View(usuario);
        }

        public async Task<IActionResult> EliminarAsync(string id)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(r => r.Id == id && r.Deleted == false);

            if (userToDelete == null)
            {
                return NotFound();
            }
            //PopulateAssignedLineasTematicas(userToDelete);

            return View(userToDelete);
        }

        [HttpPost]
        [ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarPostAsync(string id)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await PuedeEliminarAsync(userToDelete.Id))
                    {
                        await _userManager.DeleteAsync(userToDelete);
                    }
                    else
                    {
                        userToDelete.Deleted = true;
                        foreach (var rol in await _context.Roles.ToListAsync())
                        {
                            await _userManager.RemoveFromRoleAsync(userToDelete, rol.Name);
                        }
                    }

                    // Bitacora
                    _context.Bitacora.Add(new Bitacora() { Message = $"Elimina usuario con Id: {userToDelete.Id} y Correo: {userToDelete.Email}", TimeStamp = DateTime.Now, UserName = User.Identity.Name });
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    await _errorlog.ErrorLogAsync(ex.Message);
                    ModelState.AddModelError("", _localizer["No ha sido posible eliminar el usuario. Inténtelo nuevamente."]);
                }
            }

            // Se inicia el modelo en caso de error
            userToDelete = await _context.Users.FirstOrDefaultAsync(r => r.Id == id && r.Deleted == false);
            //PopulateAssignedLineasTematicas(userToDelete);
            return View(userToDelete);
        }

        public async Task<SelectList> GetRolesAsync(string Rol = null)
        {
            //if (Convert.ToBoolean(_opciones.HabilitaJurado))
            //{
            //    if (Convert.ToBoolean(_opciones.HabilitaJuradoLineaTematica))
            //        return new SelectList(await _context.Roles.OrderBy(r => r.Name).AsNoTracking().ToListAsync(), "Name", "Name", Rol);

            //    return new SelectList(await _context.Roles.Where(r => r.Name != AdminRolePolicies.CoordinadorMesaRole).OrderBy(r => r.Name).AsNoTracking().ToListAsync(), "Name", "Name", Rol);
            //}

            //return new SelectList(await _context.Roles.Where(r => r.Name == AdminRolePolicies.AdministradorRole && r.Name == AdminRolePolicies.AdministradorGeneralRole).OrderBy(r => r.Name).AsNoTracking().ToListAsync(), "Name", "Name", Rol);
            return new SelectList(await _context.Roles.OrderBy(r => r.Name).AsNoTracking().ToListAsync(), "Name", "Name", Rol);
        }

        public async Task<bool> PuedeEliminarAsync(string Id)
        {
            //if (await _context.Seguimientos.AsNoTracking().CountAsync(u => u.CustomIdentityUserId == Id) > 0)
            //    return false;

            //if (await _context.JuradoCalificaciones.AsNoTracking().CountAsync(u => u.CustomIdentityUserId == Id) > 0)
            //    return false;
            return true;
        }

        private void PopulateAssignedLineasTematicas()
        {
            var viewModel = new List<AssignedJuradoLineaTematicaViewModel>();
            var allLineasTematicas = _context.LineasTematicas.AsNoTracking().ToList();
            foreach (var lineaTematica in allLineasTematicas)
            {
                viewModel.Add(new AssignedJuradoLineaTematicaViewModel
                {
                    LineaTematicaId = lineaTematica.Id,
                    Nombre = lineaTematica.Nombre,
                    Assigned = false
                });
            }

            ViewData["LineasTematicas"] = viewModel;
        }
    }
}
