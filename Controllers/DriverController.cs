using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using royal_car_rentals_web_api.Models;

namespace royal_car_rentals_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly RoyalCarRentalsDBContext _context;

        public DriverController(RoyalCarRentalsDBContext context)
        {
            _context = context;
        }

        // GET: api/Driver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return await _context.Drivers.Where(d => d.Id != 1).ToListAsync();
        }

        // GET api/Driver/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            Driver driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }

        // POST api/Driver
        [HttpPost]
        public async Task<ActionResult<Driver>> PostVehicle([FromForm] FileUpload formdata)
        {
            var driver = JsonConvert.DeserializeObject<Driver>(formdata.Info);

            driver.City = null;

            driver.DateAdded = DateTime.Now;
            driver.DateUpdated = DateTime.Now;
            driver.ProfilePicture = "";

            if (formdata.Files != null)
            {
                try
                {
                    string path = Path.Combine("D:\\UNI\\FYP\\royal-car-rentals-website\\src\\assets\\adminUploads", "drivers");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var file = formdata.Files[0];

                    if (file.Length > 0)
                    {
                        string fileName = driver.FirstName.Replace(" ", "") + driver.LastName.Replace(" ", "") + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "" + Path.GetExtension(file.FileName);
                        string fullPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        driver.ProfilePicture = fileName;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriver", new { id = driver.Id }, driver);
        }

        // PUT api/Driver/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Driver>> PutDriver(int id, [FromForm] FileUpload formdata)
        {
            var driver = JsonConvert.DeserializeObject<Driver>(formdata.Info);
            driver.City = null;
            driver.DateUpdated = DateTime.Now;

            if (id != driver.Id)
            {
                return BadRequest();
            }

            if (formdata.Files != null)
            {
                try
                {
                    string path = Path.Combine("D:\\UNI\\FYP\\royal-car-rentals-website\\src\\assets\\adminUploads", "drivers");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var file = formdata.Files[0];

                    if (file.Length > 0)
                    {
                        string fileName = driver.FirstName.Replace(" ", "") + driver.LastName.Replace(" ", "") + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "" + Path.GetExtension(file.FileName);
                        string fullPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        driver.ProfilePicture = fileName;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            _context.Entry(driver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return driver;
        }

        // DELETE api/Driver/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }

        // GET: api/Driver/counts
        [HttpGet("counts")]
        public async Task<ActionResult<DriverCounts>> GetCounts()
        {
            DriverCounts driverCounts = new DriverCounts();

            driverCounts.ActiveDrivers = await _context.Drivers.Where(a => a.Id != 1 && a.IsActive == true).CountAsync();
            driverCounts.InActiveDrivers = await _context.Drivers.Where(a => a.Id != 1 && a.IsActive == false).CountAsync();
            driverCounts.AvailableDrivers = await _context.Drivers.Where(a => a.Id != 1 && a.Availability == true && a.IsActive == true).CountAsync();
            driverCounts.BookedDrivers = await _context.Drivers.Where(a => a.Id != 1 && a.Availability == false && a.IsActive == true).CountAsync();

            return driverCounts;
        }
    }
}
