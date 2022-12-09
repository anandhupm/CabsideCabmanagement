using CabManagement.Data;
using CabManagement.Models;
using CabManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CabManagement.Areas.DriverUser.Controllers
{
    [Area("DriverUser")]
    [Authorize(Roles = "Driver")]

    public class DriverCabController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DriverCabController(
            ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var cabDetails = await _db.CabDetails.Where(m => m.UserId == user.Id)
                .ToListAsync();
            return View(cabDetails);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CabDetailsViewModel model)
        {
           var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
                return View();
            _db.CabDetails.Add(new CabDetail()
            {
                Name = model.Name,
                VehicleNumber = model.VehicleNumber,
                ModelNumber = model.ModelNumber,
                Type = model.Type,
                Capacity = model.Capacity,
                Description = model.Description,
                UserId = user.Id
            });
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "DriverCab", "DriverUser");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cab = await _db.CabDetails.FindAsync(id);
            if (cab == null)
            {
                return NotFound();
            }
            return View(new CabDetailsViewModel()
            {
                VehicleNumber= cab.VehicleNumber,
                Name= cab.Name,
                ModelNumber= cab.ModelNumber,
                Type = cab.Type,
                Capacity = cab.Capacity,
                Description = cab.Description,

            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CabDetailsViewModel model)
        {
            var cab = await _db.CabDetails.FindAsync(id);
            if (cab == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            cab.Name = model.Name;
            cab.ModelNumber = model.ModelNumber;
            cab.Type = model.Type;
            cab.Capacity = model.Capacity;
            cab.Description = model.Description;
            cab.VehicleNumber= model.VehicleNumber;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var cab = await _db.CabDetails.FindAsync(id);
            if (cab == null)
            {
                return NotFound();
            }
            _db.CabDetails.Remove(cab);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }

    
}
