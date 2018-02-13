using SQLite;

namespace PheonixSelector.Models
{
    public class Item
    {
        [PrimaryKey]
        public string ItemCode { get; set; }
        public string CategoryCode { get; set; }
        public string ItemName { get; set; }
    }
}
