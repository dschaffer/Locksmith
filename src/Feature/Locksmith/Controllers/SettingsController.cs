using System.Collections.Generic;

namespace Locksmith.Controllers
{
    public class SettingsController
    {
        public string GetCommand()
        {
            string model = Constants.DefaultCommand;

            // look in sitecore for text value

            return model;
        }

        public string GetCommandDisplayName()
        {
            string model = Constants.DefaultCommandDisplayName;

            // look in sitecore for text value

            return model;
        }

        public List<string> GetValidWorkflowStates()
        {
            List<string> model = new List<string>();



            return model;
        }

        public string GetUnlockDatasourceMessage(string ownerName, string itemName)
        {
            string model = string.Format(Constants.DefaultUnlockDatasourceMessage, itemName, ownerName);

            // look in sitecore for text value

            return model;
        }

        public string GetUnlockItemMessage(string ownerName, string itemName)
        {
            string model = string.Format(Constants.DefaultUnlockItemMessage, itemName, ownerName);

            // look in sitecore for text value

            return model;
        }
    }
}