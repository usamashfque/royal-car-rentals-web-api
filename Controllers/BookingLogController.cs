#nullable disable
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
    public class BookingLogController : ControllerBase
    {
        private readonly RoyalCarRentalsDBContext _context;

        public BookingLogController(RoyalCarRentalsDBContext context)
        {
            _context = context;
        }

        // GET: api/BookingLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingLog>>> GetBookingLogs()
        {
            return await _context.BookingLogs.ToListAsync();
        }

        // GET: api/BookingLog/bybookingid/5
        [HttpGet("bybookingid/{id}")]
        public async Task<ActionResult<IEnumerable<BookingLog>>> GetLogsByBookingId(int id)
        {
            return await _context.BookingLogs.Where(l => l.BookingId == id).ToListAsync();
        }


        // GET: api/BookingLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingLog>> GetBookingLog(int id)
        {
            var bookingLog = await _context.BookingLogs.FindAsync(id);

            if (bookingLog == null)
            {
                return NotFound();
            }

            return bookingLog;
        }

        // PUT: api/BookingLog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingLog(int id, BookingLog bookingLog)
        {
            if (id != bookingLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookingLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingLogExists(id))
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

        // POST: api/BookingLog       
        [HttpPost]
        public async Task<ActionResult<BookingLog>> PostBookingLog(BookingLog bookingLog)
        {
            bookingLog.DateAdded = DateTime.Now;
            bookingLog.DateUpdated = DateTime.Now;

            _context.BookingLogs.Add(bookingLog);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingLog", new { id = bookingLog.Id }, bookingLog);
        }

        // DELETE: api/BookingLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingLog(int id)
        {
            var bookingLog = await _context.BookingLogs.FindAsync(id);
            if (bookingLog == null)
            {
                return NotFound();
            }

            _context.BookingLogs.Remove(bookingLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingLogExists(int id)
        {
            return _context.BookingLogs.Any(e => e.Id == id);
        }
    }
}
