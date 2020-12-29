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

namespace BakalarPrace.Controllers
{
   [Authorize]
   [ViewLayout("_AdminLayout")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            Database db = new Database();
            ViewBag.Records = db.GetFourLatestRecords();
            return View();
        }

        public IActionResult Imports()
        {
            Database db = new Database();
            List<Record> rc = db.GetRecords();
            ViewBag.Records = rc;
            return View();
        }

        [HttpPost]
        public IActionResult Imports(string h)
        {
            var reader = new StreamReader("wwwroot/uploads/3d25a565-602a-4478-8b64-c587249595c1.csv");
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Configuration.Delimiter = ";";
            csv.Configuration.HasHeaderRecord = true;
            csv.Configuration.HeaderValidated = null;
            csv.Configuration.MissingFieldFound = null;

            var records = csv.GetRecords<CsvRow>();
            ViewBag.Records = records.ToList();
            Database db = new Database();
            db.AddReport(0, "Test", ViewBag.Records);
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

                return RedirectToAction("Imports", "Admin");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Imports", "Admin");
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
            return View(rc);
        }

    }
}
