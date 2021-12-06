using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using royal_car_rentals_web_api.Models;

namespace royal_car_rentals_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private readonly RoyalCarRentalsDBContext _context;

        public VehicleModelController(RoyalCarRentalsDBContext context)
        {
            _context = context;
        }

        // GET: api/VehicleModel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleModel>>> GetVehicleModels()
        {
            var __vehicleModels = await (from _models in _context.VehicleModels
                                   join _maker in _context.VehicleMakers on _models.MakerId equals _maker.Id
                                   select new VehicleModel()
                                   {
                                       Id = _models.Id,
                                       MakerId = _models.MakerId,
                                       DisplayName = _models.DisplayName,
                                       DateAdded = _models.DateAdded,
                                       DateUpdated = _models.DateUpdated,
                                       Maker = _maker
                                   }).ToListAsync();

            return __vehicleModels;
        }

        // GET: api/VehicleModel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModel>> GetVehicleModel(int id)
        {
            var vehicleModel = await _context.VehicleModels.FindAsync(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return vehicleModel;
        }

        // PUT: api/VehicleModel/5
        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleModel>> PutVehicleModel(int id, VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return BadRequest();
            }
     
            vehicleModel.DateUpdated = DateTime.Now;

            _context.Entry(vehicleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var __vehicleModel = (from _models in _context.VehicleModels
                                  join _maker in _context.VehicleMakers on _models.MakerId equals _maker.Id
                                  select new VehicleModel()
                                  {
                                      Id = _models.Id,
                                      MakerId = _models.MakerId,
                                      DisplayName = _models.DisplayName,
                                      DateAdded = _models.DateAdded,
                                      DateUpdated = _models.DateUpdated,
                                      Maker = _maker
                                  }).Where(w => w.Id == vehicleModel.Id).FirstOrDefault();

            return __vehicleModel;
        }
        // POST: api/VehicleModel
        [HttpPost]
        public async Task<ActionResult<VehicleModel>> PostVehicleModel(VehicleModel vehicleModel)
        {
            vehicleModel.DateAdded = DateTime.Now;
            vehicleModel.DateUpdated = DateTime.Now;

            _context.VehicleModels.Add(vehicleModel);
            await _context.SaveChangesAsync();


            var __vehicleModel = (from _models in _context.VehicleModels
                                  join _maker in _context.VehicleMakers on _models.MakerId equals _maker.Id
                                  select new VehicleModel()
                                  {
                                      Id = _models.Id,
                                      MakerId = _models.MakerId,
                                      DisplayName = _models.DisplayName,
                                      DateAdded = _models.DateAdded,
                                      DateUpdated = _models.DateUpdated,
                                      Maker = _maker
                                  }).Where(w => w.Id == vehicleModel.Id).FirstOrDefault();

            return __vehicleModel;
        }

        // DELETE: api/VehicleModel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            var vehicleModel = await _context.VehicleModels.FindAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            _context.VehicleModels.Remove(vehicleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleModelExists(int id)
        {
            return _context.VehicleModels.Any(e => e.Id == id);
        }
    }
}
