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
    public class BookingController : ControllerBase
    {
        private readonly RoyalCarRentalsDBContext _context;

        public BookingController(RoyalCarRentalsDBContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            //return await _context.Bookings.ToListAsync();

            var result = await (from _booking in _context.Bookings
                                join customer in _context.Customers on _booking.CustomerId equals customer.Id
                                select new Booking()
                                {
                                    Id = _booking.Id,
                                    CustomerId = _booking.CustomerId,
                                    DriverId = _booking.DriverId,
                                    VehicleId = _booking.VehicleId,
                                    Status = _booking.Status,
                                    WithDriver = _booking.WithDriver,
                                    StartDate = _booking.StartDate,
                                    StartTime = _booking.StartTime,
                                    EndDate = _booking.EndDate,
                                    EndTime = _booking.EndTime,
                                    DateAdded = _booking.DateAdded,
                                    DateUpdated = _booking.DateUpdated,
                                    Customer = customer,
                                    Vehicle = null,
                                    Driver = null
                                }).ToListAsync();


            List<Booking> __result = new List<Booking>();

            foreach (var item in result)
            {

                var vehicle = (from _vehicle in _context.Vehicles
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
                               }).Where(a => a.Id == item.VehicleId).FirstOrDefault();

                item.Vehicle = vehicle;
                __result.Add(item);
            }

            return __result;
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }


            var result = (from _booking in _context.Bookings
                          join customer in _context.Customers on _booking.CustomerId equals customer.Id
                          join vehicle in _context.Vehicles on _booking.VehicleId equals vehicle.Id
                          join driver in _context.Drivers on _booking.DriverId equals driver.Id

                          select new Booking()
                          {
                              Id = _booking.Id,
                              CustomerId = _booking.CustomerId,
                              DriverId = _booking.DriverId,
                              VehicleId = _booking.VehicleId,
                              Status = _booking.Status,
                              WithDriver = _booking.WithDriver,
                              StartDate = _booking.StartDate,
                              StartTime = _booking.StartTime,
                              EndDate = _booking.EndDate,
                              EndTime = _booking.EndTime,
                              DateAdded = _booking.DateAdded,
                              DateUpdated = _booking.DateUpdated,
                              Customer = customer,
                              Vehicle = vehicle,
                              Driver = driver
                          }).Where(a => a.Id == id).FirstOrDefault();


            return result;
        }

        // PUT: api/Booking/5      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            booking.DateUpdated = DateTime.Now;
            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Booking
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            booking.StartDate = booking.StartDate.Value.ToLocalTime();
            booking.EndDate = booking.EndDate.Value.ToLocalTime();
            booking.DateAdded = DateTime.Now;
            booking.DateUpdated = DateTime.Now;

            booking.Customer = null;
            booking.Driver = null;
            booking.Vehicle = null;


            _context.Bookings.Add(booking);

            var vehicleOld = await _context.Vehicles.FindAsync(booking.VehicleId);
            vehicleOld.Availability = false;
            vehicleOld.DateUpdated = DateTime.Now;

            _context.Entry(vehicleOld).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }

        // GET: api/Booking/allcounts
        [HttpGet("allcounts")]
        public async Task<ActionResult<BookingCounts>> GetBookingCounts()
        {
            BookingCounts bookingCounts = new BookingCounts();

            bookingCounts.TotalBookings = await _context.Bookings.CountAsync();
            bookingCounts.PendingBookings = await _context.Bookings.Where(a => a.Status == "pending").CountAsync();
            bookingCounts.ScheduledBookings = await _context.Bookings.Where(a => a.Status == "approved").CountAsync();
            bookingCounts.CompletedBookings = await _context.Bookings.Where(a => a.Status == "completed").CountAsync();
            bookingCounts.CalcelledBookings = await _context.Bookings.Where(a => a.Status == "calcelled").CountAsync();

            return bookingCounts;
        }

        // GET: api/Booking/customerbookings/5
        [HttpGet("customerbookings/{id}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetCustomerBookings(int id)
        {
            var __customer = await _context.Customers.FindAsync(id);

            if (__customer == null)
            {
                return NotFound();
            }

            var result = await (from _booking in _context.Bookings
                                join customer in _context.Customers on _booking.CustomerId equals customer.Id
                                select new Booking()
                                {
                                    Id = _booking.Id,
                                    CustomerId = _booking.CustomerId,
                                    DriverId = _booking.DriverId,
                                    VehicleId = _booking.VehicleId,
                                    Status = _booking.Status,
                                    WithDriver = _booking.WithDriver,
                                    StartDate = _booking.StartDate,
                                    StartTime = _booking.StartTime,
                                    EndDate = _booking.EndDate,
                                    EndTime = _booking.EndTime,
                                    DateAdded = _booking.DateAdded,
                                    DateUpdated = _booking.DateUpdated,
                                    Customer = customer,
                                    Vehicle = null
                                }).Where(a => a.CustomerId == id).ToListAsync();


            List<Booking> __result = new List<Booking>();

            foreach (var item in result)
            {

                var vehicle = (from _vehicle in _context.Vehicles
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
                               }).Where(a => a.Id == item.VehicleId).FirstOrDefault();

                item.Vehicle = vehicle;
                __result.Add(item);
            }

            return __result;

            //var result = await (from _booking in _context.Bookings
            //              join customer in _context.Customers on _booking.CustomerId equals customer.Id
            //              join vehicle in _context.Vehicles on _booking.VehicleId equals vehicle.Id
            //              join driver in _context.Drivers on _booking.DriverId equals driver.Id
            //              select new Booking()
            //              {
            //                  Id = _booking.Id,
            //                  CustomerId = _booking.CustomerId,
            //                  DriverId = _booking.DriverId,
            //                  VehicleId = _booking.VehicleId,
            //                  Status = _booking.Status,
            //                  WithDriver = _booking.WithDriver,
            //                  StartDate = _booking.StartDate,
            //                  StartTime = _booking.StartTime,
            //                  EndDate = _booking.EndDate,
            //                  EndTime = _booking.EndTime,
            //                  DateAdded = _booking.DateAdded,
            //                  DateUpdated = _booking.DateUpdated,
            //                  Customer = customer,
            //                  Vehicle = vehicle,
            //                  Driver = driver
            //              }).Where(a => a.CustomerId == id).ToListAsync();

            //return result;
        }

        // GET: api/Booking/customercounts/5
        [HttpGet("customercounts/{id}")]
        public async Task<ActionResult<BookingCounts>> GetCustomerBookingCounts(int id)
        {
            BookingCounts bookingCounts = new BookingCounts();

            bookingCounts.TotalBookings = await _context.Bookings.CountAsync();
            bookingCounts.PendingBookings = await _context.Bookings.Where(a => a.CustomerId == id && a.Status == "pending").CountAsync();
            bookingCounts.ScheduledBookings = await _context.Bookings.Where(a => a.CustomerId == id && a.Status == "approved").CountAsync();
            bookingCounts.CompletedBookings = await _context.Bookings.Where(a => a.CustomerId == id && a.Status == "completed").CountAsync();
            bookingCounts.CalcelledBookings = await _context.Bookings.Where(a => a.CustomerId == id && a.Status == "calcelled").CountAsync();

            return bookingCounts;
        }
    }
}
