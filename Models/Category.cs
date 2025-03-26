namespace Digital_Menu.Models
{
    public class Category
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public bool Images { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public string LocalId { get; set; }
        public Local Local { get; set; }

    }
}