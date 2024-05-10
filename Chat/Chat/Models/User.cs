namespace Chat.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public string? Name { get; set; }
        public bool IsLoggedIn { get; set; } // флаг, который определяет, вошел ли пользователь
        
    }
}
