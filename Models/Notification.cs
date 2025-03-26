namespace Digital_Menu.Models
{
    public class Notification
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public string LocalId { get; set; }
        public Local Local { get; set; }

    }
}