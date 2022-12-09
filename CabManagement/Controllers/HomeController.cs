using CabManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CabManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            RoleManager<IdentityRole>roleManager,
            UserManager<User>userManager)
        {
            _logger = logger;
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GenerateData()
        {
            await roleManager.CreateAsync(new IdentityRole() { Name = "Passenger" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "Driver" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });


            var userTotal = await userManager.GetUsersInRoleAsync("Admin");
            if (userTotal.Count == 0)
            {
                var appuser = new User()
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "Admin@mail.com",
                    UserName = "admin"
                };
                var res = await userManager.CreateAsync(appuser, "Pass@123");
                await userManager.AddToRoleAsync(appuser, "Admin");
            }
            return Ok("Data generated");


        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}