namespace CI_Project.Models
{
    public class ShareStoryViewModel
    {

        public long StoryId { get; set; }
        public long UserId { get; set; }
        public long MissionId { get; set; }
        public string? Title { get; set; }
        public string? editor1 { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public long StoryMedia { get; set; }

        public string Type { get; set; } = null!;
        public string Path { get; set; } = null!;
    }
}
