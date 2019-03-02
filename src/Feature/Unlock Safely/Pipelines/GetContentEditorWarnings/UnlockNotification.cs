using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetContentEditorWarnings;
using System.Collections.Generic;
using Unlock_Safely.Controllers;
using Unlock_Safely.Extensions;
using Unlock_Safely.Models;

namespace Unlock_Safely.Pipelines.GetContentEditorWarnings
{
    public class UnlockNotification
    {
        public void Process(GetContentEditorWarningsArgs arguments)
        {
            Assert.ArgumentNotNull(arguments, "arguments");
            if (arguments.Item == null) return;

            NotificationsController controller = new NotificationsController();
            List<Notification> notifications = notifications = controller.Get(arguments.Item, arguments.Item.HasPresentation());

            if (notifications.Count > 0)
            {
                for (int i = 0; i < notifications.Count; i++)
                {
                    var notification = arguments.Add();
                    notification.Text = notifications[i].Message;
                    notification.Icon = Constants.WarningIcon;
                    notification.AddOption(notifications[i].CommandDisplayName, notifications[i].Command);
                }
            }
        }
    }
}