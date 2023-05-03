using CI_Entities.Models;
//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CI_Project.Models
{
    public class AdminMissionViewModel
    {
        public long missionId { get; set; }

        public List<Mission> Missions { get; set; }

        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }

        public List<MissionTheme> MissionThemes { get; set; }

        public List<Skill> Skills { get; set; }

        [Required (ErrorMessage = "MissionType is reqiired")]
        public string MissionType { get; set; } = null!;

        [Required(ErrorMessage = "Title is reqiired")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Short Description is reqiired")]
        public string? ShortDescription { get; set; }

       
        [Required(ErrorMessage = "Description is reqiired")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "CityId is reqiired")]
        public long CityId { get; set; }

        [Required(ErrorMessage = "CountryId is reqiired")]
        public long CountryId { get; set; }

        [Required(ErrorMessage = "Organization Name is reqiired")]
        public string? OrganizationName { get; set; }

        [Required(ErrorMessage = "Organization Detail is reqiired")]
        public string? OrganizationDetail { get; set; }

        [Required(ErrorMessage = "Seats Left is reqiired")]
        public long? SeatsLeft { get; set; }

        [Required(ErrorMessage = "Deadline is reqiired")]
        public DateTime? Deadline { get; set; }

        [Required(ErrorMessage = "Start Date is reqiired")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is reqiired")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Availability is reqiired")]
        public string? Availability { get; set; }

        public List<MissionMedium> ImageFiles { get; set; }

        public List<IFormFile> docfiles { get; set; }


        public string url { get; set; }




    }
}
