using Locksmith.Controllers;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Specialized;

namespace Locksmith.Commands
{
    [Serializable]
    public class ReviewAndUnlock : Command
    {
        public override CommandState QueryState(CommandContext context)
        {
            CommandState currentState = CommandState.Enabled;
            SecurityController security = new SecurityController();

            if (!security.IsAuthorized())
                currentState = CommandState.Disabled;
            else if (context.Items.Length > 0)
            {
                Item item = context.Items[0];

                if (!security.IsUnlockable(item))
                    currentState = CommandState.Disabled;
            }
            else
                currentState = base.QueryState(context);

            return currentState;
        }

        public override void Execute(CommandContext context)
        {
            if (context.Items.Length > 0)
            {
                Item item = context.Items[0];
                NameValueCollection parameters = new NameValueCollection();
                parameters["id"] = item.ID.ToString();
                parameters["language"] = item.Language.ToString();
                parameters["database"] = item.Database.Name;
                Sitecore.Context.ClientPage.Start(this, "Run", parameters);
            }
        }

        protected void Run(ClientPipelineArgs args)
        {
            SecurityController security = new SecurityController();

            if (security.IsAuthorized())
            {
                if (args.IsPostBack)
                {
                    Sitecore.Context.ClientPage.SendMessage(this, "item:refresh");
                    return;
                }
                else
                {
                    Sitecore.Text.UrlString popUpUrl = new Sitecore.Text.UrlString("/sitecore%20modules/shell/locksmith/modal.html");
                    popUpUrl.Append("id", args.Parameters["id"]);
                    popUpUrl.Append("database", args.Parameters["database"]);
                    popUpUrl.Append("language", args.Parameters["language"]);
                    SheerResponse.ShowModalDialog(popUpUrl.ToString(), "600", "450");
                    args.WaitForPostBack();
                }
            }
        }
    }
}