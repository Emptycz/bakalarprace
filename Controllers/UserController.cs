using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BakalarPrace.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using BakalarPrace.Extensions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Authorization;
using BakalarPrace.Data;
using System.Reflection;
using BakalarPrace.Services;
using BakalarPrace.ExceptionModel;
using Microsoft.AspNetCore.Identity;

namespace BakalarPrace.Controllers
{
   [Authorize]
   [ViewLayout("_UserLayout")]
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserController(SignInManager<IdentityUser> signInManager)
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
            var email = _signInManager.Context.User.Identity.Name;
            User usr = db.GetUserByEmail(email);
            List<Record> rc = db.GetRecords(usr.ID);
            ViewBag.Records = rc;
            return View();
        }

        [HttpGet]
        public IActionResult RemoveRecord(string RecordId)
        {
            try
            {
                int recordId = Int32.Parse(RecordId);
                Database db = new Database();

                LogMessage lm = db.RemoveReport(recordId);
                new Alerter(lm, HttpContext);

                return RedirectToAction("Imports", "User");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Imports", "User");
            }
        }

        public IActionResult Search()
        {
            CsvRow cs = new CsvRow();
            Type classType = typeof(CsvRow);
            List<string> ClassProperties = new List<string>();
            foreach (PropertyInfo property in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                //Pokud property obsahuje _hodnota, přeskoč ji, není potřeba
                if(property.Name.Contains("_hodnota") || property.Name.Contains("_Hodnota"))
                {
                    continue;
                }
                //Přidej property do listu
                ClassProperties.Add(property.Name);
            }
            ViewBag.ClassProperties = ClassProperties;
            return View();
        }

        [HttpGet]
        [ViewLayout("_CsvExportLayout")]
        public IActionResult ShowRecord(int RecordId)
        {
            Database db = new Database();
            Record rc = db.GetDetailedRecord(RecordId);
            if(rc.ID == 0)
            {
                new Alerter("Zobrazení záznamu", "Nepodařilo se dohledat žádaný záznam", "ERROR", HttpContext);
                return RedirectToAction("Imports");
            }
            var email = _signInManager.Context.User.Identity.Name;
            User usr = db.GetUserByEmail(email);
            try
            {
                if (rc.Author.ID != usr.ID)
                {
                    new Alerter("Zobrazení záznamu", "Nemáte dostatečné oprávnění k zobrazení toho záznamu", "ERROR", HttpContext);
                    return RedirectToAction("Imports");
                }
                else
                {
                    return View(rc);
                }
            }catch(Exception)
            {
                new Alerter("Zobrazení záznamu", "Došlo k chybě při pokusu o ověření oprávnění", "ERROR", HttpContext);
                return RedirectToAction("Imports");
            }
            
        }

    }
}
