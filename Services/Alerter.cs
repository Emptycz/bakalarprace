﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakalarPrace.Extensions;
using BakalarPrace.ExceptionModel;

namespace BakalarPrace.Services
{
    public class Alerter
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        private static HttpContext _http { get; set; }

        public Alerter()
        {

        }

        public Alerter(string title, string text, string status, HttpContext http)
        {
            Title = title;
            Text = text;
            Status = status;
            _http = http;
            this._setClass();

            this._createMessage();
        }

        public Alerter(string text, string stat, HttpContext http)
        {
            Text = text;
            Status = stat;
            this._setClass();
            _http = http;

            this._createMessage();
        }

        public Alerter(LogMessage lm, HttpContext http)
        {
            Text = lm.Message;
            Status = lm.Status;
            this._setClass();
            _http = http;
            Title = lm.Name;
        }
        public bool CheckMessageExistence()
        {
            if (_http.Request.Cookies["SystemResponse.Cookie"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteMessage()
        {
            try
            {
                _http.Response.Cookies.Delete("SystemResponse.Cookie");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void _createMessage()
        {
            if (this.CheckMessageExistence())
            {
                //HttpContext.Response.Cookies.Delete("SystemResponse.Cookie");
                _http.Response.Cookies.Delete("SystemResponse.Cookie");
            }
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(1);
            var t = new Dictionary<string, string>()
            {
                {"Title", this.Title },
                {"Text", this.Text },
                {"Class", this.Class },
                {"Status", this.Status }
            };

            _http.Response.Cookies.Append("SystemResponse.Cookie", t.ToLegacyCookieString(), option);

        }

        private void _setClass()
        {
            switch (this.Status.ToLower())
            {
                case "ok":
                case "OK":
                    this.Class = "bg-success";
                    break;

                case "error":
                case "Error":
                    this.Class = "bg-danger";
                    break;

                case "warning":
                case "Warning":
                    this.Class = "bg-warning";
                    break;

                case "Note":
                case "note":
                default:
                    this.Class = "bg-primary";
                    break;
            }
        }
    }
}
