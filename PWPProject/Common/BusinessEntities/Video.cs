using Common.Enum;
using CommonLibrary.BusinessEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Common.BusinessEntities
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(40)]
        public string Id { get; set; }

        [DefaultValue("")]
        [Required(ErrorMessage = "VideoId is required")]
        public string VideoId { get; set; }


        [DefaultValue("")]
        [Required(ErrorMessage = "UserId is required")]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [DefaultValue("")]
        public string? Tags { get; set; }

        [DefaultValue("")]
        public string? Description { get; set; }

        [DefaultValue("")]
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }


        [DefaultValue("")]
        public string? VideoExtension { get; set; }


        [DefaultValue(false)]
        public bool IsEncoded { get; set; }


        [DefaultValue("")]
        public string? AvailableResolutions { get; set; }


        //[DefaultValue("")]
        //[Required(ErrorMessage = "0 - None, 1 - Intro Video, 2 - IsWinnerVideo")]
        //public VideoStatus? VideoStatus { get; set; }

        [JsonIgnore]
        public bool IsDismiss { get; set; }

        [JsonIgnore]
        public bool IsApproved { get; set; }

        [DefaultValue(false)]
        public bool IsSellingVideo { get; set; }

        public DateTime? VideoUploadedDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookMark>? BookMark { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vote>? Vote { get; set; }

    }

    
}