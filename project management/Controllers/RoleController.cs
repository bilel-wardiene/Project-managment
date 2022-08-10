using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace project_management.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager  , UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [Authorize (Roles = "Admin")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View(new IdentityRole() );
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetMyRoles()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var role = await _userManager.GetRolesAsync(user);
            return View(role);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AllUser()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var roles = await _roleManager.FindByIdAsync(id);
            if (roles == null)
            {
                return View("Error");
            }
            else
            {
             
                _roleManager.DeleteAsync(roles);
                await _roleManager.UpdateAsync(roles);
                return RedirectToAction("Index");
            }
        }
       
        
    }
}
