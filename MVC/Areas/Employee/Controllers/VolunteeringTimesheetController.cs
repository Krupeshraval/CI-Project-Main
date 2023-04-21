using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Models;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_Project.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class VolunteeringTimesheetController : Controller
    {

        private readonly CIPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public VolunteeringTimesheetController(CIPlatformContext db)
        {
            _db = db; //underscore db ma baddho database store thai jase
        }

        public IActionResult VolunteeringTimesheet()
        {

            //Timesheet

            ShareStoryViewModel ss = new ShareStoryViewModel();
            var userid = HttpContext.Session.GetString("userID");
            ss.missions = _db.Missions.ToList();
            ss.MissionApplications = _db.MissionApplications.Where(e => e.UserId == Convert.ToInt64(userid)).ToList();
            ss.timesheets = _db.Timesheets.ToList();

            return View(ss);
        }

        [HttpPost]
        public IActionResult Timesheet(ShareStoryViewModel ss)
        {
            if (ss.TimesheetId != 0)
            {
                Timesheet sheet = _db.Timesheets.FirstOrDefault(e => e.TimesheetId == ss.TimesheetId);
                if (ss.hour != 0 && ss.minute != 0)
                {
                    var userid = HttpContext.Session.GetString("userID");
                    var user = Convert.ToInt32(userid);
                    sheet.UserId = user;
                    sheet.MissionId = ss.MissionId;
                    sheet.TimesheetTime = ss.hour + ":" + ss.minute;
                    ss.DateVolunteered = ss.DateVolunteered;
                    sheet.DateVolunteered = ss.DateVolunteered;
                    sheet.Notes = ss.Notes;
                }

                else
                {
                    var userid = HttpContext.Session.GetString("userID");
                    var user = Convert.ToInt32(userid);
                    sheet.UserId = user;
                    sheet.MissionId = ss.MissionId;
                    ss.DateVolunteered = ss.DateVolunteered;
                    sheet.DateVolunteered = ss.DateVolunteered;
                    sheet.Notes = ss.Notes;
                    sheet.Action = ss.Action;
                }

                _db.Timesheets.Update(sheet);
                _db.SaveChanges();
            }

            else
            {
                Timesheet sheets = new Timesheet();
                if (ss.hour != 0 && ss.minute != 0)
                {
                    var userid = HttpContext.Session.GetString("userID");
                    var user = Convert.ToInt32(userid);
                    sheets.UserId = user;
                    sheets.MissionId = ss.MissionId;
                    sheets.TimesheetTime = ss.hour + ":" + ss.minute;
                    ss.DateVolunteered = ss.DateVolunteered;
                    sheets.DateVolunteered = ss.DateVolunteered;
                    sheets.Notes = ss.Notes;
                }

                else
                {
                    var userid = HttpContext.Session.GetString("userID");
                    var user = Convert.ToInt32(userid);
                    sheets.UserId = user;
                    sheets.MissionId = ss.MissionId;
                    ss.DateVolunteered = ss.DateVolunteered;
                    sheets.DateVolunteered = ss.DateVolunteered;
                    sheets.Notes = ss.Notes;
                    sheets.Action = ss.Action;
                }

                _db.Timesheets.Add(sheets);
                _db.SaveChanges();
            }



            return RedirectToAction("VolunteeringTimesheet", "VolunteeringTimesheet");
        }

        public IActionResult EditTimesheet(int id)
        {
            ShareStoryViewModel ss = new ShareStoryViewModel();
            var userid = HttpContext.Session.GetString("userID");
            ss.missions = _db.Missions.ToList();
            ss.MissionApplications = _db.MissionApplications.Where(e => e.UserId == Convert.ToInt64(userid)).ToList();
            ss.timesheets = _db.Timesheets.ToList();

            ss.Singlesheet = _db.Timesheets.FirstOrDefault(u => u.UserId == Convert.ToInt32(userid));
            var sheet = _db.Timesheets.FirstOrDefault(u => u.TimesheetId == id);

            ss.DateVolunteered = sheet.DateVolunteered;
            ss.TimesheetId = sheet.TimesheetId;
            ss.Notes = sheet.Notes;

            //if (ss.hour != 0 && ss.minute != 0)
            if (sheet.TimesheetTime != null)
            {
                ss.hour = Convert.ToInt32(sheet.TimesheetTime.Split(":")[0]);
                ss.minute = Convert.ToInt32(sheet.TimesheetTime.Split(":")[1]);
            }

            else
            {
                //ss.hour = 0;
                //ss.minute = 0;
                ss.Action = sheet.Action;
            }


            return View("VolunteeringTimesheet", ss);
        }


        //Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var time = _db.Timesheets.FirstOrDefault(x => x.TimesheetId == id);
            _db.Timesheets.Remove(time);
            _db.SaveChanges();

            return View();
        }

    }
}
