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

namespace BakalarPrace.Controllers
{
   //[Authorize]
   [ViewLayout("_AdminLayout")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
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
        public IActionResult ShowRecord(int RecordId)
        {
            Database db = new Database();
            Record rc = db.GetDetailedRecord(RecordId);
            return View(rc);
        }

    }
}
