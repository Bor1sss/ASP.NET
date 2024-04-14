namespace Guest.Models
{
    public class CombinedMessages
    {
        public Messages? MessageModel { get; set; }
        public IEnumerable<Messages>? Messages { get; set; }
    }
}
