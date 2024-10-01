using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.BusinessEntities
{
    public class CreateUser
    {
        [DefaultValue("")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DefaultValue("")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [DefaultValue("")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [DefaultValue("")]
        public string? ImagePath { get; set; }

        public CreateUser()
        {
            Email = "";
            Username = "";
            Password = "";
            ImagePath = "";
        }

        // Overloaded Constructor with parameters
        public CreateUser(string email, string username, string password, string imagePath)
        {
            Email = email;
            Username = username;
            Password = password;
            ImagePath = imagePath ?? "";
        }
    }
}
