using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unlock_Safely.Controllers
{
    public class ContentController
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