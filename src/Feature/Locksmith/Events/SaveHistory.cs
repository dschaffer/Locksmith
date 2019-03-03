using Locksmith.Controllers;
using Locksmith.Models;
using log4net;
using Sitecore.Data.Items;
using Sitecore.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Locksmith.Events
{
    public class SaveHistory
    {
        private static readonly ILog Logger = LogManager.GetLogger("LocksmithLogger");

        protected void OnItemSaving(object sender, EventArgs args)
        {
            Item newItem = Event.ExtractParameter(args, 0) as Item;

            if (newItem != null && !string.IsNullOrEmpty(newItem.Statistics.UpdatedBy))
            {
                AccountController accounts = new AccountController();
                Account updater = accounts.Get(newItem.Statistics.UpdatedBy);
                Item originalItem = newItem.Database.GetItem(newItem.ID, newItem.Language, newItem.Version);

                if (originalItem != null && originalItem.Locking.IsLocked())
                {
                    var differences = FindDifferences(newItem, originalItem);

                    if (!updater.Admin && differences.Any())
                    {
                        foreach (string difference in differences)
                            Logger.Info(String.Format(" | {0} | {1} | {2} | {3}", updater.Name, newItem.ID.ToString(), originalItem["__Lock"], difference));
                    }
                }
            }
        }

        private static List<string> FindDifferences(Item newItem, Item originalItem)
        {
            newItem.Fields.ReadAll();
            List<string> model = new List<string>();
            IEnumerable<string> fieldNames = newItem.Fields.Where(f => !f.Name.StartsWith("__")).Select(f => f.Name);

            foreach (string fieldName in fieldNames)
            {
                if (newItem[fieldName] != originalItem[fieldName])
                    model.Add(string.Format("{0} | {1} | {2}", fieldName, originalItem[fieldName], newItem[fieldName]));
            }

            return model;
        }
    }
}