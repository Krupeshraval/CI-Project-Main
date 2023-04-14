using Microsoft.AspNetCore.Mvc;

namespace CI_Project.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult UserProfile()
        {
            return View();
        }

    }
}
