using System.Collections.Generic;

namespace NetCore.Models
{
    public class GroupJoinResult
    {
        public int categroyId { get; set; }
        public string catergoryName { get; set; }
        public IEnumerable<Product> categoryProductGroup { get; set; }
    }
}