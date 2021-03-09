using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BakalarPrace.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BakalarPrace.Services;
using Microsoft.AspNetCore.Identity;
using BakalarPrace.Models;

namespace BakalarPrace.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }

                if (User.IsInRole("User"))
                {
                    return RedirectToAction("Index", "User");
                }
                return View();
            }

            return View();
        }

        public IActionResult Register()
        {
            User us = new User();
            return View(us);
        }

        [HttpPost]
        public IActionResult Register(User user, string verifyPassword)
        {

            Database db = new Database();
            if (db.IsConnected == false)
            {
                user.Message = "Není možné navázat spojení s databází. Kontaktujte prosím administrátora.";
                return View(user);
            }

            if (user.Password != verifyPassword)
            {
                user.Message = "Hesla se neshodují!";
                return View(user);
            }
            user.HashPassword();

            if (db.VerifyUserExistenceByEmail(user.Email) == true)
            {
                user.Message = "Uživatel s tímto emailem již existuje!";
                return View(user);
            }

            bool result = db.RegisterUser(user);
            if (result)
            {
                new Alerter("Účet úspěšně vytvořen", "Váš účet byl úspěšně vytvořen, nyní se můžete přihlásit.", "OK", HttpContext);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if(username == null || password == null)
            {
                ViewBag.Message = "Nevyplnili jste všechny pole.";
                return View();
            }

            Database db = new Database();
            if(db.IsConnected == false)
            {
                ViewBag.Message = "Není možné navázat spojení s databází. Kontaktujte prosím administrátora.";
                return View();
            }
            string pswd = Hasher.ComputeSha256Hash(password);
            //Získání záznamu uživatele z db
            User user = db.LoginUser(username, pswd);

            //Zkontroluj, jestli byl uživatel nalezen v DB
            if (user.IsAuthenticated() == false)
            {
                ViewBag.Message = "Zadali jste špatné jméno nebo heslo.";
                return View();
            }

            bool adminRole = await _roleManager.RoleExistsAsync("Admin");
            bool userRole = await _roleManager.RoleExistsAsync("User");

            if (!adminRole)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);
            }

            if (!userRole)
            {
                var role = new IdentityRole();
                role.Name = "User";
                await _roleManager.CreateAsync(role);
            }

            //Přiřaď uživateli roli
            if (user.HasRole())
            {
                var resultSettingRole = await _userManager.AddToRoleAsync(user, user.Level);
            }
            else
            {
                var resultSettingRole = await _userManager.AddToRoleAsync(user, "User");
            }

            //Přihlaš uživatele
            await _signInManager.SignInAsync(user, false);
            //await _signInManager.PasswordSignInAsync(user, user.Password, false, false);
            new Alerter("Vítejte zpět", "Byl jste úspěšně přihlášen, vítejte zpět uživateli "+user.Firstname +" "+user.Lastname, "OK", HttpContext);
            if (user.IsAdmin())
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            new Alerter("Byl jste úspěšně odhlášen", "Info", HttpContext);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
