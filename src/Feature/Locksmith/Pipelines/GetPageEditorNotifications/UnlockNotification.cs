using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetPageEditorNotifications;
using System.Collections.Generic;
using Locksmith.Controllers;

namespace Locksmith.Pipelines.GetPageEditorNotifications
{
    public class UnlockNotification : GetPageEditorNotificationsProcessor
    {
        public override void Process(GetPageEditorNotificationsArgs arguments)
        {
            Assert.ArgumentNotNull(arguments, "arguments");
            if (arguments.ContextItem == null) return;

            NotificationsController controller = new NotificationsController();
            List<PageEditorNotification> notifications = controller.GetPageEditorNotifications(arguments.ContextItem);

            if (notifications.Count > 0)
            {
                for (int i = 0; i < notifications.Count; i++)
                {
                    arguments.Notifications.Add(notifications[i]);
                }
            }
        }
    }
}