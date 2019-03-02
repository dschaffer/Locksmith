using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unlock_Safely.Models
{
    public class Notification
    {
        public string Command { get; set; }
        public string CommandDisplayName { get; set; }
        public Item Item { get; set; }
        public string Message { get; set; }
        public Account Owner { get; set; }

        public Notification(Item item, Account owner, string message = Constants.DefaultUnlockItemMessage, string command = Constants.DefaultCommand, string commandDisplayName = Constants.DefaultCommandDisplayName)
        {
            Command = command;
            CommandDisplayName = commandDisplayName;
            Item = item;
            Message = message;
            Owner = owner;
        }
    }
}