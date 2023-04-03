using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace CI_Project.Controllers
{
    public class StoryListingController : Controller
    {
        private readonly CIPlatformContext _db;
         
        public StoryListingController(CIPlatformContext db)
        {
            _db = db; //underscore db ma baddho database store thai jase
        }

        public IActionResult StoryDetail(long id, long storyid)
        {
            var userId = HttpContext.Session.GetString("userID");

            ViewBag.UserId = Convert.ToInt64(userId);



            return View();
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
            int pageNumber = (page ?? 1); // Default to the first page
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

            return View(items);

        }

        public IActionResult ShareStory(long storyID)
        {
            IEnumerable<Mission> missions = _db.Missions.ToList();
            ViewData["mission"] = _db.MissionApplications.ToList();

            var UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));

            ShareStoryViewModel storyView = new ShareStoryViewModel();

            if (storyID != 0)
            {
                storyView.storyMedia = _db.StoryMedia.Where(e => e.StoryId == storyID).ToList();
                storyView.MissionApplications= _db.MissionApplications.Where(u => u.UserId == UserId).ToList();

                var story = _db.Stories.Where(e => e.StoryId == storyID).FirstOrDefault();
                storyView.MissionId = story.MissionId;
                storyView.Title = story.Title;
                storyView.editor1 = story.Description;
                storyView.CreatedAt = story.CreatedAt;
                storyView.StoryId = storyID;

            }

            return View();
        }

        [HttpPost]
        public IActionResult ShareStory(ShareStoryViewModel shareStoryView, IFormFileCollection? dragdrop , string action)
        {

            if(action == "submit")
            {
                IEnumerable<Mission> missions = _db.Missions.ToList();
                ViewData["mission"] = _db.MissionApplications.ToList();
                Story story = new Story();
                story.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                story.MissionId = shareStoryView.MissionId;
                story.Title = shareStoryView.Title;
                story.Description = shareStoryView.editor1;
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
                return View();
            }
            else if(action == "save")
            {
                IEnumerable<Mission> missions = _db.Missions.ToList();
                ViewData["mission"] = _db.MissionApplications.ToList();
                Story story = new Story();
                story.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
                story.MissionId = shareStoryView.MissionId;
                story.Title = shareStoryView.Title;
                story.Description = shareStoryView.editor1;
                story.Status = "DRAFT";
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

            else { return View(); }

            
        }
        public IActionResult StoryDraft()
        {

            var SessionUserId = HttpContext.Session.GetString("userID");

            ShareStoryViewModel storyView = new ShareStoryViewModel();
            storyView.story = _db.Stories.Where(u => u.Status == "Draft" && u.UserId == Convert.ToUInt32(SessionUserId)).ToList();
            storyView.missions = _db.Missions.ToList();
            storyView.missionthemes = _db.MissionThemes.ToList();
            storyView.storyMedia = _db.StoryMedia.ToList();
            storyView.users = _db.Users.ToList();
            return View(storyView);
        }
    }
}
