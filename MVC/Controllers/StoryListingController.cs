using CI_Entities.CIPlatformContext;
using CI_Entities.Models;
using CI_Project.Models;
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
        public IActionResult StoryListing(int? page)
        {
            List<Story> storylist = _db.Stories.ToList();
            List<VolunteeringViewModel> StoryList = new List<VolunteeringViewModel>();
            foreach (var story in storylist)
            {
                var storyuser = _db.Users.FirstOrDefault(x => x.UserId == story.UserId);
                StoryList.Add(new VolunteeringViewModel
                {
                    StoryId = story.StoryId,
                    MissionId = story.MissionId,
                    UserId = story.UserId,
                    StoryTitle = story.Title,
                    ShortDescription = story.Description,
                    username = storyuser.FirstName,
                    lastname = storyuser.LastName,

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

        public IActionResult ShareStory()
        {
            return View();
        }
    }
}
