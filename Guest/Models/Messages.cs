namespace Guest.Models
{
    public class Messages
    {

        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }

        public Users? User { get; set; }
    }


}
