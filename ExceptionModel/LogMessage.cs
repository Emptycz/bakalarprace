using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakalarPrace.ExceptionModel
{
    public class LogMessage
    {
        public string Name { get; set; } //Name of function
        public string Code { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

        public LogMessage(string name, string code, string message, string status)
        {
            Name = name;
            Code = code;
            Message = message;
            Status = status;
        }


    }
}
