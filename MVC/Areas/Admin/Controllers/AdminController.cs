using CI_Entities.CIPlatformContext;
using CI_Project.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

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
            var users = new UserCrudViewModel();
            users.users = _db.Users.ToList();
            return View(users);
        } 
        public IActionResult CmsCrud()
        {
            var page = new UserCrudViewModel();
            page.page = _db.CmsPages.ToList();
            return View(page);
        }

        public IActionResult AdminMission()
        {
            var missions = new UserCrudViewModel();
            missions.missions= _db.Missions.ToList();
            return View(missions);
        } 
        public IActionResult MissionApplication()
        {
            var missions = new UserCrudViewModel();
            missions.users = _db.Users.ToList() ;
            missions.missionApplications= _db.MissionApplications.ToList();
            missions.missions= _db.Missions.ToList();
            return View(missions);
        }
    }
}
