using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BakalarPrace.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using BakalarPrace.Extensions;
using System.Globalization;
using BakalarPrace.Data;
using CsvHelper;
using BakalarPrace.ExceptionModel;
using BakalarPrace.Services;
using Microsoft.AspNetCore.Identity;

namespace BakalarPrace.Controllers
{
    //[Authorize]
    [ViewLayout("_AdminLayout")]
    public class UploaderController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public UploaderController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            Uploader uploader = new Uploader();
            return View(uploader);
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file, string Delimeter, string Location)
        {
            Console.WriteLine("Načtení controlleru a souboru: "+file.FileName);
            Uploader uploader = new Uploader();
            if(Delimeter == "tabulator")
            {
                Delimeter = "\t";
            }
            uploader.Delimeter = Delimeter;
            if (file != null)
            {
                if(Path.GetExtension(file.FileName) != ".csv")
                {
                    uploader.SetIncorrectExtensionMessage(Path.GetExtension(file.FileName));
                    return View(uploader);
                }
                //Generate file name
                string FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                Console.WriteLine("Vygenerováno jméno souboru");

                //Generate url to save
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", FileName);
                uploader.FilePath = SavePath;
                Console.WriteLine("Příprava na přesun");
               
                try{
                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }catch(Exception ex){
                    Console.WriteLine(ex.Message);
                }


                Console.WriteLine("Přesun kompletní");

                //Check status of upload
                bool result = uploader.CheckUpload(FileName);
                Console.WriteLine("Stav uploadu zkontrolován: "+result);
                if (result)
                {
                    this._processCSV(FileName, Location, Delimeter);

                    return RedirectToAction("Imports", "Admin");
                }
                else
                {
                    return View(uploader);
                }
            }
            return View(uploader);
        }

        private bool _processCSV(string filename, string location, string delimeter)
        {
            var reader = new StreamReader("wwwroot/uploads/"+filename);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Configuration.Delimiter = delimeter;
            csv.Configuration.HasHeaderRecord = true;
            csv.Configuration.HeaderValidated = null;
            csv.Configuration.MissingFieldFound = null;
            
            var records = csv.GetRecords<CsvRow>();
            var data = records.ToList();
            Database db = new Database();

            //Získání přihlášeného uživatele
            var email = _signInManager.Context.User.Identity.Name;
            User user = db.GetUserByEmail(email);

            LogMessage lm = db.AddReport(user.ID, location, data);
            new Alerter(lm, HttpContext);
            return true;
        }

    }
}
