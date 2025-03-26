namespace Digital_Menu.Models
{
    public class Product
    {

        public string Id { get; set; } // možeš koristiti string, GUID ili int
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

    }
}