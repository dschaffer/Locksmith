using Locksmith.Models;
using Sitecore.Data.Items;
using Sitecore.Security.AccessControl;

namespace Locksmith.Controllers
{
    public class SecurityController
    {
        public bool IsAuthorized()
        {
            if (Sitecore.Context.User != null && Sitecore.Context.User.IsAuthenticated)
                return true;
            else
                return false;
        }

        public bool IsUnlockable(Item item)
        {
            if (item != null && item.Locking.IsLocked())
            {
                AccountController accounts = new AccountController();
                return IsUnlockable(item, accounts.Get(item.Locking.GetOwner()));
            }
            else
                return false;
        }

        public bool IsUnlockable(Item item, Account owner)
        {
            if (owner.Valid && !owner.Admin && !owner.LoggedIn && AuthorizationManager.IsAllowed(item, AccessRight.ItemWrite, Sitecore.Context.User))
                return true;
            else
                return false;
        }
    }
}