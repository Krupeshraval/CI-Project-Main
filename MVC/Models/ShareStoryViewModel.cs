using CI_Entities.Models;
using Microsoft.Build.Framework;

namespace CI_Project.Models
{
    public class ShareStoryViewModel
    {
        public string storymediapath { get; set; }

        public string Useravtar { get; set; }

        public string lastname { get; set; }

        public string username { get; set; }

        public string Themename { get; set; }

        public string? StoryTitle { get; set; }
        public string? StoryDescription { get; set; }



    
        public List<IFormFile> attachment { get; set; }
        //public long StoryId { get; set; }
        public int? StoryViews { get; set; }

        public User Singleuser { get; set; }
        public Timesheet Singlesheet { get; set; }

        public long StoryId { get; set; }
        public long UserId { get; set; }

        public long MissionId { get; set; }

        [Required]
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

        public List<Story> story { get; set; }
        public List<User> users { get; set; }
        public List<Mission> missions { get; set; }
        public List<MissionTheme> missionthemes { get; set; }
        public List<StoryMedium> storyMedia { get; set; }

        public List<MissionApplication> MissionApplications { get; set; }

        //=========================================================================================================
                                                         //Timesheet
        //=========================================================================================================

        public List<Timesheet> timesheets { get; set; }

        public long TimesheetId { get; set; }

        public long hiddenid { get; set; }

        public string TimesheetTime { get; set; }

        //[Required(ErrorMessage = "please Enter Action")]
        public int? Action { get; set; }

        public DateTime DateVolunteered { get; set; }

        public string? Notes { get; set; }

        [Required]
        public int hour { get; set; }
        [Required]
        public int minute { get; set; }

        [Required]
        public virtual Mission? Mission { get; set; }

    }
}
