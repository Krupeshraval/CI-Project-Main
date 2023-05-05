using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;

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
            users.users = _db.Users.Where(u=>u.DeletedAt==null).ToList();
            users.cities = _db.Cities.ToList();
            users.country = _db.Countries.ToList();
            return View(users);
        }

        // =================================== Add Delete Edit User ===================================

        public IActionResult adduser(string avtar, string Fname, string Lname, string email, string password, string empID, string department, long city, long country, string proftxt, int status, long userID)
        {
            try
            {
                if (userID == 0 || userID == null)
                {
                    _Iuser.addUser(avtar,Fname, Lname, email, password, empID, department, city, country, proftxt, status);
                }
                else
                {
                    _Iuser.updateUser(avtar,Fname, Lname, email, password, empID, department, city, country, proftxt, status, userID);
                }
                return RedirectToAction("user");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { area = "Employee" });
            }



        }

        public IActionResult getUser(long UserID)
        {
            var userlist = _db.Users.FirstOrDefault(x => x.UserId == UserID);
            var user = new UserCrudViewModel();
            user.avtar = userlist.Avatar;
            user.FirstName = userlist.FirstName;
            user.LastName = userlist.LastName;
            user.Email = userlist.Email;
            user.Password = userlist.Password;
            user.Department = userlist.Department;
            user.ProfileText = userlist.ProfileText;
            user.Status = userlist.Status;
            user.EmployeeId = userlist.EmployeeId;
            user.CityId = userlist.CityId == null ? 0 : userlist.CityId;
            user.CountryId = userlist.CountryId == null ? 0 : userlist.CountryId;
            user.UserId = userlist.UserId;
            user.users = _Iuser.users();
            user.cities = _db.Cities.ToList();
            user.country  = _db.Countries.ToList();

            return View("User",user);
        }


        public IActionResult deleteUser(long UserID)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == UserID);
            user.DeletedAt = DateTime.Now;

            _db.Users.Update(user);
            _db.SaveChanges();

            return RedirectToAction("user", new { UserID = UserID});
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

        public IActionResult AdminMission(AdminMissionViewModel mission)
        {
            HttpContext.Session.SetInt32("Nav", 3);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            AdminMissionViewModel missions;
            if (mission.missionId != 0)
            {
                missions = mission;
            }
            else
            {
                missions = new AdminMissionViewModel();
            }

            missions.Missions = _db.Missions.Where(m => m.DeletedAt == null).ToList();
            missions.Cities = _db.Cities.ToList();
            missions.Countries = _db.Countries.ToList();
            missions.MissionThemes = _db.MissionThemes.ToList();
            missions.Skills = _db.Skills.ToList();

            return View(missions);
        }


        [HttpPost]
        public IActionResult addMission(long missionId, string Title, string ShortDesc, string Desc, int city, int country, string OrgName, string OrgDetail, string misstype, int seats, DateTime startdate, DateTime endDate, DateTime RegDeadline, string availability, int themeid, int skill)
        {
            try
            {
                if (missionId == 0 || missionId == null)
                {
                    _Iuser.addMission(Title, ShortDesc, Desc, city, country, OrgName, OrgDetail, misstype, seats, startdate, endDate, RegDeadline, availability, themeid, skill);
                }
                else
                {
                    _Iuser.updateMission(missionId, Title, ShortDesc, Desc, city, country, OrgName, OrgDetail, misstype, seats, startdate, endDate, RegDeadline, availability, themeid, skill);
                }
                return RedirectToAction("AdminMission");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "User", new { area = "Employee" });
            }

        }

        public IActionResult getMiss(long missionId)
        {
            var missionlist = _db.Missions.FirstOrDefault(x => x.MissionId == missionId);
            var mission = new AdminMissionViewModel();
            mission.Title = missionlist.Title;
            mission.ShortDescription = missionlist.ShortDescription;
            mission.Description = missionlist.Description;
            mission.CityId = missionlist.CityId == null ? 0 : missionlist.CityId;
            mission.CountryId = missionlist.CountryId == null ? 0 : missionlist.CountryId;
            mission.OrganizationName = missionlist.OrganizationName;
            mission.OrganizationDetail = missionlist.OrganizationDetail;
            mission.MissionType = missionlist.MissionType;
            mission.SeatsLeft = missionlist.SeatsLeft;
            mission.Availability = missionlist.Availability;
            mission.missionId = missionlist.MissionId;
            mission.Missions = _Iuser.missionlist();

            return RedirectToAction("AdminMission", mission);
        }
        public IActionResult delmiss(long missionId)
        {
            var mission = _db.Missions.FirstOrDefault(x => x.MissionId == missionId);
            mission.DeletedAt = DateTime.Now;

            _db.Missions.Update(mission);
            _db.SaveChanges();

            return RedirectToAction("AdminMission", new { missionId = missionId });
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
                    CreatedAt = DateTime.Now,
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

            return Json("CmsCrud");
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

            return RedirectToAction("CmsCrud", "Admin");
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

        // ====================================== Admin Banner Management ===================================

        public IActionResult AdminBannerManagement()
        {
            HttpContext.Session.SetInt32("Nav", 8);
            ViewBag.nav = HttpContext.Session.GetInt32("Nav");
            ViewData["banners"] = _db.Banners.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AdminBannerManagement(Banner b, IFormFileCollection? bannerPic)
        {
            ViewData["banners"] = _db.Banners.ToList();

            Banner newBan = new Banner();
            newBan.Text = b.Text;

            foreach (IFormFile file in bannerPic)
            {
                if (file != null)
                {
                    //Set Key Name
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    //Get url To Save
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", ImageName);

                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        newBan.Image = ImageName;
                        file.CopyTo(stream);
                    }
                }
            }
            newBan.CreatedAt = DateTime.Now;
            _db.Banners.Add(newBan);
            _db.SaveChanges();
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["bye"] = "logged out successfully";
            TempData["okay"] = null;
            return RedirectToAction("LandingPage", "User", new { area = "Employee" });
        }

    }
}
