using CI_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_Project.Models
{
    public class MissionThemeViewModel
    {
        public List<MissionTheme> MissionThemes { get; set; }

        public List<Skill> skills { get; set; }

        public long MissionThemeId { get; set; }

        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Status is required")]
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
