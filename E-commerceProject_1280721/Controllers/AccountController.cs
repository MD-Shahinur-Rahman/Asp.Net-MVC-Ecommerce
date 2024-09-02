using E_commerceProject_1280721.DAL;
using E_commerceProject_1280721.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_commerceProject_1280721.Controllers
{
    
    
        [Authorize]
        public class AccountController : Controller
        {
            private ApplicationSignInManager _signInManager;
            private ApplicationUserManager _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public AccountController()
            {
            }

            public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, RoleManager<IdentityRole> roleManager)
            {
                UserManager = userManager;
                SignInManager = signInManager;
                _roleManager = roleManager;
            }

            public ApplicationSignInManager SignInManager
            {
                get
                {
                    return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                }
                private set
                {
                    _signInManager = value;
                }
            }

            public ApplicationUserManager UserManager
            {
                get
                {
                    return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                private set
                {
                    _userManager = value;
                }
            }

            //
            // GET: /Account/Login
            [AllowAnonymous]
            public ActionResult Login(string returnUrl)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await UserManager.FindByNameAsync(model.Email);
                    var roles = await UserManager.GetRolesAsync(user.Id);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (roles.Contains("Customer"))
                    {
                        return RedirectToAction("Index", "Cart");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
            public ActionResult Register()
            {
                return View();
            }

            //
            // POST: /Account/Register
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Register(RegisterViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        // Assign 'Customer' role to the user upon successful registration
                        var roleResult = await UserManager.AddToRoleAsync(user.Id, "Customer");
                        if (!roleResult.Succeeded)
                        {
                            AddErrors(roleResult);
                            return View(model);
                        }

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }

                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult LogOff()
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Index", "Home");
            }

            private IAuthenticationManager AuthenticationManager
            {
                get
                {
                    return HttpContext.GetOwinContext().Authentication;
                }
            }

            #region Helpers

            private void AddErrors(IdentityResult result)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            private ActionResult RedirectToLocal(string returnUrl)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            #endregion
        }
    
}