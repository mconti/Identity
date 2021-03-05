using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityDaZero.Models;

// Aggiungere questa per usare Identity
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IdentityDaZero.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            // if ( !User.Identity.IsAuthenticated )
            //     return RedirectToAction( "Privacy", "Home" );

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            var result1 = await _signInManager.PasswordSignInAsync("posta@maurizioconti.com", "Prova1234!!!", false, false);

            if (!result1.Succeeded)
            {
                // Se non riesce a fare login, crea l'utente
                var user = new IdentityUser { UserName = "posta@maurizioconti.com", Email = "posta@maurizioconti.com" };
                var result2 = await _userManager.CreateAsync(user, "Prova1234!!!");
                if (result2.Succeeded)
                    await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return View("index","home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
