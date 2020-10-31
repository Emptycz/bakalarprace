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

namespace BakalarPrace.Controllers
{
    [ViewLayout("_AdminLayout")]
    public class UploaderController : Controller
    {
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
        public IActionResult Upload(IFormFile file, string Delimeter)
        {
            Uploader uploader = new Uploader();
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

                //Generate url to save
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", FileName);
                uploader.FilePath = SavePath;
                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                //Check status of upload
                uploader.CheckUpload(FileName);
            }

            return View(uploader);
        }

    }
}
