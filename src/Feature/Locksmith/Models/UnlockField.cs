namespace Locksmith.Models
{
    public class UnlockField
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
    }
}