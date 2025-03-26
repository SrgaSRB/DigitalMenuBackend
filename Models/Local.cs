namespace Digital_Menu.Models
{
    public class Local
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Package { get; set; } //basic / standard / premium

        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Table> Tables { get; set; } = new List<Table>();
    }
}
