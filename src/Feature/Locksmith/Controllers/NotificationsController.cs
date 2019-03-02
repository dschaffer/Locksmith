using Locksmith.Extensions;
using Locksmith.Models;
using Sitecore.Data.Items;
using Sitecore.Pipelines.GetPageEditorNotifications;
using System.Collections.Generic;

namespace Locksmith.Controllers
{
    public class NotificationsController
    {
        public List<Notification> Get(Item item, bool checkDatasources = true)
        {
            SettingsController content = new SettingsController();
            string command = content.GetCommand();
            string commandDisplayName = content.GetCommandDisplayName();
            string message;
            AccountController accounts = new AccountController();
            SecurityController security = new SecurityController();
            List<Notification> model = new List<Notification>();
            List<Item> items = new List<Item>() { item };

            if (checkDatasources)
                items.AddRange(item.GetDataSources());

            if (items != null && items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (security.IsUnlockable(items[i]))
                    {
                        Account owner = accounts.Get(items[i].Locking.GetOwner());

                        if (i == 0)
                            message = content.GetUnlockItemMessage(owner.Name, items[i].DisplayName);
                        else
                            message = content.GetUnlockDatasourceMessage(owner.Name, items[i].DisplayName);

                        model.Add(new Notification(items[i], owner, message, command, commandDisplayName));
                    }
                }
            }

            return model;
        }

        public List<PageEditorNotification> GetPageEditorNotifications(Item item, bool checkDatasources = true)
        {
            List<PageEditorNotification> model = new List<PageEditorNotification>();
            List<Notification> notifications = Get(item, checkDatasources);

            if (notifications.Count > 0)
            {
                foreach (Notification notification in notifications)
                {
                    var editorNotification = new PageEditorNotification(notification.Message, PageEditorNotificationType.Warning)
                    {
                        Icon = Constants.WarningIcon,
                    };
                    editorNotification.Options.Add(new PageEditorNotificationOption(notification.CommandDisplayName, notification.Command));

                    model.Add(editorNotification);
                }
            }

            return model;
        }
    }
}