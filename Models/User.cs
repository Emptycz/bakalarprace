using BakalarPrace.Services;
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
        private string _lastname;
        private string _email;
        private string _level;
        private string _password;
        private string _username;

        public User()
        {

        }

        public User(int id, string fname, string sname, string email, string level)
        {
            this._id = id;
            this.Firstname = fname;
            this._lastname = sname;
            this._email = email;
            this._username = email;
            this._level = level;
            this.Message = "Uživatel byl úspěšně přihlášen";
        }

        public bool IsAuthenticated()
        {
            if(this._email != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasRole()
        {
            if(this._level != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HashPassword()
        {
            this.Password = Hasher.ComputeSha256Hash(this.Password);
        }

        public int ID { get { return _id; } set { _id = value; } }
        public string Firstname { get; set; }
        public string Lastname { get { return _lastname; } set { _lastname = value; } }
        public override string Email { get { return _email; } set { _email = value; } }
        public override string UserName { get { return _username; } set { _username = value; } }
        public string Message { get; set; }
        public string Level { get { return _level; } set { _level = value; } }
        public string Password { get { return _password; } set { _password = value; } }
    }
}
