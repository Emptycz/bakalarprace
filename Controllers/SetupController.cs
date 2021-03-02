using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BakalarPrace.Data;
using BakalarPrace.Services;
using BakalarPrace.ExceptionModel;
using BakalarPrace.Models;

namespace BakalarPrace.Controllers
{
    public class SetupController : Controller
    {
        public IActionResult Index()
        {
            Database db = new Database();
            if(db.CheckForAdminUser())
            {
                new Alerter(new LogMessage("Prvotní nastavení", "404", "Možnost prvotní nastavení je deaktivována, protože již existuje účet správce", "Error"),HttpContext);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("SetAdmin", "Setup");
            }
        }

        public IActionResult SetAdmin()
        {
            Database db = new Database();
            if (db.CheckForAdminUser())
            {
                new Alerter(new LogMessage("Prvotní nastavení", "404", "Možnost prvotní nastavení je deaktivována, protože již existuje účet správce", "Error"), HttpContext);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(new User());
            }
        }
    
        [HttpPost]
        public IActionResult SetAdmin(User user, string verifyPassword)
        {
            Database db = new Database();
            if (db.CheckForAdminUser())
            {
                new Alerter(new LogMessage("Prvotní nastavení", "404", "Možnost prvotního nastavení je deaktivována, protože již existuje účet správce", "Error"), HttpContext);
                return RedirectToAction("Index", "Home");
            }

            if (user.Password != verifyPassword)
            {
                user.Message = "Hesla se neshodují!";
                return View(user);
            }

            if (db.VerifyUserExistenceByEmail(user.Email) == true)
            {
                user.Message = "Uživatel s tímto emailem již existuje!";
                return View(user);
            }

            user.HashPassword();
            //Nastavit admin úroveň
            user.Level = "Admin";

            bool result = db.RegisterUser(user);
            if (result)
            {
                new Alerter(new LogMessage("Dokončení nastavení", "200", "Byl úspěšně vytvořen účet s oprávněním administrátora", "OK"), HttpContext);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                new Alerter(new LogMessage("Dokončení nastavení", "500", "Nastala chyba při pokusu o vytvoření nového účtu", "Error"), HttpContext);
                return View(user);
            }
        }
    }
}
