namespace NetCore.Models
{
    public class Product
    {
        public int id { get; set; }
        public int categoryId { get; set; }
        public string name { get; set; }
        public int price { get; set; }
    }
}