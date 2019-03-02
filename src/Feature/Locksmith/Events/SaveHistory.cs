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

            if (newItem == null)
            {
                return;
            }

            Item originalItem = newItem.Database.GetItem(newItem.ID, newItem.Language, newItem.Version);

            var differences = FindDifferences(newItem, originalItem);

            if (differences.Any())
            {
                string message = String.Format("Item content changed [{0}]", String.Join(", ", differences));

                Logger.Info(message);
            }
        }

        private static List<string> FindDifferences(Item newItem, Item originalItem)
        {
            newItem.Fields.ReadAll();

            IEnumerable<string> fieldNames = newItem.Fields.Select(f => f.Name);

            return fieldNames
              .Where(fieldName => newItem[fieldName] != originalItem[fieldName])
              .ToList();
        }
    }
}