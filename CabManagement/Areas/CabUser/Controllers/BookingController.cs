using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CabManagement.Areas.CabUser.Controllers
{
    [Area("Cabuser")]

    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BookingController(
            ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Search()
        {
            
            var model = new BookingViewModel()
            {
                Pickup = location,
                Destination = location
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Search(BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            if (model.DestinationLabel == model.PickupLabel)
            {
                ModelState.AddModelError("", "From and To cannot be same");
                return View(model);

            }
            else
            {
                return RedirectToAction("CabList", new {model.PickupLabel, model.DestinationLabel});
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> CabList(string pickup,string destination)
        {
            //var model = await _db.CabSchedules.Where(m => m.From == pickup && m.To == destination);
            Console.WriteLine(pickup,destination);
            return View();
        }

    }
}
