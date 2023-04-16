using CI_Entities.CIPlatformContext;
using Microsoft.AspNetCore.Mvc;
using CI_Entities.Models;
using CI_Project.Models;
using Microsoft.AspNetCore.Http.Extensions;
using System.Reflection;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Net.Mail;
using System.Net;
using CI_Project.Repository.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CI_Project.Controllers
{

    public class UserController : Controller
    {
        int co = 0;

        private readonly CIPlatformContext _db;
        private readonly IUserInterface _Iuser;

        public UserController(CIPlatformContext db, IUserInterface Iuser)
        {
            _Iuser = Iuser;
            _db = db; //underscore db ma baddho database store thai jase
        }
        public IActionResult Registration()
        {
           return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NoMissionFound()
        {
            return View();
        }
          public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public async Task<IActionResult> apply(int MissionId, int UserId)
        {
            _Iuser.apply(MissionId, UserId);

            return Json(new { success = true });
        }
        public IActionResult AddToFav(long missionid,long id)
         {
            
            var isFav = _Iuser.favoriteMissions().FirstOrDefault(m => m.MissionId == missionid && m.UserId == id);
            if (isFav == null)
            {
                _Iuser.Addfavorite(missionid,id);
            }
            else
            {
                _Iuser.removefavorite(missionid, id);
            }
            return RedirectToAction("VolunteeringMission", "User", new { id = id, missionid = missionid });
            //return RedirectToAction("VolunteeringMission" , new{ id=id, missionid=missionid});
            //return re(new {success = true, isFav});
        }

        

        [HttpPost]
        public async Task<IActionResult> AddRating(string rating, long id, long missionId)
        {
            MissionRating ratingExists =  _Iuser.missionRatings().FirstOrDefault(fm => fm.UserId == id && fm.MissionId == missionId);

            if (ratingExists != null)
            {
                ratingExists.Rating = int.Parse(rating);
                //ratingExists.UserId = id;
                //ratingExists.MissionId = missionId;
                _db.MissionRatings.Update(ratingExists);
                await _db.SaveChangesAsync();
                //return Json(new { success = true, ratingExists, isRated = true });
 
            }
            else
            {
                var newRating = new MissionRating();
                newRating.Rating = int.Parse(rating); 
                newRating.UserId = id;
                newRating.MissionId = missionId;
                await _db.MissionRatings.AddAsync(newRating); await _db.SaveChangesAsync();
                //return Json(new { success = true, newRating, isRated = true });
            }
            return RedirectToAction("VolunteeringMission", "User", new { missionId = missionId });
        }


        [HttpPost]
        //< ========================== send recomodation ======================================= >
        public async Task<IActionResult> sendRecom(long Id, long missionid, string[] Email,string currentURL)
        {
            foreach (var email in Email)
            {
                //var user = _db.Users.FirstOrDefault(m => m.Email == email);
                var user =_Iuser.UserByEmail(email);
                var sender = _Iuser.users().FirstOrDefault(m => m.UserId == Id);
                var sendername = sender.FirstName + $" " + sender.LastName;
                var userid = user.UserId;
                var resetLink = Url.Action("VolunteeringMission", "User", new { missionid = missionid, id = userid }, Request.Scheme);
                // Send email to user with reset password link
                // ...

                var fromAddress = new MailAddress("testermaster43@gmail.com", "Community Empowerment Portal");
                var toAddress = new MailAddress(email);
                var subject = "Password reset request";
                var body = $"Hi,<br /><br /> you are recomanded a mission by {sendername} Please click on the following link to see recomanded mission detail:<br /><br /><a href='{currentURL}'>{currentURL}</a>";
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("testermaster43@gmail.com", "rnonuvkukadwpnpx"),
                    EnableSsl = true
                };
            smtpClient.Send(message);

        }
            return Json(new { success = true });
        }

        public IActionResult VolunteeringMission( long id, long missionid, VolunteeringViewModel volunteeringViewModel)
        {
            var userId = HttpContext.Session.GetString("userID");

            
            
            
            ViewBag.UserId = Convert.ToInt64(userId);   

            List<Mission> missions = _Iuser.missionlist();
            List<Mission> newmission = _Iuser.missionlist();
            List<Mission> missionfound = _Iuser.missionlist();
            List<City> cities = _Iuser.cities();
            List<Country> countries = _Iuser.countries();
            List<MissionTheme> themes = _Iuser.themes();
            List<Skill> skills = _Iuser.skills();

            ViewBag.cityList = cities;
            ViewBag.countryList = countries;
            ViewBag.themeList = themes;
            ViewBag.skillsList = skills;
            ViewBag.User = _db.Users.FirstOrDefault(e => e.UserId == id);


            List<VolunteeringViewModel> relatedlist = new List<VolunteeringViewModel>();

            VolunteeringViewModel volunteeringVM = new VolunteeringViewModel();
            IEnumerable<Mission> objMis = _Iuser.missionlist();
            IEnumerable<Comment> objComm = _Iuser.comments();
            IEnumerable<FavoriteMission> fav = _Iuser.favoriteMissions().Where(m => m.MissionId == missionid).ToList();
            IEnumerable<Mission> selected = _Iuser.missionlist().Where(m => m.MissionId == missionid).ToList();
            var applied =(id!=null)? _Iuser.missionApplications().Any(m => m.UserId == int.Parse(userId) && m.MissionId == missionid):false;
            var volmission = _Iuser.missionlist().FirstOrDefault(m => m.MissionId == missionid);
            var theme = _Iuser.themes().FirstOrDefault(m => m.MissionThemeId == volmission.ThemeId);
            var City = _Iuser.cities().FirstOrDefault(m => m.CityId == volmission.CityId);
            var prevRating = _Iuser.missionRatings().Where(e => e.MissionId == missionid && e.UserId == id).FirstOrDefault();
            var themeobjective = _Iuser.goalMissions().FirstOrDefault(m => m.MissionId == missionid);
            string[] Startdate = volmission.StartDate.ToString().Split(" ");
            string[] Enddate = volmission.EndDate.ToString().Split(" ");
            volunteeringVM.UserPrevRating = prevRating != null ? prevRating.Rating : 0;
            var favrioute = (id != null) ? _Iuser.favoriteMissions().Any(u => u.UserId == Convert.ToInt64(userId) && u.MissionId == volmission.MissionId) : false;

            
            volunteeringVM.MissionId = missionid;
            volunteeringVM.Title = volmission.Title;
            volunteeringVM.ShortDescription = volmission.ShortDescription;
            volunteeringVM.OrganizationName= volmission.OrganizationName; 
            volunteeringVM.Description = volmission.Description;
            volunteeringVM.OrganizationDetail = volmission.OrganizationDetail;
            volunteeringVM.Availability = volmission.Availability;
            volunteeringVM.MissionType = volmission.MissionType;
            volunteeringVM.Cityname = City.Name;
            volunteeringVM.Themename = theme.Title;
            volunteeringVM.EndDate = Enddate[0];
            volunteeringVM.StartDate = Startdate[0];
            volunteeringVM.isapplied = applied;
            volunteeringVM.isfav = favrioute;
            volunteeringVM.UserId = Convert.ToInt64(userId);


            if (prevRating!= null) { volunteeringVM.UserPrevRating = prevRating.Rating; }
            
            
            volunteeringVM.GoalObjectiveText = themeobjective.GoalObjectiveText;
            ViewBag.Missiondetail = volunteeringVM;


            List<User> alluser = _Iuser.users().ToList();
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
            ViewBag.allavailuser = allavailuser;

            var relatedmission = _Iuser.missionlist().Where(m => m.ThemeId == volmission.ThemeId && m.MissionId != missionid).ToList();
            foreach (var item in relatedmission.Take(3))
            {

                var relcity = _Iuser.cities().FirstOrDefault(m => m.CityId == item.CityId);
                var reltheme = _Iuser.themes().FirstOrDefault(m => m.MissionThemeId == item.ThemeId);
                var relgoalobj = _Iuser.goalMissions().FirstOrDefault(m => m.MissionId == item.MissionId);
                string[] Startdate1 = item.StartDate.ToString().Split(" ");
                string[] Enddate2 = item.EndDate.ToString().Split(" ");



                relatedlist.Add(new VolunteeringViewModel
                {
                    MissionId = item.MissionId,
                    Cityname = relcity.Name,
                    Themename = reltheme.Title,
                    Title = item.Title,
                    ShortDescription = item.ShortDescription,
                    StartDate = Startdate1[0],
                    EndDate = Enddate2[0],
                    Availability = item.Availability,
                    OrganizationName = item.OrganizationName,
                    GoalObjectiveText = relgoalobj.GoalObjectiveText,
                    MissionType = item.MissionType,

                }
                );
                ;
            }


            ViewBag.relatedmission = relatedlist.Take(3);

            List<VolunteeringViewModel> recentvolunteredlist = new List<VolunteeringViewModel>();
            //var recentvolunttered = from U in CID.Users join MA in CiMainContext.MissionApplications on U.UserId equals MA.UserId where MA.MissionId == mission.MissionId select U;
            var recentvoluntered = from U in _Iuser.users() join MA in _Iuser.missionApplications() on U.UserId equals MA.UserId where MA.MissionId == missionid select U;
            foreach (var item in recentvoluntered)
            {


                recentvolunteredlist.Add(new VolunteeringViewModel
                {
                    username = item.FirstName,
                });

            }
            ViewBag.recentvolunteered = recentvolunteredlist;

            return View(selected);
        }


        public IActionResult AddComment(int missionId, string commentText)
        {
            Comment objComment = new Comment();
            objComment.UserId = Convert.ToInt64(HttpContext.Session.GetString("userID"));
            objComment.MissionId = missionId;
            objComment.CommentText = commentText;
            objComment.CreatedAt = DateTime.Now;
            //_db.Comments.Add(objComment);
            //_db.SaveChanges();
            _Iuser.comment(objComment);
            return RedirectToAction("VolunteeringMission", new { id = Convert.ToInt64(HttpContext.Session.GetString("userID")), missionid = missionId });
            //return Json(objComment);
        }



        public IActionResult landingPage(long userId, int id, int missionid, string? search, int? pageIndex, string? sortValue, string[] country, string[] city, string[] theme)
        {
            var SessionUserId = HttpContext.Session.GetString("userID");
            //var UserId = HttpContext.Session.GetString("userID");
            ViewData["applicaions"] = _Iuser.missionApplications();
            ViewBag.countrylist = _Iuser.countries();
            ViewBag.themelist = _Iuser.themes();
            ViewBag.citylist = _Iuser.cities();
            return View();

        }





        //#region landingPage
        //public IActionResult LandingPage(int? page, String SearchingMission, int Order, String searchQuery, long[] fCountries)
        //{
        //    var mission = _Iuser.missionlist();
        //    IEnumerable<MissionRating> fav = _Iuser.missionratingslist();

        //    switch (Order) 
        //    {
        //        case 2:
        //            mission = mission.OrderByDescending(e => e.StartDate).ToList();
        //            break;
        //        case 3:
        //            mission = mission.OrderBy(e => e.StartDate).ToList();
        //            break;
        //        case 4:
        //            mission = mission.OrderBy(e => int.Parse(e.Availability)).ToList();
        //            break;        
        //        case 5:
        //            mission = mission.OrderByDescending(e => int.Parse(e.Availability)).ToList();
        //            break;
        //        case 6:
        //            mission = mission.OrderBy(e => e.EndDate).ToList();
        //            break;
        //        default:
        //            mission = mission.OrderBy(e => e.Theme).ToList();
        //            break;

        //            // return View(mission.ToList());
        //    }


        //    List<Mission> missions = _Iuser.missionlist(); /*_db.Missions.ToList();*/
        //    List<Mission> newmission = _Iuser.missionlist();
        //    List<Mission> missionfound = _Iuser.missionlist();
        //    List<City> cities = _Iuser.cities();
        //    List<Country> countries = _Iuser.countries();
        //    List<MissionTheme> themes = _Iuser.themes();
        //    List<Skill> skills = _Iuser.skills();
        //    List<GoalMission> goalMissions = _Iuser.goalMissions();
        //    ViewBag.Goal1 = goalMissions;

        //    ViewBag.cityList = cities;
        //    ViewBag.countryList = countries;
        //    ViewBag.themeList = themes;
        //    ViewBag.skillsList = skills;

        //    //Search Mission
        //    if (!string.IsNullOrEmpty(searchQuery))
        //    {
        //        mission = _Iuser.missionlist().Where(m => m.Title.Contains(searchQuery)).ToList();
        //        ViewBag.Searchinput = Request.Query["searchQuery"];

        //        if (mission.Count() == 0)
        //        {
        //            return RedirectToAction("NoMissionFound", "User");
        //        }

        //        //const int pageSize = 9; // Number of items to display per page
        //        //int pageNumber = (page ?? 1); // Default to the first page
        //        //                              // Total number of items
        //        //var items = mission
        //        //    //.OrderByDescending(i => i.CreatedDate
        //        //    .Skip((pageNumber - 1) * pageSize)
        //        //    .Take(pageSize)
        //        //    .ToList();
        //        //int total = mission.Count();
        //        //ViewBag.TotalMissions = total;

        //        //ViewBag.PageNumber = pageNumber;
        //        //ViewBag.PageSize = pageSize;
        //        //ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
        //        //ViewData["mission"] = items.ToList();

        //        //return View(items);
        //    }

        //    if (fCountries != null && fCountries.Length > 0)
        //    {
        //        foreach (var country in fCountries)
        //        {
        //            if (co == 0)
        //            {
        //                mission = mission.Where(m => m.CountryId == country + 2500).ToList();

        //                co++;
        //            }
        //            missionfound = newmission.Where(m => m.CountryId == country).ToList();
        //            mission.AddRange(missionfound);
        //            ViewBag.SearchCountryId = country;
        //            if (mission.Count() == 0)
        //            {
        //                return RedirectToAction("NoMissionFound", "User");
        //            }
        //        }
        //    }
        //        //else
        //        //{
        //        const int pageSize = 9; // Number of items to display per page
        //        int pageNumber = (page ?? 1); // Default to the first page
        //                                      // Total number of items
        //        var items = mission
        //            //.OrderByDescending(i => i.CreatedDate
        //            .Skip((pageNumber - 1) * pageSize)
        //            .Take(pageSize)
        //            .ToList();
        //        int total = mission.Count();
        //        ViewBag.TotalMissions = total;

        //        ViewBag.PageNumber = pageNumber;
        //        ViewBag.PageSize = pageSize;
        //        ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
        //        ViewData["mission"] = items.ToList();


        //    //}
        //    UriBuilder uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host);
        //    if (Request.Host.Port.HasValue)
        //    {
        //        uriBuilder.Port = Request.Host.Port.Value;
        //    }
        //    uriBuilder.Path = Request.Path;

        //    // Remove the query parameter you want to exclude
        //    var query = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        //    query.Remove("pageIndex");
        //    uriBuilder.Query = query.ToString();



        //    ViewBag.currentUrl = uriBuilder.ToString();

        //    return View(items);
        //}

        //#endregion




        #region filters

        public PartialViewResult _LandingPageCards(long userId, int id, int missionid, string? search, int? pageIndex, string? sortValue, string country, string city, string theme, int jpg)
        {
            var SessionUserId = HttpContext.Session.GetString("userID");

            ViewData["applicaions"] = _Iuser.missionApplications();

            IEnumerable<MissionApplication> missionApplications = _Iuser.missionApplications();
            List<User> alluser = _Iuser.users();
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
            ViewBag.allavailuser = allavailuser;
            List<Mission> allmission = _Iuser.missionlist();
            List<VolunteeringViewModel> mission = new List<VolunteeringViewModel>();
            foreach (Mission item in allmission)
            {
                var City = _Iuser.cities().FirstOrDefault(u => u.CityId == item.CityId);
                var Country = _Iuser.countries().FirstOrDefault(u => u.CountryId == item.CountryId);
                var Theme = _Iuser.themes().FirstOrDefault(u => u.MissionThemeId == item.ThemeId);
                var goalobj = _Iuser.goalMissions().FirstOrDefault(m => m.MissionId == item.MissionId);
                string[] Startdate1 = item.StartDate.ToString().Split(" ");
                string[] Enddate2 = item.EndDate.ToString().Split(" ");
                var favrioute = (id != null) ? _Iuser.favoriteMissions().Any(u => u.UserId == Convert.ToInt64(SessionUserId) && u.MissionId == item.MissionId) : false;
                var Applybtn = (id != null) ? _Iuser.missionApplications().Any(u => u.MissionId == item.MissionId && u.UserId == Convert.ToInt64(SessionUserId)) : false;
                ViewBag.FavoriteMissions = favrioute;
                var ratiing = _Iuser.missionRatings().Where(u => u.MissionId == item.MissionId).ToList();


                int finalrating = 0;
                if (ratiing.Count > 0)
                {
                    int rating = 0;
                    foreach (var r in ratiing)
                    {

                        rating = rating + r.Rating;


                    }
                    finalrating = rating / ratiing.Count();

                }




                mission.Add(new VolunteeringViewModel
                {
                    MissionId = item.MissionId,
                    Cityname = City.Name,
                    Countryname = Country.Name,
                    Themename = Theme.Title,
                    Title = item.Title,
                    ShortDescription = item.ShortDescription,
                    StartDate = Startdate1[0],
                    EndDate = Enddate2[0],
                    Availability = item.Availability,
                    OrganizationName = item.OrganizationName,
                    GoalObjectiveText = goalobj.GoalObjectiveText,
                    MissionType = item.MissionType,
                    AvgRating = finalrating,
                    isfav = favrioute,
                    isapplied = Applybtn,
                    UserId = Convert.ToInt64(SessionUserId),
                });
            }

            var Missions = mission.ToList();


            //Seacrh
            if (search != null)
            {
                Missions = Missions.Where(m => m.Title.ToUpper().Contains(search.ToUpper())).ToList();

            }

            ////Sort By
            ViewBag.sort = sortValue;
            switch (sortValue)
            {

                case "Newest":
                    Missions = Missions.OrderByDescending(m => m.StartDate).ToList();
                    ViewBag.sortby = "Newest";
                    break;
                case "Oldest":
                    Missions = Missions.OrderBy(m => m.StartDate).ToList();
                    ViewBag.sortby = "Oldest";
                    break;
                case "Lowest seats":
                    Missions = Missions.OrderBy(m => int.Parse(m.Availability)).ToList();
                    break;
                case "Highest seats":
                    Missions = Missions.OrderByDescending(m => int.Parse(m.Availability)).ToList();
                    break;
                case "My favourites":
                    Missions = Missions.Where(m => m.isfav == true).ToList();
                    break;
                case "Registration deadline":
                    Missions = Missions.OrderBy(m => m.EndDate).ToList();
                    break;
            }

            //filter
            if (country!= null)
            {
                string[] countryText = country.Split(',');
                Missions = Missions.Where(s => countryText.Contains(s.Countryname)).ToList();
            }
            if (city != null)
            {
                    string[] cityText = city.Split(',');

                Missions = Missions.Where(s => city.Contains(s.Cityname)).ToList();
            }
            if (theme !=null)
            {
                string[] themeText = theme.Split(',');
                Missions = Missions.Where(s => theme.Contains(s.Themename)).ToList();
            }



            //Pagination
            //int pageSize = 6;
            //int skip = (pageIndex ?? 0) * pageSize;
            //var Missionss = Missions.Skip(skip).Take(pageSize).ToList();
            //int totalMissions = mission.Count();

            //ViewBag.TotalMission = totalMissions;
            //ViewBag.TotalPages = (int)Math.Ceiling(totalMissions / (double)pageSize);
            //ViewBag.CurrentPage = pageIndex ?? 0;


            // ============================ Pagination =========================
            #region Pagination 
            //Pagination
            ViewBag.missionCount = Missions.Count();
            const int pageSize = 6;
            if (jpg < 1)
            {
                jpg = 1;
            }
            int recsCount = Missions.Count();
            var pager = new PagerViewModel(recsCount, jpg, pageSize);
            int recSkip = (jpg - 1) * pageSize;
            var data = Missions.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            ViewBag.missionTempDate = data;
            Missions = data.ToList();
            ViewBag.TotalMission = recsCount;

            //return PartialView("_LandingPageCards",);
            return PartialView("_LandingPageCards", Missions);
            
        }
        #endregion

        #endregion




        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            var status = _Iuser.UserByEmail_Password(user.Email, user.Password);
            if (status != null)
            {
                var username = _Iuser.UserByEmail(user.Email);
                HttpContext.Session.SetString("userID", username.UserId.ToString());
                HttpContext.Session.SetString("username", username.FirstName);
                TempData["Done"] = "logged in";
                return RedirectToAction("LandingPage" , new {@id = status.UserId});
            }
            else
            {
                TempData["LoginErr"] = "Invalid login credentials";
                return View();
            }
        }

        [HttpPost]

        public IActionResult Registration(RegistrationViewModel user)
        {
            if (ModelState.IsValid)
            {
                var obj = _Iuser.UserByEmail(user.Email);
                if (obj == null)
                {
                    User newUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                         Password = user.Password
                    };
                   _Iuser.adduser(newUser);
                    TempData["Done"] = "Registered Succesfully";
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.RegistrationError = "Email already exists";
                }
            }
            return View();
        }

    }
}
