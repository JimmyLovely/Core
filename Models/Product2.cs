namespace NetCore.Models
{
    public class Product2
    {
        public int id { get; set; }
        public Category category { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public Product2 () {
            this.category = new Category{id = 0, name = string.Empty, price = 0};
        }
    }
}