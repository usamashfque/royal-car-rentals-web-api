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
    public class VehicleController : ControllerBase
    {
        private readonly RoyalCarRentalsDBContext _context;
        private IWebHostEnvironment _iWebHostingEnvironment;

        public VehicleController(RoyalCarRentalsDBContext context, IWebHostEnvironment iWebHostingEnvironment)
        {
            _context = context;
            _iWebHostingEnvironment = iWebHostingEnvironment;
        }

        // GET: api/<VehicleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var __vehicles = (from _vehicle in _context.Vehicles
                              join _maker in _context.VehicleMakers on _vehicle.MakerId equals _maker.Id
                              join _model in _context.VehicleModels on _vehicle.ModelId equals _model.Id
                              join _city in _context.Cities on _vehicle.CityId equals _city.Id
                              select new Vehicle()
                              {
                                  Id = _vehicle.Id,
                                  MakerId = _vehicle.MakerId,
                                  ModelId = _vehicle.ModelId,
                                  CityId = _vehicle.CityId,
                                  ModelYear = _vehicle.ModelYear,
                                  RegistrationNumber = _vehicle.RegistrationNumber,
                                  Color = _vehicle.Color,
                                  Status = _vehicle.Status,
                                  Availability = _vehicle.Availability,
                                  Price = _vehicle.Price,
                                  ImagesPath = _vehicle.ImagesPath,
                                  DateAdded = _vehicle.DateAdded,
                                  DateUpdated = _vehicle.DateUpdated,
                                  Maker = _maker,
                                  Model = _model,
                                  City = _city
                              }).ToList();



            return __vehicles;
            // return await _dbContext.Vehicles.ToListAsync();
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var __vehicles = (from _vehicle in _context.Vehicles
                              join _maker in _context.VehicleMakers on _vehicle.MakerId equals _maker.Id
                              join _model in _context.VehicleModels on _vehicle.ModelId equals _model.Id
                              join _city in _context.Cities on _vehicle.CityId equals _city.Id
                              select new Vehicle()
                              {
                                  Id = _vehicle.Id,
                                  MakerId = _vehicle.MakerId,
                                  ModelId = _vehicle.ModelId,
                                  CityId = _vehicle.CityId,
                                  ModelYear = _vehicle.ModelYear,
                                  RegistrationNumber = _vehicle.RegistrationNumber,
                                  Color = _vehicle.Color,
                                  Status = _vehicle.Status,
                                  Availability = _vehicle.Availability,
                                  Price = _vehicle.Price,
                                  ImagesPath = _vehicle.ImagesPath,
                                  DateAdded = _vehicle.DateAdded,
                                  DateUpdated = _vehicle.DateUpdated,
                                  Maker = _maker,
                                  Model = _model,
                                  City = _city
                              }).Where(a => a.Id == id).FirstOrDefault();

            return __vehicles;
        }

        // POST api/<VehicleController>
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromForm] FileUpload formdata)
        {

            var vehicle = JsonConvert.DeserializeObject<Vehicle>(formdata.Info);

            vehicle.City = null;
            vehicle.Maker = null;
            vehicle.Model = null;

            vehicle.DateAdded = DateTime.Now;
            vehicle.DateUpdated = DateTime.Now;

            vehicle.ImagesPath = "";

            if (formdata.Files != null)
            {
                try
                {
                    //string path = Path.Combine(this._iWebHostingEnvironment.ContentRootPath, "uploads");

                    string path = Path.Combine("D:\\UNI\\FYP\\royal-car-rentals-website\\src\\assets\\adminUploads", "vehicles");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    foreach (var file in formdata.Files)
                    {
                        if (file.Length > 0)
                        {
                            //string fileNameXX = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                            //vehicle.MakerId.ToString() +
                            string fileName = vehicle.ModelId.ToString() + vehicle.ModelYear + "" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "" + Path.GetExtension(file.FileName);

                            string fullPath = Path.Combine(path, fileName);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            if (vehicle.ImagesPath == "")
                            {
                                vehicle.ImagesPath = fileName;
                            }
                            else
                            {
                                vehicle.ImagesPath = vehicle.ImagesPath + "," + fileName;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }


            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        // PUT api/<VehicleController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Vehicle>> PutVehicle(int id, [FromBody] Vehicle vehicle)
        {
            //var vehicle = JsonConvert.DeserializeObject<Vehicle>(formdata.VehicleInfo);

            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            vehicle.DateUpdated = DateTime.Now;

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var __vehicles = (from _vehicle in _context.Vehicles
                              join _maker in _context.VehicleMakers on _vehicle.MakerId equals _maker.Id
                              join _model in _context.VehicleModels on _vehicle.ModelId equals _model.Id
                              join _city in _context.Cities on _vehicle.CityId equals _city.Id
                              select new Vehicle()
                              {
                                  Id = _vehicle.Id,
                                  MakerId = _vehicle.MakerId,
                                  ModelId = _vehicle.ModelId,
                                  CityId = _vehicle.CityId,
                                  ModelYear = _vehicle.ModelYear,
                                  RegistrationNumber = _vehicle.RegistrationNumber,
                                  Color = _vehicle.Color,
                                  Status = _vehicle.Status,
                                  Availability = _vehicle.Availability,
                                  Price = _vehicle.Price,
                                  ImagesPath = _vehicle.ImagesPath,
                                  DateAdded = _vehicle.DateAdded,
                                  DateUpdated = _vehicle.DateUpdated,
                                  Maker = _maker,
                                  Model = _model,
                                  City = _city
                              }).Where(a => a.Id == id).FirstOrDefault();

            return __vehicles;
        }

        // PUT api/<VehicleController>/uploadpicture/5
        [HttpPut("uploadpicture/{id}")]
        public async Task<ActionResult<Vehicle>> UploadPictureVehicle(int id, [FromForm] FileUpload formdata)
        {
            var vehicleOld = await _context.Vehicles.FindAsync(id);

            if (formdata.Files != null)
            {
                try
                {
                    //string path = Path.Combine(this._iWebHostingEnvironment.ContentRootPath, "uploads");

                    string path = Path.Combine("D:\\UNI\\FYP\\royal-car-rentals-website\\src\\assets\\adminUploads", "vehicles");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    foreach (var file in formdata.Files)
                    {
                        if (file.Length > 0)
                        {
                            //string fileNameXX = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                            //vehicleOld.MakerId.ToString() +
                            string fileName = $"{vehicleOld.ModelId.ToString()}{vehicleOld.ModelYear}{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Path.GetExtension(file.FileName)}";

                            string fullPath = Path.Combine(path, fileName);

                            //await file.CopyToAsync(System.IO.File.Create(fullPath));

                            using (Stream stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            if (vehicleOld.ImagesPath == "")
                            {
                                vehicleOld.ImagesPath = fileName;
                            }
                            else
                            {
                                vehicleOld.ImagesPath = vehicleOld.ImagesPath + "," + fileName;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            vehicleOld.DateUpdated = DateTime.Now;

            _context.Entry(vehicleOld).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            var __vehicles = (from _vehicle in _context.Vehicles
                              join _maker in _context.VehicleMakers on _vehicle.MakerId equals _maker.Id
                              join _model in _context.VehicleModels on _vehicle.ModelId equals _model.Id
                              join _city in _context.Cities on _vehicle.CityId equals _city.Id
                              select new Vehicle()
                              {
                                  Id = _vehicle.Id,
                                  MakerId = _vehicle.MakerId,
                                  ModelId = _vehicle.ModelId,
                                  CityId = _vehicle.CityId,
                                  ModelYear = _vehicle.ModelYear,
                                  RegistrationNumber = _vehicle.RegistrationNumber,
                                  Color = _vehicle.Color,
                                  Status = _vehicle.Status,
                                  Availability = _vehicle.Availability,
                                  Price = _vehicle.Price,
                                  ImagesPath = _vehicle.ImagesPath,
                                  DateAdded = _vehicle.DateAdded,
                                  DateUpdated = _vehicle.DateUpdated,
                                  Maker = _maker,
                                  Model = _model,
                                  City = _city
                              }).Where(a => a.Id == id).FirstOrDefault();

            return __vehicles;
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }

        // GET: api/<VehicleController>/approved
        [HttpGet("approved")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetApprovedVehicles()
        {
            var __vehicles = (from _vehicle in _context.Vehicles
                              join _maker in _context.VehicleMakers on _vehicle.MakerId equals _maker.Id
                              join _model in _context.VehicleModels on _vehicle.ModelId equals _model.Id
                              join _city in _context.Cities on _vehicle.CityId equals _city.Id
                              select new Vehicle()
                              {
                                  Id = _vehicle.Id,
                                  MakerId = _vehicle.MakerId,
                                  ModelId = _vehicle.ModelId,
                                  CityId = _vehicle.CityId,
                                  ModelYear = _vehicle.ModelYear,
                                  RegistrationNumber = _vehicle.RegistrationNumber,
                                  Color = _vehicle.Color,
                                  Status = _vehicle.Status,
                                  Availability = _vehicle.Availability,
                                  Price = _vehicle.Price,
                                  ImagesPath = _vehicle.ImagesPath,
                                  DateAdded = _vehicle.DateAdded,
                                  DateUpdated = _vehicle.DateUpdated,
                                  Maker = _maker,
                                  Model = _model,
                                  City = _city
                              }).Where(a => a.Status == "approved").ToList();

            if (__vehicles == null)
            {
                return NotFound();
            }

            return __vehicles;
        }

        // GET api/<VehicleController>/bymaker/honda
        [HttpGet("bymaker/{makerId}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicleByMakerName(int makerId)
        {
            // var vehicle = await _dbContext.Vehicles.Where(a => a.MakerId == makerId).ToListAsync();

            var vehicles = (from _vehicle in _context.Vehicles
                            join _maker in _context.VehicleMakers on _vehicle.MakerId equals _maker.Id
                            join _model in _context.VehicleModels on _vehicle.ModelId equals _model.Id
                            join _city in _context.Cities on _vehicle.CityId equals _city.Id
                            select new Vehicle()
                            {
                                Id = _vehicle.Id,
                                MakerId = _vehicle.MakerId,
                                ModelId = _vehicle.ModelId,
                                CityId = _vehicle.CityId,
                                ModelYear = _vehicle.ModelYear,
                                RegistrationNumber = _vehicle.RegistrationNumber,
                                Color = _vehicle.Color,
                                Status = _vehicle.Status,
                                Availability = _vehicle.Availability,
                                Price = _vehicle.Price,
                                ImagesPath = _vehicle.ImagesPath,
                                DateAdded = _vehicle.DateAdded,
                                DateUpdated = _vehicle.DateUpdated,
                                Maker = _maker,
                                Model = _model,
                                City = _city
                            }).Where(a => a.MakerId == makerId && a.Status == "approved").ToList();

            if (vehicles == null)
            {
                return NotFound();
            }

            return vehicles;
        }

        // GET: api/<VehicleController>/counts
        [HttpGet("counts")]
        public async Task<ActionResult<VehicleCounts>> GetCounts()
        {
            VehicleCounts vehicleCounts = new VehicleCounts();

            vehicleCounts.TotalVehicles = await _context.Vehicles.CountAsync();
            vehicleCounts.PendingVehicles = await _context.Vehicles.Where(a => a.Status == "pending").CountAsync();
            vehicleCounts.ApprovedVehicles = await _context.Vehicles.Where(a => a.Status == "approved").CountAsync();
            vehicleCounts.BlockedVehicles = await _context.Vehicles.Where(a => a.Status == "blocked").CountAsync();
            vehicleCounts.AvailableVehicles = await _context.Vehicles.Where(a => a.Availability == true && a.Status == "approved").CountAsync();
            vehicleCounts.BookedVehicles = await _context.Vehicles.Where(a => a.Availability == false && a.Status == "approved").CountAsync();

            return vehicleCounts;
        }
    }
}
