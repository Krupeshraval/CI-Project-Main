﻿using System;
using System.Collections.Generic;

namespace CI_Entities.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            FavoriteMissions = new HashSet<FavoriteMission>();
            MissionApplications = new HashSet<MissionApplication>();
            MissionInviteFromUsers = new HashSet<MissionInvite>();
            MissionInviteToUsers = new HashSet<MissionInvite>();
            MissionRatings = new HashSet<MissionRating>();
            Stories = new HashSet<Story>();
            StoryInviteFromUsers = new HashSet<StoryInvite>();
            StoryInviteToUsers = new HashSet<StoryInvite>();
            Timesheets = new HashSet<Timesheet>();
            UserSkills = new HashSet<UserSkill>();
        }

        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public long PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string? WhyIVolunteer { get; set; }
        public string? EmployeeId { get; set; }
        public string? Department { get; set; }
        public long? CityId { get; set; }
        public long? CountryId { get; set; }
        public string? ProfileText { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Availability { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavoriteMission> FavoriteMissions { get; set; }
        public virtual ICollection<MissionApplication> MissionApplications { get; set; }
        public virtual ICollection<MissionInvite> MissionInviteFromUsers { get; set; }
        public virtual ICollection<MissionInvite> MissionInviteToUsers { get; set; }
        public virtual ICollection<MissionRating> MissionRatings { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
        public virtual ICollection<StoryInvite> StoryInviteFromUsers { get; set; }
        public virtual ICollection<StoryInvite> StoryInviteToUsers { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
