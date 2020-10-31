using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakalarPrace.Models
{
    public class User : IdentityUser
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _message;

        public User()
        {

        }

        public int ID { get { return _id; } set { _id = value; } }
        public string Firstname { get { return _firstname; } set { _firstname = value; } }
        public string Lastname { get { return _lastname; } set { _lastname = value; } }
        public override string Email { get { return _email; } set { _email = value; } }
        public string Message { get { return _message; }set { _message = value; } }
    }
}
