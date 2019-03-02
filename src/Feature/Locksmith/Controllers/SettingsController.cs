using Sitecore.Data.Items;
using System.Collections.Generic;

namespace Locksmith.Controllers
{
    public class SettingsController
    {
        private Item Settings { get; set; }

        public string GetCommand()
        {
            string model = Constants.DefaultCommand;

            if (Settings == null)
                Settings = Dbs.Db.GetItem(Constants.SettingsId);
            if (Settings != null)
                model = Settings["Command"];

            return model;
        }

        public string GetCommandDisplayName()
        {
            string model = Constants.DefaultCommandDisplayName;

            if (Settings == null)
                Settings = Dbs.Db.GetItem(Constants.SettingsId);
            if (Settings != null)
                model = Settings["Command Display Name"];

            return model;
        }

        public double GetIdleTimeout()
        {
            double model = Constants.IdleTimeout;
            double test;

            if (Settings == null)
                Settings = Dbs.Db.GetItem(Constants.SettingsId);
            if (Settings != null && double.TryParse(Settings["Idle Timeout"], out test))
                model = test;

            return model;
        }

        public List<string> GetValidWorkflowStates()
        {
            List<string> model = new List<string>();

            if (Settings == null)
                Settings = Dbs.Db.GetItem(Constants.SettingsId);
            if (Settings != null)
                model = GetList(Settings["Valid Workflow States"]);

            return model;
        }

        public string GetUnlockDatasourceMessage(string ownerName, string itemName)
        {
            string model = string.Format(Constants.DefaultUnlockDatasourceMessage, itemName, ownerName);

            if (Settings == null)
                Settings = Dbs.Db.GetItem(Constants.SettingsId);
            if (Settings != null)
                model = string.Format(Settings["Unlock Datasource Message"], itemName, ownerName);

            return model;
        }

        public string GetUnlockItemMessage(string ownerName, string itemName)
        {
            string model = string.Format(Constants.DefaultUnlockItemMessage, itemName, ownerName);

            if (Settings == null)
                Settings = Dbs.Db.GetItem(Constants.SettingsId);
            if (Settings != null)
                model = string.Format(Settings["Unlock Item Message"], itemName, ownerName);

            return model;
        }

        private static List<string> GetList(string value)
        {
            List<string> models = new List<string>();

            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value.Trim().Trim('|')))
                models.AddRange(value.Trim().Trim('|').Split('|'));

            return models;
        }
    }
}