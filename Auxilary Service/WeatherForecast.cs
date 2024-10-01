namespace Auxilary_Service
{
    public class VideosDto
    {
        public string? Id { get; set; }
        public string? VideoId { get; set; }

        public string? Tags { get; set; }

        public string? Description { get; set; }

        public string? Url { get; set; }

        public int? VideoStatus { get; set; }

        public DateTime? VideoUploadedDate { get; set; }

        public bool isVoted { get; set; }
        public bool isBookmarked { get; set; }

        public string? thumbnailUrl { get; set; }

    }
}
