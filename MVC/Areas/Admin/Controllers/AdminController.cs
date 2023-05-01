﻿using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CI_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly CIPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public AdminController(CIPlatformContext db, IUserInterface Iuser)
        {
            _Iuser = Iuser;
            _db = db; //underscore db ma baddho database store thai jase
        }
        public IActionResult user()
        {
            HttpContext.Session.SetInt32("Nav", 1);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var users = new UserCrudViewModel();
            users.users = _db.Users.ToList();
            users.cities = _db.Cities.ToList();
            users.country = _db.Countries.ToList();
            return View(users);
        }

        // =================================== Add Delete Edit User ===================================

        [HttpPost]

        public IActionResult adduser(long userID, int status, string proftxt, string email, string password, string department, string Fname, string Lname, string empID, int country, int city)
        {


            if (userID == 0)
            {
                User user = new User()
                {
                    FirstName = Fname,
                    LastName = Lname,
                    Email = email,
                    Password = password,
                    Department = department,
                    Status = status,
                    ProfileText = proftxt,
                    EmployeeId = empID,
                    CountryId = country,
                    CityId = city,

                };
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            else
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == userID);
                user.FirstName = Fname;
                user.LastName = Lname;
                user.Email = email;
                user.Password = password;
                user.Department = department;
                user.CityId = city;
                user.CountryId = country;
                user.Department = department;
                user.ProfileText = proftxt;
                user.EmployeeId = empID;
                user.Status = status;

                _db.Update(user);
                _db.SaveChanges();

            }

            return Json("user");
        }

        public IActionResult GetUser(long UserID)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == UserID);
            return Json(user);
        }

        public IActionResult deleteUser(long UserID)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == UserID);
            user.DeletedAt = DateTime.Now;

            _db.Users.Update(user);
            _db.SaveChanges();

            return View("user");
        }



        [HttpGet]
        public IActionResult CmsCrud()
        {
            HttpContext.Session.SetInt32("Nav", 2);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");

            var page = new CmsPageViewModel();
            page.Pages = _db.CmsPages.ToList();
            return View(page);

             
        }

        //[HttpPost]
        //public IActionResult CmsCrud(CmsPageViewModel)
        //{
        //    if (model.CmsPageId == 0 || model.CmsPageId == null)
        //    {
        //        _Idb.AddCms(model);

        //    }
        //    else
        //    {
        //        _Idb.UpdateCms(model);
        //    }
        //    return RedirectToAction("CmsCrud");
        //}

        public IActionResult AdminMission()
        {
            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var missions = new AdminMissionViewModel();
            missions.Missions= _db.Missions.ToList();
            missions.MissionThemes= _db.MissionThemes.ToList();
            missions.Skills = _db.Skills.ToList();
            missions.Cities= _db.Cities.ToList();
            missions.Countries= _db.Countries.ToList();
            return View(missions);
        } 
        public IActionResult MissionApplication()
        {
            HttpContext.Session.SetInt32("Nav", 6);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var missions = new UserCrudViewModel();
            missions.users = _db.Users.ToList() ;
            missions.missionApplications= _db.MissionApplications.Where(u => u.ApprovalStatus == "PENDING").ToList();
            missions.missions= _db.Missions.ToList();
            return View(missions);
        }
        
        public IActionResult AdminStory()
        {
            HttpContext.Session.SetInt32("Nav", 7);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            var storys = new UserCrudViewModel();
            storys.users= _db.Users.ToList() ;
            storys.missions = _db.Missions.ToList();
            storys.stories = _db.Stories.ToList();
           return View(storys);
        }


        // ============================================ CMS =========================================

        public IActionResult CMSAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CMSAdd(long CMSId, String Title, String Desc, String Slug, int Status)
        {
            if (CMSId == 0)
            {
                CmsPage cms = new CmsPage()
                {
                    Title = Title,
                    Description = Desc,
                    Slug = Slug,
                    Status = Status,
                    //CreatedAt = DateTime.Now,
                };

                _db.Add(cms);
                _db.SaveChanges();
            }
            else
            {
                CmsPage cms = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == CMSId);
                cms.Status = Status;
                cms.Title = Title;
                cms.Description = Desc;
                cms.Slug = Slug;
                cms.UpdatedAt = DateTime.Now;

                _db.Update(cms);
                _db.SaveChanges();
            }

            return Json("_CMSAdmin");
        }

        /*CMS Get Data*/
        public IActionResult GetCMSData(long CMSId)
        {
            var Data = _db.CmsPages.FirstOrDefault(x => x.CmsPageId == CMSId);
            return Json(Data);
        }

        public IActionResult CMSDelete(long CMSId)
        {
            var cms = _db.CmsPages.FirstOrDefault(u => u.CmsPageId == CMSId);

            _db.CmsPages.Remove(cms);
            _db.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }


        [HttpGet]
        public IActionResult approveMission(long missionApplicationId)
        {
            var missionapp = _db.MissionApplications.FirstOrDefault(m => m.MissionApplicationId == missionApplicationId);
            missionapp.ApprovalStatus = "Approved";

            _db.MissionApplications.Update(missionapp);
            _db.SaveChanges();

            return RedirectToAction("MissionApplication", "Admin");
        }

        [HttpGet]

        public IActionResult rejectMission(long missionApplicationId)
        {
            var missionrej = _db.MissionApplications.FirstOrDefault(m => m.MissionApplicationId == missionApplicationId);
            missionrej.ApprovalStatus = "Rejected";

            _db.MissionApplications.Update(missionrej);
            _db.SaveChanges();

            return RedirectToAction("MissionApplication", "Admin");


        }

        public IActionResult MissionTheme()
        {
            var theme = new MissionThemeViewModel();
                theme.MissionThemes = _db.MissionThemes.ToList();
            return View(theme);
        }

        public IActionResult MissionSkill()
        {
            var missionskills = new MissionThemeViewModel();
            missionskills.skills =_db.Skills.ToList();
            return View(missionskills);
        }
    }
}
