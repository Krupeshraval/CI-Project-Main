﻿using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Web;

namespace CI_Project.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class StoryListingController : Controller
    {
        private readonly CIPlatformContext _db;

        public StoryListingController(CIPlatformContext db)
        {
            _db = db; //underscore db ma baddho database store thai jase
        }

        public IActionResult StoryDetail(long id, long storyid)
        {
            List<User> alluser = _db.Users.ToList();
            List<VolunteeringViewModel> allavailuser = new List<VolunteeringViewModel>();
            foreach (var all in alluser)
            {
                allavailuser.Add(new VolunteeringViewModel
                {
                    username = all.FirstName,
                    lastname = all.LastName,
                    userEmail = all.Email,
                    UserId = all.UserId,
                });

            }
            var userId = HttpContext.Session.GetString("userID");

            ViewBag.UserId = Convert.ToInt64(userId);
            ViewBag.allavailuser = allavailuser;
            IList<User> users = _db.Users.ToList();
            //var userId = HttpContext.Session.GetString("userID");
            var story = _db.Stories.Find(storyid);
            ViewBag.UserId = Convert.ToInt64(userId);

            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewData["userImg"] = _db.Users.ToList().Where(m => m.UserId == Convert.ToInt64(HttpContext.Session.GetString("userID"))).Select(m => m.Avatar).FirstOrDefault();
            }


            return View(story);
        }





        public IActionResult StoryListing(int? page)
        {
            List<Story> storylist = _db.Stories.ToList();
            List<VolunteeringViewModel> StoryList = new List<VolunteeringViewModel>();
            foreach (var story in storylist)
            {
                var storyuser = _db.Users.FirstOrDefault(x => x.UserId == story.UserId);
                var storymedia = _db.StoryMedia.Where(s => s.StoryId == story.StoryId).FirstOrDefault();
                StoryList.Add(new VolunteeringViewModel
                {
                    StoryId = story.StoryId,
                    MissionId = story.MissionId,
                    UserId = story.UserId,
                    StoryTitle = story.Title,
                    ShortDescription = story.Description,
                    username = storyuser.FirstName,
                    lastname = storyuser.LastName,
                    storypath = storymedia != null ? storymedia.Path : null,

                });

            }
            ViewBag.StoryList = StoryList;

            const int pageSize = 3; // Number of items to display per page
            int pageNumber = page ?? 1; // Default to the first page
                                          // Total number of items
            var items = StoryList
                //.OrderByDescending(i => i.CreatedDate
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int total = StoryList.Count();
            ViewBag.TotalMissions = total;

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            ViewData["mission"] = items.ToList();


            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewData["userImg"] = _db.Users.ToList().Where(m => m.UserId == Convert.ToInt64(HttpContext.Session.GetString("userID"))).Select(m => m.Avatar).FirstOrDefault();
            }

            return View(items);

        }

        public IActionResult ShareStory(long storyID)
        
        {
            IEnumerable<Mission> missions = _db.Missions.ToList();
            IEnumerable<StoryMedium> ObjStoryMed = _db.StoryMedia.ToList();
            ViewData["mission"] = _db.MissionApplications.ToList();

            var UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));

            ShareStoryViewModel storyView = new ShareStoryViewModel();

            if (storyID != 0)
            {
                storyView.storyMedia = _db.StoryMedia.Where(e => e.StoryId == storyID).ToList();
                storyView.MissionApplications = _db.MissionApplications.Where(u => u.UserId == UserId).ToList();

                var story = _db.Stories.Where(e => e.StoryId == storyID).FirstOrDefault();
                storyView.MissionId = story.MissionId;
                storyView.Title = story.Title;
                storyView.editor1 = story.Description;
                storyView.CreatedAt = story.CreatedAt;
                storyView.StoryId = storyID;
               
                

            }

            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewData["userImg"] = _db.Users.ToList().Where(m => m.UserId == Convert.ToInt64(HttpContext.Session.GetString("userID"))).Select(m => m.Avatar).FirstOrDefault();
            }


            return View(storyView);
        }

        [HttpPost]
        public IActionResult ShareStory(ShareStoryViewModel storyView, IFormFileCollection? dragdrop, string action)
        {
            if (action == "submit")
            {
                if (storyView.StoryId == null || storyView.StoryId == 0)
                {
                    IEnumerable<Mission> missions = _db.Missions.ToList();
                    ViewData["mission"] = _db.MissionApplications.ToList();
                    Story story = new Story();
                    story.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    story.MissionId = storyView.MissionId;
                    story.Title = storyView.Title;
                    story.Description = storyView.editor1;
                    story.Status = "1";
                    story.CreatedAt = DateTime.Now;

                    foreach (IFormFile file in dragdrop)
                    {
                        if (file != null)
                        {
                            //Set Key Name
                            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                            //Get url To Save
                            string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\StoryImages", ImageName);

                            using (var stream = new FileStream(SavePath, FileMode.Create))
                            {
                                StoryMedium sm = new StoryMedium();
                                sm.Type = file.ContentType.ToString().Replace("image/", "");
                                sm.Path = ImageName;
                                sm.CreatedAt = DateTime.Now;
                                story.StoryMedia.Add(sm);
                                file.CopyTo(stream);
                            }
                        }
                    }

                    _db.Stories.Add(story);
                    _db.SaveChanges();
                    return RedirectToAction("StoryListing", "StoryListing");
                }
                else
                {
                    var foundstory = _db.Stories.FirstOrDefault(x => x.StoryId == storyView.StoryId);
                    foundstory.StoryId = storyView.StoryId;
                    foundstory.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    foundstory.MissionId = storyView.MissionId;
                    foundstory.Title = storyView.Title;
                    foundstory.Description = storyView.editor1;
                    foundstory.Status = "1";
                    foundstory.CreatedAt = DateTime.Now;
                    foundstory.UpdatedAt = DateTime.Now;
                    _db.Update(foundstory);
                    _db.SaveChanges();
                    return RedirectToAction("StoryListing", "StoryListing");
                }
            }
            else if (action == "save")
            {
                if (storyView.StoryId == null || storyView.StoryId == 0)
                {


                    IEnumerable<Mission> missions = _db.Missions.ToList();
                    ViewData["mission"] = _db.MissionApplications.ToList();
                    Story story = new Story();
                    story.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    story.MissionId = storyView.MissionId;
                    story.Title = storyView.Title;
                    story.Description = storyView.editor1;
                    story.Status = "Draft";
                    story.CreatedAt = DateTime.Now;

                    foreach (IFormFile file in dragdrop)
                    {
                        if (file != null)
                        {
                            //Set Key Name
                            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                            //Get url To Save
                            string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\StoryImages", ImageName);

                            using (var stream = new FileStream(SavePath, FileMode.Create))
                            {
                                StoryMedium sm = new StoryMedium();
                                sm.Type = file.ContentType.ToString().Replace("image/", "");
                                sm.Path = ImageName;
                                sm.CreatedAt = DateTime.Now;
                                story.StoryMedia.Add(sm);
                                file.CopyTo(stream);
                            }
                        }
                    }

                    _db.Stories.Add(story);
                    _db.SaveChanges();
                    return View();

                }
                else
                {
                    var foundstory = _db.Stories.FirstOrDefault(x => x.StoryId == storyView.StoryId);
                    foundstory.StoryId = storyView.StoryId;
                    foundstory.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                    foundstory.MissionId = storyView.MissionId;
                    foundstory.Title = storyView.Title;
                    foundstory.Description = storyView.editor1;
                    foundstory.Status = "Draft";
                    foundstory.CreatedAt = DateTime.Now;
                    foundstory.UpdatedAt = DateTime.Now;
                    _db.Update(foundstory);
                    _db.SaveChanges();
                    return RedirectToAction("StoryDraft", "StoryListing");

                }
            }



            else { return View(); }
        }

        public IActionResult StoryDraft()
        {
            
            var SessionUserId = HttpContext.Session.GetString("userID");
            var stories = _db.Stories.Where(u => u.Status == "Draft" && u.UserId == Convert.ToUInt32(SessionUserId)).ToList();
            
            
            List<ShareStoryViewModel> storyView = new List<ShareStoryViewModel>();

            foreach (var story in stories)
            {

                var storyuser = _db.Users.FirstOrDefault(x => x.UserId == story.UserId);
                var missiontheme = _db.Missions.FirstOrDefault(m => m.MissionId == story.MissionId).ThemeId;
                var storytheme = _db.MissionThemes.FirstOrDefault(m => m.MissionThemeId == missiontheme).Title;
                var storymedia = _db.StoryMedia.FirstOrDefault(m => m.StoryId == story.StoryId);
                storyView.Add(new ShareStoryViewModel
                {
                    StoryId = story.StoryId, 
                    MissionId = story.MissionId,
                    UserId = story.UserId,
                    StoryTitle = story.Title,
                    Themename = storytheme,
                    StoryDescription = HttpUtility.HtmlDecode(story.Description),
                    username = storyuser.FirstName,
                    lastname = storyuser.LastName,
                    Useravtar = storyuser.Avatar != null ? storyuser.Avatar : "",
                    storymediapath = storymedia != null ? storymedia.Path : "",

                });
            }
            var Storys = storyView;
            ViewBag.StoryList = storyView;


            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewData["userImg"] = _db.Users.ToList().Where(m => m.UserId == Convert.ToInt64(HttpContext.Session.GetString("userID"))).Select(m => m.Avatar).FirstOrDefault();
            } 

            return View(Storys);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Clear();

            return RedirectToAction("LandingPage", "User");
        }
    }
}
