namespace Chat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string From { get; set; }
        //public int UserId { get; set; }
        public DateTime dateTime { get; set; }

    }
}
