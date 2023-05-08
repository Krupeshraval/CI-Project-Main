using CI_Entities.Models;

namespace CI_Project.Models
{
    public class AdminSkillViewModel
    {
        public List<Skill> skills { get; set; }
        public long SkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
 