﻿using CI_Entities.Models;

namespace CI_Project.Models
{
    public class VolunteeringViewModel
    {
        public List<User> Users { get; set; }   
        public long MissionId { get; set; }

        public long CityId { get; set; }
        public string Cityname { get; set; }
       
        public long CountryId { get; set; }
        public string Countryname { get; set; }

        public long ThemeId { get; set; }
        public string Themename { get; set; }


        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
        public string MissionType { get; set; } = null!;
        public string? OrganizationName { get; set; }

        public string? OrganizationDetail { get; set; }

        public string? Availability { get; set; }
        public string? GoalObjectiveText { get; set; }

        public int UserPrevRating { get; set; } 


        public string GoalValue { get; set; } = null!;

        public string username { get; set; }

        public string userEmail { get; set; }
        public long? UserId { get; set; }
        
        public string lastname { get; set; }

        public long StoryId { get; set; }
        public string? StoryTitle { get; set; }
        public string? StoryDescription { get; set; }
        public bool isapplied { get; set; }

        public int AvgRating { get; set; }
        public bool isfav { get; set; }
        public object Theme { get; internal set; }

        public string storypath { get; set; }

        public long storyid { get; set; }

        public  ICollection<MissionApplication> MissionApplications { get; set; }
        public List<Mission> Missions { get;set; }

        public int? goal { get; set; }

        public int? progress { get; set; }

        public int? progressInPerc { get; set; }


        //  public string storyname { get; set;}
        //public int MissionRatings { get; set; }
    }
}
