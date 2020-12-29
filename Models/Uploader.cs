﻿using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BakalarPrace.Models
{
    public class Uploader
    {
        private DateTime _timeStamp { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string AlertStatus { get; set; }
        public string FilePath { get; set; }
        public string Delimeter { get; set; }

        public Uploader()
        {
            FileName = "Název souboru";
            Status = "Čekám na upload";
            AlertStatus = "alert-info";
        }

        public Uploader(string filename)
        {
            FileName = filename;
            FilePath = "wwwroot/uploads/" + FileName;
            Status = "Čekám na upload";
            AlertStatus = "alert-info";
        }

        public void SetIncorrectExtensionMessage(string ext)
        {
            Status = "Je možné nahrát soubory pouze s příponou .csv (Váš soubor měl příponu: "+ext+")";
            AlertStatus = "alert-danger";
        }

        public bool CheckFileExistence(string filename)
        {
            this.FileName = filename;
            string path = "wwwroot/uploads/" + this.FileName;
            bool existenceOfFile = File.Exists(path);
            if (existenceOfFile == true)
            {
                FileName = this.FileName;
                Status = "Soubor byl úspěšně nahrán. Oddělovač je: " + Delimeter;
                AlertStatus = "alert-success";
                return true;
            }
            else
            {
                Status = "Soubor nebyl nalezen";
                AlertStatus = "alert-danger";
                return false;
            }

        }

        public bool Delete()
        {
            if(this.FileName == null)
            {
                return false;
            }

            this.FilePath = "wwwroot/uploads/" + this.FileName;
            File.Delete(this.FilePath);
            if (this.CheckFileExistence(this.FileName) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
