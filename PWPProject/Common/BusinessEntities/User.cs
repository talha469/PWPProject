using Common.BusinessEntities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommonLibrary.BusinessEntities
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int UserId { get; set; }

        public string Email { get; set; }
        public string? Username { get; set; }
        public string PasswordHash { get; set; }

        public string? ImagePath { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        public DateTime AccountCreatedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Video>? UploadedVideos { get; set; }

        public User()
        {
            
            UserId = 0; 
            Email = "";
            Username = null; 
            PasswordHash = "";
            ImagePath = null; 
            Role = "";
            AccountCreatedDate = DateTime.MinValue;
        }
    }

}
