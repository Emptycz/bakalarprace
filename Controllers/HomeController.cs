using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BakalarPrace.Models;
using BakalarPrace.Data;

namespace BakalarPrace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Database db = new Database();
            if(db.IsConnected == false)
            {
                //zatim nedelej nic
            }
            else
            {
                if (db.CheckForAdminUser())
                {
                    ViewBag.IsAdminCreated = true;
                }
                else
                {
                    ViewBag.IsAdminCreated = false;
                }
            }
           
            ViewBag.Message = db.Message;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [Route("{*url}", Order = 999)]
        public IActionResult Page_404()
        {
            Response.StatusCode = 404;
            ViewBag.Message = "Stránka, kterou hledáte, již není k dispozici.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
