using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace project_management.Controllers
{
    public class ShareTheTasksController : Controller
    {
       private UserManager<IdentityUser> _userManager;
        public ShareTheTasksController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager; 
        }
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View();
        }
       
    }
}
