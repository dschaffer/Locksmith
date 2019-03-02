﻿using Sitecore.Data.Items;
using Sitecore.Pipelines.GetPageEditorNotifications;
using Sitecore.Security.AccessControl;
using System.Collections.Generic;
using Unlock_Safely.Extensions;
using Unlock_Safely.Models;

namespace Unlock_Safely.Controllers
{
    public class NotificationsController
    {
        public List<Notification> Get(Item item, bool checkDatasources = true)
        {
            ContentController content = new ContentController();
            string command = content.GetCommand();
            string commandDisplayName = content.GetCommandDisplayName();
            string message;
            AccountController accounts = new AccountController();
            List<Notification> model = new List<Notification>();
            List<Item> items = new List<Item>() { item };

            if (checkDatasources)
                items.AddRange(item.GetDataSources());

            if (items != null && items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Locking.IsLocked())
                    {
                        Account owner = accounts.Get(items[i].Locking.GetOwner());

                        if (owner.Valid && !owner.Admin && !owner.LoggedIn && AuthorizationManager.IsAllowed(items[i], AccessRight.ItemWrite, Sitecore.Context.User))
                        {
                            if (i == 0)
                                message = content.GetUnlockItemMessage(owner.Name, items[i].DisplayName);
                            else
                                message = content.GetUnlockDatasourceMessage(owner.Name, items[i].DisplayName);

                            model.Add(new Notification(items[i], owner, message, command, commandDisplayName));
                        }
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