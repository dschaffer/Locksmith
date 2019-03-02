namespace Locksmith
{
    public static class Constants
    {
        public const string ContentEditorUrlFormat = "{0}://{1}/sitecore/shell/Applications/Content Editor?id={2}&amp;sc_content=master&amp;fo={2}&amp;vs={3}&amp;la={4}";
        public const string DefaultCommand = "item:ReviewAndUnlock";
        public const string DefaultCommandDisplayName = "Review Changes and Unlock";
        public const string DefaultUnlockDatasourceMessage = "Datasource ({0}) is locked by {1} but can be unlocked.";
        public const string DefaultUnlockItemMessage = "Item ({0}) is locked by {1} but can be unlocked.";
        public const string SettingsId = "";
        public const string WarningIcon = "/sitecore/shell/themes/standard/Images/warning_yellow.png";
    }
}