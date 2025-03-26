using System.Text.Json.Serialization;

namespace Digital_Menu.Models
{
    public class OrderItem
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public decimal Price { get; set; } // Cena za jedan proizvod

        public int OrderId { get; set; }

        [JsonIgnore] // da se izbegne ciklus prilikom slanja ka frontu
        public Order Order { get; set; }
    }


}
