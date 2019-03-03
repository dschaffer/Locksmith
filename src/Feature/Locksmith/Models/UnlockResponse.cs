using System.Collections.Generic;

namespace Locksmith.Models
{
    public class UnlockResponse
    {
        public string Id { get; set; }
        public List<UnlockField> Fields { get; set; }

        public UnlockResponse()
        {
            Fields = new List<UnlockField>();
        }
    }
}