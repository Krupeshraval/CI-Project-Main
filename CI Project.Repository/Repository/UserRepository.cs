﻿using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _db.Missions.ToList();
        }

        public List<MissionRating> missionratingslist()
        {
            return _db.MissionRatings.ToList();
        }
        public List<City> cities()
        {
            return _db.Cities.ToList();
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
            return _db.Users.ToList();
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
            return _db.Comments.ToList();
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
    }
}
