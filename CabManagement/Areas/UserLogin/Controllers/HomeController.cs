using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CabManagement.Areas.UserLogin.Controllers
{
    [Area("UserLogin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(
            ApplicationDbContext db,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email Not Found");
                return View(model);
            }
            var res = await signInManager.PasswordSignInAsync(user, model.Password, true, true);
            if (res.Succeeded)
            {
                //var roles = await userManager.GetRolesAsync(user);
                if (await userManager.IsInRoleAsync(user, "Driver"))
                {
                    //ViewBag.Email = model.Email;
                    return RedirectToAction("index", "home", new { area = "DriverUser" });

                }
                else if (await userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("index", "home", new { area = "Admin" });

                }
                else if(await userManager.IsInRoleAsync(user, "Passenger"))
                {
                    return RedirectToAction("index", "home", new { area = "CabUser" });

                }
            }
            ModelState.AddModelError("", "Invalid Password");
            return RedirectToAction("index", "home", new { area = "" });

        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home", new { area = "" });
        }

    }
}
