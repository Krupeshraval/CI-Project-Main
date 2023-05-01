using CI_Entities.Models;

namespace CI_Project.Models
{
    public class AdminMissionViewModel
    {


        public List<Mission> Missions { get; set; }

        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }

        public List<MissionTheme> MissionThemes { get; set; }

        public List<Skill> Skills { get; set; }

        public string MissionType { get; set; } = null!;
        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public long CityId { get; set; }

        public long CountryId { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDetail { get; set; }

        public long? SeatsLeft { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Availability { get; set; }

        public List<MissionMedium> ImageFiles { get; set; }

        public List<IFormFile> docfiles { get; set; }

        public string url { get; set; }




    }
}
