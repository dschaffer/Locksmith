using Locksmith.Models;
using Sitecore.Web.Authentication;
using System;
using System.Web.Security;

namespace Locksmith.Controllers
{
    public class AccountController
    {
        public Account Get(string username)
        {
            Account model = new Account();
            SettingsController settings = new SettingsController();

            if (!string.IsNullOrEmpty(username))
            {
                model.User = Sitecore.Security.Accounts.User.FromName(username, false);

                if (model.User != null)
                {
                    model.Valid = true;
                    model.Admin = model.User.IsAdministrator;
                    model.Name = model.User.Profile != null ? model.User.Profile.Name : username;

                    MembershipUser mu = Membership.GetUser(username);

                    if (model.User.Profile != null)
                    {
                        Sitecore.Diagnostics.Log.Warn("Found user (" + username + ") online through last activity date (" + model.User.Profile.LastActivityDate.ToShortDateString() + " " + model.User.Profile.LastActivityDate.ToShortTimeString() + ")", this);
                        model.LoggedIn = model.User.Profile.LastActivityDate.AddMinutes(settings.GetIdleTimeout()) <= DateTime.Now;
                    }
                    else if (mu != null && mu.IsOnline)
                    {
                        Sitecore.Diagnostics.Log.Warn("Found user (" + username + ") online through Membership User", this);
                        model.LoggedIn = true;
                    }
                    else
                    {
                        DomainAccessGuard.Session userSession = DomainAccessGuard.Sessions.Find(session => session.UserName == username);
                        if (userSession == null)
                        {
                            Sitecore.Diagnostics.Log.Warn("Found user (" + username + ") logged out through no DomainAccessGuard session", this);
                            model.LoggedIn = false;
                        }
                        else
                        {
                            Sitecore.Diagnostics.Log.Warn("Found user (" + username + ") online through DomainAccessGuard last request " + userSession.LastRequest.ToString(), this);
                            model.LoggedIn = userSession.LastRequest.AddMinutes(settings.GetIdleTimeout()) <= DateTime.Now;
                        }
                    }
                }
            }

            return model;
        }
    }
}