using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BLLayer
{
    public class ApplicationStats
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int TotalVideos { get; set; }
        public int TotalUsers { get; set; }
        public string MostVotedVideo { get; set; }
        public string MostBookmarkedVideo { get; set; }
    }
}