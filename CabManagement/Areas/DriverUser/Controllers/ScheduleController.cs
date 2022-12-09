using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CabManagement.Areas.DriverUser.Controllers
{
    [Area("DriverUser")]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ScheduleController(
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
            return View(_db.CabSchedules.Include(m=>m.User).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            //var location = _db.Locations.ToList();
            //var model = new CabScheduleViewModel()
            //{
            //    From = location,
            //    To = location
            //};
            return View();
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(CabScheduleViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            if (model.FromLabel==model.ToLabel)
            {
                ModelState.AddModelError("", "From and To cannot be same");
                return View(model);

            }
            var user = await _userManager.GetUserAsync(User);
           

            _db.CabSchedules.Add(new CabSchedule()
            {
                //From = model.From,
                //To = model.To,
                //Cost = model.Cost,
                //UserId=user.Id

            }); 
            await _db.SaveChangesAsync();
            return RedirectToAction("index", "schedule", "flights");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var cab = await _db.CabSchedules.FindAsync(id);
            if (cab == null)
            {
                return NotFound();
            }
            _db.CabSchedules.Remove(cab);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await _db.CabSchedules.FindAsync(id);
            
            if (schedule == null)
            {
                return NotFound();
            }
           
            var model = new CabScheduleViewModel()
            {
                
            };
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CabScheduleViewModel model)
        {
            var cab = await _db.CabSchedules.FindAsync(id);
            if (cab == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.FromLabel == model.ToLabel)
            {
                ModelState.AddModelError("", "From and To cannot be same");
                return View(model);

            }
            var user = await _userManager.GetUserAsync(User);


            _db.CabSchedules.Add(new CabSchedule()
            {
                //From = model.From,
                //To = model.To,
                //Cost = model.Cost,
                //UserId = user.Id

            });
            await _db.SaveChangesAsync();
            return RedirectToAction("index", "schedule", "flights");
        }


    }
}
