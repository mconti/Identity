using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
// Serve per usare 
// - IdentityUser   - Oggetto con tutte le property utili a gestire un utente
// - SignInManager  - Oggetto con tanti metodi per gestire la fase di SignIn
// - UserManager    - Oggetto con diversi metodi per creare, cercare, gestire gli utenti che sono nel db

// Per creare db EF con tutte le tabelle per gestire gli utenti si usa IdentityDbContext
// Lo troviamo qui
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using conti.maurizio.Models;

namespace conti.maurizio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                     //await _signInManager.SignInAsync(user, isPersistent: false);
                     return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
               var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

               if (result.Succeeded) {
                   // Se l'utente fa login correttamente, entra.
                   return RedirectToAction("Index", "Home");
               }
               else{

                   // ...altrimenti meglio non dare troppo info a chi ci prova
                   // meglio un generico errore,
                   ModelState.AddModelError(string.Empty, "Login error");

                /* Volendo (ma non è consigliato) 
                   si può indagare sul fatto che la EMail sia o meno confermata.)

                   var u = await _userManager.FindByNameAsync( user.Email.TrimEnd() );
                   if( u != null ) {
                        bool EmailConfermata = await _signInManager.UserManager.IsEmailConfirmedAsync( u );
                        if( !EmailConfermata )
                           ModelState.AddModelError(string.Empty, "EMail non sia confermata");
                   }
                */

               }
            }
            return View(user);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
