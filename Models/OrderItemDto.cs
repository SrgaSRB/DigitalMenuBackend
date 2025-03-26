namespace Digital_Menu.Models
{
    public class OrderItemDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public decimal Price { get; set; } // Cena jednog proizvoda
    }
}
