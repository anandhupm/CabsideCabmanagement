using CabManagement.Data;
using CabManagement.Models;
using CabManagement.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CabManagement.Areas.CabUser.Controllers
{
    [Area("CabUser")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(
            ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager )
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CabUserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                UserName = Guid.NewGuid().ToString().Replace("-", "")
            };
            var res = await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, "Passenger");

            if (res.Succeeded)
            {
                return RedirectToAction("login", "home", new { Area = "Userlogin" });

            }
            ModelState.AddModelError("", "Something Went Wrong...");
            return View(model);


        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {


            //IEnumerable<long> Id = (IEnumerable<long>)(from currentUser in db.Users where currentUser.UserName == username select currentUser.Id);


            var user = await userManager.FindByNameAsync(User.Identity.Name);
            //var user = await db.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            return View(new CabUserRegisterViewModel()
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                Phone=user.PhoneNumber
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CabUserRegisterViewModel model)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            user.FirstName= model.FirstName;
            user.LastName= model.LastName;
            user.Email= model.Email;
            user.PhoneNumber = model.Phone;
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

    }
}
