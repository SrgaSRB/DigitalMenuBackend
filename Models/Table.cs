using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Menu.Models
{
    //[Table("Tables")] // obavezno jer "Tables" nije plural konvencija EF-a
    public class Table
    {
        public int Id { get; set; } // Broj stola
        public string LocalId { get; set; }
        public Local Local { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}
