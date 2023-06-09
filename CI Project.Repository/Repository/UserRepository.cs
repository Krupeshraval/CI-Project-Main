﻿using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web; 

namespace CI_Project.Repository.Repository
{
    public class UserRepository : IUserInterface
    {
        private readonly CIPlatformContext _db;
         
        public UserRepository(CIPlatformContext db)
        {
            _db = db; //underscore db ma baddho database store thai jase
        }
        public User UserByEmail(String mail)
        {
            return _db.Users.FirstOrDefault(u => u.Email == mail);
        }
        public User UserByEmail_Password(String mail, String Password)
        {
            return _db.Users.Where(m => m.Email == mail && m.Password == Password).FirstOrDefault();
        }

        public List<PasswordReset> passwordreset()
        {
            return _db.PasswordResets.ToList();
        }

        public PasswordReset ResetPass(string email, string token)
        {
            return _db.PasswordResets.FirstOrDefault(pr => pr.Email == email && pr.Token == token);
        }

        public List<Mission> missionlist()
        {

            return _db.Missions.Where(m => m.DeletedAt == null).ToList();
        }

        public List<MissionRating> missionratingslist()
        {
            return _db.MissionRatings.ToList();
        }
        public List<City> cities()
        {
            return _db.Cities.ToList();
        }
        public List<MissionTheme> themeList()
        {
            return _db.MissionThemes.Where(t => t.DeletedAt == null).ToList();
        }
        public List<Country> countries()
        {
            return _db.Countries.ToList();
        }
        public List<MissionTheme> themes()
        {
            return _db.MissionThemes.ToList();
        }
        public List<Skill> skills()
        {
            return _db.Skills.ToList();
        }
        public List<GoalMission> goalMissions()
        {
            return _db.GoalMissions.ToList();
        } 
        public List<User> users()
        {
            return _db.Users.Where( u => u.DeletedAt == null).ToList();
        }

        public List<FavoriteMission> favoriteMissions()
        {

            return _db.FavoriteMissions.ToList();
        }
        public List<MissionRating> missionRatings()
        {
            return _db.MissionRatings.ToList();
        }
        public List<MissionApplication> missionApplications()
        {
            return _db.MissionApplications.ToList();
        }
        public List<Comment> comments()
        {
            return _db.Comments.OrderByDescending(u => u.CreatedAt).ToList();
        } 
        public void adduser (User user) 
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
        public void comment (Comment comment) 
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }

        public void apply(long missionid, long userid)
        {
            var missionapplication = new MissionApplication();
            missionapplication.UserId = userid;
            missionapplication.MissionId = missionid;
            missionapplication.AppliedAt = DateTime.Now;
            missionapplication.ApprovalStatus = "1";
            _db.Add(missionapplication);
            _db.SaveChanges();
        }
        public List<FavoriteMission> Addfavorite(long missionid, long id)
        {var obj = new FavoriteMission();
            obj.UserId = id;
            obj.MissionId = missionid;
            obj.CreatedAt = DateTime.Now;
            _db.FavoriteMissions.Add(obj);
            _db.SaveChanges();
            return null;
        }
        public List<FavoriteMission> removefavorite(long missionid, long id)
        {
           var match=_db.FavoriteMissions.FirstOrDefault(x => x.MissionId == missionid&&x.UserId==id);
            _db.FavoriteMissions.Remove(match);
            _db.SaveChanges();
            return null;
        }

        public void AddUserSkills(long SkillId, int UserId)
        {
            UserSkill skill = new UserSkill();
            skill.SkillId = SkillId;
            skill.UserId = UserId;
            _db.UserSkills.Add(skill);
            _db.SaveChanges();
        }
        public void updateuser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public List<UserSkill> skilllist(int userid)
        {
            return _db.UserSkills.Where(e => e.UserId == userid).ToList();
        }

        public ContactU addContactUs(string subject, string message, string username, string email)
        {
            var contactUs = new ContactU();
            contactUs.Username = username;
            contactUs.Email = email;
            contactUs.Subject = subject;
            contactUs.Message = message;
            contactUs.CreatedAt= DateTime.Now;
            _db.Add(contactUs);
            _db.SaveChanges();
            return contactUs;
        }

        public CmsPage GetCmsPage(long CmsPageId)
        {
            var cms = _db.CmsPages.FirstOrDefault(e => e.CmsPageId == CmsPageId);
            var cmsPage = new CmsPage();
            cmsPage.CmsPageId = CmsPageId;
            //cmsPage.c = _db.CmsPages.Where(e => e.DeletedAt == null).ToList();
            cmsPage.Title = cms.Title;
            cmsPage.Description = HttpUtility.HtmlDecode(cms.Description);
            cmsPage.Status = cms.Status;
            cmsPage.Slug = cms.Slug;

            return cmsPage;
        }

        public User addUser(string avtar,string firstName, string lastName, string email, string password, string empid, string department, long cityId, long countryId, string ProfileText, int status)
        {
            User user = new User();
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Password = password;
            user.EmployeeId = empid;
            user.Department = department;
            user.CityId = cityId;
            user.CountryId = countryId;
            user.ProfileText = ProfileText;
            user.Status = status;
            user.Avatar = avtar;

            user.CreatedAt = DateTime.Now;

            _db.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User updateUser(string avtar,string firstName, string lastName, string email, string password, string empid, string department, long cityId, long countryId, string ProfileText, int status, long userId)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserId == userId);
            user.UserId = userId;
            user.Avatar= avtar;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.Password = password;
            user.EmployeeId = empid;
            user.Department = department;
            user.CityId = cityId;
            user.CountryId = countryId;
            user.ProfileText = ProfileText;
            user.Status = status;
            user.UpdatedAt = DateTime.Now;

            _db.Update(user);
            _db.SaveChanges();
            return user;
        }
        public User UserExist(string Email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == Email);
        }

        public Mission addMission(string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill)
        {
            var mission = new Mission();
            mission.Title = Title;
            mission.ShortDescription = ShortDesc;
            mission.Description = Desc;
            mission.CityId = city;
            mission.CountryId = country;
            mission.OrganizationName = OrgName;
            mission.OrganizationDetail = OrgDetail;
            mission.MissionType = misstype;
            mission.Availability = availability;
            mission.SeatsLeft = seats;
            mission.Availability = availability;
            mission.Deadline = DateTime.Now;
            mission.StartDate = DateTime.Now;
            mission.EndDate = DateTime.Now;
            mission.ThemeId = themeid;

            _db.Add(mission);
            _db.SaveChanges();

            MissionSkill missionSkill = new MissionSkill()
            {
                SkillId = skill,
                MissionId = mission.MissionId,
            };
            _db.MissionSkills.Add(missionSkill);
            _db.SaveChanges();

            return mission;
        }

        public Mission updateMission(long missionId, string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill)
        {
            Mission mission = _db.Missions.FirstOrDefault(m => m.MissionId == missionId);
            mission.Title = Title;
            mission.ShortDescription = ShortDesc;
            mission.Description = Desc;
            mission.CityId = city;
            mission.CountryId = country;
            mission.OrganizationName = OrgName;
            mission.OrganizationDetail = OrgDetail;
            mission.MissionType = misstype;
            mission.Availability = availability;
            mission.UpdatedAt = DateTime.Now;

            _db.Update(mission);
            _db.SaveChanges();
            return mission;
        }

        public MissionTheme addtheme(string themeName, int Status)
        {
            var theme = new MissionTheme();
            theme.Title = themeName;
            theme.Status= Status;
            theme.CreatedAt= DateTime.Now;

            _db.Add(theme);
            _db.SaveChanges();

            return theme;
        }

        public MissionTheme updateTheme(string themeName, int Status, long MissionThemeId)
        {
            MissionTheme themes = _db.MissionThemes.FirstOrDefault(m => m.MissionThemeId == MissionThemeId);
            themes.Title = themeName;
            themes.Status = Status;
            themes.MissionThemeId = MissionThemeId;
            themes.UpdatedAt = DateTime.Now;

            _db.Update(themes);
            _db.SaveChanges();
            return themes;
        }

        public Skill addSkill(string skillName, int Status)
        {
            var skill = new Skill();
            skill.SkillName = skillName;
            skill.Status = Status;
            skill.CreatedAt= DateTime.Now;

            _db.Add(skill);
            _db.SaveChanges();

            return skill;
        }

        public Skill updateSkill(string skillName, int Status, long skillId)
        {
            Skill skills = _db.Skills.FirstOrDefault(m => m.SkillId == skillId);
            skills.SkillName = skillName;
            skills.Status= Status;
            skills.SkillId = skillId;
            skills.UpdatedAt= DateTime.Now;

            _db.Update(skills);
            _db.SaveChanges();
            return skills;
        }
    }
}
