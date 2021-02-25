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
        public IActionResult Upload(IFormFile file, string Delimeter, string Location, string Name)
        {
            if(file == null || Delimeter == null || Location == null || Name == null){
                return View();
            }
            else
            {
                this._verifyUploadFolderExistence();
            }
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
                bool result = uploader.CheckFileExistence(FileName);
                Console.WriteLine("Stav uploadu zkontrolován: "+result);
                List<string> delimeter = new List<string>();
                if (Delimeter == "auto")
                {
                    delimeter.Add(";");
                    delimeter.Add(",");
                    delimeter.Add("\t");
                }
                else
                {
                    if (Delimeter == "tabulator")
                    {
                        Delimeter = "\t";
                    }
                    else
                    {
                        delimeter.Add(Delimeter);
                    }
                }

                if (result)
                {
                    int iteration = 0;
                    bool importDoneCorrectly = false;
                    while (importDoneCorrectly == false)
                    {
                        if (this._processCSV(FileName, Name, Location, delimeter[iteration]) == true){
                            importDoneCorrectly = true;
                        }
                        else
                        {
                            iteration++;
                        }

                        if(iteration == delimeter.Count())
                        {
                            break;
                        }
                        Console.WriteLine(importDoneCorrectly);
                    }
                    if(importDoneCorrectly == false)
                    {
                        LogMessage lm = new LogMessage("Zpracování CSV", "500", "Nebylo možné přečíst CSV soubor. Zkustě jiný oddělovač", "Error");
                        new Alerter(lm, HttpContext);
                    }
                    else
                    {
                        LogMessage lm = new LogMessage("Zpracování CSV", "200", "CSV bylo zpracováno a nahráno", "OK");
                        new Alerter(lm, HttpContext);
                    }
                    //Odstraň CSV soubor
                    uploader.Delete();
                    return RedirectToAction("Imports", "Admin");
                }
                else
                {
                    //Odstraň CSV soubor
                    uploader.Delete();

                    return View(uploader);
                }
            }
            return View(uploader);
        }

        private bool _verifyUploadFolderExistence()
        {
            string path = "wwwroot/uploads/";
            if(Directory.Exists(path) == true)
            {
                return true;
            }
            else
            {
                Directory.CreateDirectory(path);
                return true;
            }
        }

        private bool _processCSV(string filename, string name, string location, string delimeter)
        {
            Database db = new Database();

            using (var reader = new StreamReader("wwwroot/uploads/" + filename))
            {
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                csv.Configuration.Delimiter = delimeter;
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                try
                {
                    var records = csv.GetRecords<CsvRow>();
                    var data = records.ToList();

                    //Získání přihlášeného uživatele
                    var email = _signInManager.Context.User.Identity.Name;
                    User user = db.GetUserByEmail(email);

                    LogMessage lm = db.AddReport(user.ID, name, location, data);
                    new Alerter(lm, HttpContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }
                
        }

    }
}
