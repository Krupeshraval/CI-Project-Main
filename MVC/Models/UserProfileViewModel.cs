using CI_Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_Project.Models
{
    public class UserProfileViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide First Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First Name Should be min 2 and max 20 length")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Last Name")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First Name Should be min 2 and max 20 length")]

        public string? LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string Email { get; set; } = null!;

        public string EmployeeID { get; set; }

        public string? Avatar { get; set; }
        public IFormFile UserImg { get; set; }

        public string Availability { get; set; }


        public string title { get; set; }

        public string department { get; set; }

        public string? LinkedinUrl { get; set; }

        public string? WhyIVolunteered { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Profile Text")]

        public string? ProfileText { get; set; }

        public long? CityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Country")]
        public long? CountryId { get; set; }
       
        public List<IFormFile> files { get; set; }

        public List<Skill> skills { get; set; }
        public List<UserSkill> userSkills { get; set; }
        public List<Skill> RemainingSkill { get; set; }
    }
}
