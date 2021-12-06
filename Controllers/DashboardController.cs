using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using royal_car_rentals_web_api.Models;

namespace royal_car_rentals_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly RoyalCarRentalsDBContext _context;
        public DashboardController(RoyalCarRentalsDBContext context)
        {
            _context = context;
        }

        // GET: api/Dashboard/counts
        [HttpGet("counts")]
        public async Task<ActionResult<DashboardCounts>> GetDashboardCounts()
        {
            DashboardCounts dashboardCount = new DashboardCounts();

            dashboardCount.TotalBookings = _context.Bookings.Count();
            dashboardCount.TotalCustomers =  _context.Customers.Count();
            dashboardCount.TotalDrivers =  _context.Drivers.Count();
            dashboardCount.TotalVehicles =  _context.Vehicles.Count();

            return dashboardCount;
        }

        // GET: api/Dashboard/settingcounts
        [HttpGet("settingcounts")]
        public async Task<ActionResult<SettingCounts>> GetSettingsCounts()
        {
            SettingCounts settingCounts = new SettingCounts();

            settingCounts.TotalMakers =  _context.VehicleMakers.Count();
            settingCounts.TotalModels =  _context.VehicleModels.Count();
            settingCounts.TotalCities =  _context.Cities.Count();

            return settingCounts;
        }
    }
}
