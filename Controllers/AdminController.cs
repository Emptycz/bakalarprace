using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BakalarPrace.Models;
using BakalarPrace.Data;
using Microsoft.AspNetCore.Authorization;
using BakalarPrace.Services;
using BakalarPrace.ExceptionModel;
using BakalarPrace.Extensions;

namespace BakalarPrace.Controllers
{
    [Authorize(Roles ="Admin")]
    [ViewLayout("_AdminLayout")]
    public class AdminController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        public AdminController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Database db = new Database();
            var email = _signInManager.Context.User.Identity.Name;
            User usr = db.GetUserByEmail(email);
            ViewBag.Records = db.GetFourLatestRecords(usr.ID);
            return View(usr);
        }

        public IActionResult Imports()
        {
            Database db = new Database();
            List<Record> allRecords = db.GetAllRecords();
            ViewBag.Records = allRecords;
            return View();
        }

        [HttpGet]
        public IActionResult RemoveImport(string RecordId)
        {
            if(RecordId == null)
            {
                return RedirectToAction("Imports", "Admin");
            }
            try
            {
                int recordId = Int32.Parse(RecordId);
                Database db = new Database();

                LogMessage lm = db.RemoveReport(recordId);
                new Alerter(lm, HttpContext);

                return RedirectToAction("Imports", "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Imports", "Admin");
            }
        }

        public IActionResult Users()
        {
            Database db = new Database();
            List<User> allUsers = db.GetAllUsers();
            if(allUsers.Count == 0)
            {
                new Alerter(new LogMessage("Zobrazení uživatelů", "500", "Nastala neočekávaná chyba aplikace", "Error"), HttpContext);
            }
            ViewBag.Users = allUsers;
            return View();
        }

        public IActionResult RemoveUser(int id)
        {
            Database db = new Database();
            if(db.VerifyUserExistenceByID(id) == false)
            {
                return RedirectToAction("Users");
            }

            new Alerter(db.RemoveUser(id), HttpContext);
            return RedirectToAction("Users");
        }
    }
}
