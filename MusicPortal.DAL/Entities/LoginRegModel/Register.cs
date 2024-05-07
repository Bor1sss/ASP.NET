using System.ComponentModel.DataAnnotations;

namespace MusicPortal.DAL.Entities.LoginRegModel
{
    public class Register
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
