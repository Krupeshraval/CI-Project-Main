using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Project.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly CIPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public UserProfileController(CIPlatformContext db, IUserInterface Iuser)
        {
            _Iuser = Iuser;
            _db = db; //underscore db ma baddho database store thai jase
        }

        public IActionResult UserProfile()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString("userID"));

            var user = _Iuser.users().FirstOrDefault(u => u.UserId == userId);
            UserProfileViewModel userProfile = new UserProfileViewModel();

            //userProfile.EmployeeID = user.EmployeeId;
            userProfile.EmployeeID = user.EmployeeId;
            userProfile.FirstName = user.FirstName;
            userProfile.LastName = user.LastName;
            userProfile.Email = user.Email;

            userProfile.WhyIVolunteered = user.WhyIVolunteer;
            userProfile.title = user.Title;
            userProfile.CityId = user.CityId;
            userProfile.CountryId = user.CountryId;
            //userVM.availability = user.
            userProfile.ProfileText = user.ProfileText;
            userProfile.LinkedinUrl = user.LinkedInUrl;
            userProfile.Avatar = user.Avatar != null ? user.Avatar : "";
            userProfile.department = user.Department;
            userProfile.CityId = user.CityId;
            userProfile.CountryId = user.CountryId;
            userProfile.Availability = user.Availability;
            var allskills = _Iuser.skills();
            ViewBag.allskills = allskills;
            var skills = from US in _db.UserSkills
                         join S in _db.Skills on US.SkillId equals S.SkillId
                         select new { US.SkillId, S.SkillName, US.UserId };
            var uskills = skills.Where(e => e.UserId == userId).ToList();
            ViewBag.userskills = uskills;
            foreach (var skill in uskills)
            {
                var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
                allskills.Remove(rskill);
            }
            ViewBag.remainingSkills = allskills;
            ViewBag.allcities = _Iuser.cities();
            ViewBag.allcountry = _Iuser.countries();
            userProfile.skills = _Iuser.skills();
            userProfile.userSkills = _Iuser.skilllist(Convert.ToInt32(userId));

            var r = from row1 in userProfile.skills.AsEnumerable()
                    where !userProfile.userSkills.AsEnumerable().Select(r => r.SkillId).Contains(row1.SkillId)
                    select row1;

            userProfile.RemainingSkill = r.ToList();
            return View(userProfile);
        }

        [HttpPost]

        public async Task<IActionResult> userProfile(UserProfileViewModel model, IFormFileCollection files)
        {

            var userId = Convert.ToInt64(HttpContext.Session.GetString("userID"));

            //long id = Convert.ToInt64(userid);
            //long storyid = model.storyId;
            var userdetail = _Iuser.users().FirstOrDefault(u => u.UserId == userId);
            userdetail.FirstName = model.FirstName;
            userdetail.LastName = model.LastName;
            userdetail.WhyIVolunteer = model.WhyIVolunteered;
            userdetail.Title = model.title;

            userdetail.EmployeeId = model.EmployeeID;
            userdetail.ProfileText = model.ProfileText;
            userdetail.LinkedInUrl = model.LinkedinUrl;
            userdetail.UpdatedAt = DateTime.Now;
            userdetail.Department = model.department;
            userdetail.CountryId = model.CountryId;
            userdetail.CityId = model.CityId;
            userdetail.Availability = model.Availability;

            if (files.Count() == 0)
            {
                model.Avatar = userdetail.Avatar;
            }
            else
            {
                userdetail.Avatar = model.Avatar;

            }
            foreach (var file in files)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var imageBytes = ms.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);
                    userdetail.Avatar = "data:image/png;base64," + base64String;
                    model.Avatar = "data:image/png;base64," + base64String;
                    HttpContext.Session.SetString("useravtar", "data:image/png;base64," + base64String);
                }
            }

            var allskills = _Iuser.skills();
            ViewBag.allskills = allskills;
            var skills = from US in _db.UserSkills
                         join S in _db.Skills on US.SkillId equals S.SkillId
                         select new { US.SkillId, S.SkillName, US.UserId };
            var uskills = skills.Where(e => e.UserId == userId).ToList();
            ViewBag.userskills = uskills;
            foreach (var skill in uskills)
            {
                var rskill = allskills.FirstOrDefault(e => e.SkillId == skill.SkillId);
                allskills.Remove(rskill);
            }
            ViewBag.remainingSkills = allskills;
            ViewBag.allcities = _Iuser.cities();
            ViewBag.allcountry = _Iuser.countries();

            _Iuser.updateuser(userdetail);
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> SaveUserSkills(long[] selectedSkills)
        {
            var userid = HttpContext.Session.GetString("userID");
            long id = Convert.ToInt64(userid);
            var abc = _db.UserSkills.Where(e => e.UserId == id).ToList();
            _db.RemoveRange(abc);
            _db.SaveChanges();
            foreach (var skills in selectedSkills)
            {
                _Iuser.AddUserSkills(skills, Convert.ToInt32(userid));
            }
            return RedirectToAction("UserProfile", "UserProfile");
        }


    }
}
