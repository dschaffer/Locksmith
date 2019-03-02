using Locksmith.Models;
using Sitecore.Data.Items;
using Sitecore.Security.AccessControl;
using Sitecore.Workflows;
using System.Collections.Generic;
using System.Linq;

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
            if ((!owner.Valid || (owner.Valid && !owner.Admin && !owner.LoggedIn)) && 
                IsWorkflowEditable(item) &&
                AuthorizationManager.IsAllowed(item, AccessRight.ItemWrite, Sitecore.Context.User))
                return true;
            else
                return false;
        }

        public bool IsWorkflowEditable(Item item)
        {
            bool editable = false;
            SettingsController settings = new SettingsController();
            List<string> validStates = settings.GetValidWorkflowStates();

            if (item != null)
            {
                IWorkflow workflow = item.Database.WorkflowProvider.GetWorkflow(item);

                if (workflow == null)
                    editable = true;
                else if (validStates.Count > 0)
                {
                    WorkflowState state = workflow.GetState(item);

                    if (state != null && validStates.Any(t => t == state.StateID))
                        editable = true;
                }
            }

            return editable;
        }
    }
}