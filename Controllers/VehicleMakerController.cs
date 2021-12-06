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
    public class VehicleMakerController : ControllerBase
    {
        private readonly RoyalCarRentalsDBContext _context;

        public VehicleMakerController(RoyalCarRentalsDBContext context)
        {
            _context = context;
        }

        // GET: api/VehicleMaker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMaker>>> GetVehicleMakers()
        {
            return await _context.VehicleMakers.ToListAsync();
        }

        // GET: api/VehicleMaker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMaker>> GetVehicleMaker(int id)
        {
            var vehicleMaker = await _context.VehicleMakers.FindAsync(id);

            if (vehicleMaker == null)
            {
                return NotFound();
            }

            return vehicleMaker;
        }

        // PUT: api/VehicleMaker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMaker(int id, VehicleMaker vehicleMaker)
        {
            if (id != vehicleMaker.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleMaker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleMakerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VehicleMaker
        [HttpPost]
        public async Task<ActionResult<VehicleMaker>> PostVehicleMaker(VehicleMaker vehicleMaker)
        {
            _context.VehicleMakers.Add(vehicleMaker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleMaker", new { id = vehicleMaker.Id }, vehicleMaker);
        }

        // DELETE: api/VehicleMaker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMaker(int id)
        {
            var vehicleMaker = await _context.VehicleMakers.FindAsync(id);
            if (vehicleMaker == null)
            {
                return NotFound();
            }

            _context.VehicleMakers.Remove(vehicleMaker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleMakerExists(int id)
        {
            return _context.VehicleMakers.Any(e => e.Id == id);
        }
    }
}
