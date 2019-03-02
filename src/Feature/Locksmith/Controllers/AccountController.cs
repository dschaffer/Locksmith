using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locksmith.Models;

namespace Locksmith.Controllers
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