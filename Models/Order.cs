using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Digital_Menu.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; } = "PORUCENO";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int TableId { get; set; }
        [JsonIgnore] // da se izbegne ciklus prilikom slanja ka frontu
        public Table Table { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

}
