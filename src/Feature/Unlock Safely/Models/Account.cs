using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unlock_Safely.Models
{
    public class Account
    {
        public bool Admin { get; set; }
        public bool Active { get; set; }
        public bool LoggedIn { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public bool Valid { get; set; }

        public Account()
        {
            Admin = Active = LoggedIn = Valid = false;
        }
    }
}
