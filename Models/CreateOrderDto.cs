namespace Digital_Menu.Models
{
    public class CreateOrderDto
    {
        public int TableId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
