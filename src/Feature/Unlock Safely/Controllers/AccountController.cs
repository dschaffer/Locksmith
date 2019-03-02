using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unlock_Safely.Models;

namespace Unlock_Safely.Controllers
{
    public class AccountController
    {
        public Account Get(string username)
        {
            Account model = new Account();

            if (!string.IsNullOrEmpty(username))
            {
                model.User = Sitecore.Security.Accounts.User.FromName(username, false);

                if (model.User != null)
                {
                    model.Valid = true;
                    model.Admin = model.User.IsAdministrator;
                    model.Name = model.User.Profile != null ? model.User.Profile.Name : "";

                }
            }

            return model;
        }
    }
}