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
    public class CustomerController : ControllerBase
    {
        private readonly RoyalCarRentalsDBContext _context;

        public CustomerController(RoyalCarRentalsDBContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> PutCustomer(int id, [FromForm] FileUpload formdata)
        {
            var oldCustomer = await _context.Customers.FindAsync(id);

            var customer = JsonConvert.DeserializeObject<Customer>(formdata.Info);


            oldCustomer.FirstName = customer.FirstName;
            oldCustomer.LastName = customer.LastName;
            oldCustomer.PhoneNumber = customer.PhoneNumber;
            oldCustomer.Address = customer.Address;
            oldCustomer.LicenceNo = customer.LicenceNo;
            oldCustomer.Gender = customer.Gender;
            oldCustomer.VerificationStatus = customer.VerificationStatus;
            oldCustomer.IsActive = customer.IsActive;
            oldCustomer.DateUpdated = DateTime.Now;


            if (id != customer.Id)
            {
                return BadRequest();
            }

            if (formdata.Files != null)
            {
                try
                {
                    string path = Path.Combine("D:\\UNI\\FYP\\royal-car-rentals-website\\src\\assets\\adminUploads", "customers");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var file = formdata.Files[0];

                    if (file.Length > 0)
                    {
                        string fileName = oldCustomer.FirstName.Replace(" ", "") + oldCustomer.LastName.Replace(" ", "") + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "" + Path.GetExtension(file.FileName);
                        string fullPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        oldCustomer.ProfilePicture = fileName;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            _context.Entry(oldCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            oldCustomer.Password = null;

            return oldCustomer;

            // return NoContent();
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromForm] FileUpload formdata)
        {
            var customer = JsonConvert.DeserializeObject<Customer>(formdata.Info);

            customer.DateAdded = DateTime.Now;
            customer.DateUpdated = DateTime.Now;
            customer.ProfilePicture = "";

            if (formdata.Files != null)
            {
                try
                {
                    string path = Path.Combine("D:\\UNI\\FYP\\royal-car-rentals-website\\src\\assets\\adminUploads", "customers");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var file = formdata.Files[0];

                    if (file.Length > 0)
                    {
                        string fileName = customer.FirstName.Replace(" ", "") + customer.LastName.Replace(" ", "") + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "" + Path.GetExtension(file.FileName);
                        string fullPath = Path.Combine(path, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        customer.ProfilePicture = fileName;
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // POST: api/Customer/signup
        [HttpPost("signup")]
        public async Task<ActionResult<Customer>> SignUpCustomer([FromBody] Customer customer)
        {
            customer.DateAdded = DateTime.Now;
            customer.DateUpdated = DateTime.Now;
            customer.ProfilePicture = "";

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            customer.Password = null;

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // POST: api/Customer/signin
        [HttpPost("signin")]
        public async Task<ActionResult<Customer>> SignInCustomer([FromBody] Customer customer)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(a => a.Email == customer.Email && a.Password == customer.Password);

            if (result == null)
            {
                return NotFound();
            }
            //else if ((bool)!result.IsActive)
            //{
            //    return NotFound();
            //}

            result.Password = null;

            return result;
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        // GET: api/Customer/counts
        [HttpGet("counts")]
        public async Task<ActionResult<CustomerCounts>> GetCounts()
        {
            CustomerCounts customerCount = new CustomerCounts();

            customerCount.TotalCustomers = await _context.Customers.CountAsync();
            customerCount.PendingCustomers = await _context.Customers.Where(a => a.VerificationStatus == "pending").CountAsync();
            customerCount.ApprovedCustomers = await _context.Customers.Where(a => a.VerificationStatus == "approved").CountAsync();
            customerCount.BlockedCustomers = await _context.Customers.Where(a => a.VerificationStatus == "blocked").CountAsync();
            customerCount.ActiveCustomers = await _context.Customers.Where(a => a.IsActive == true).CountAsync();
            customerCount.InActiveCustomers = await _context.Customers.Where(a => a.IsActive == false).CountAsync();
            customerCount.MaleCustomers = await _context.Customers.Where(a => a.Gender == "male").CountAsync();
            customerCount.FemaleCustomers = await _context.Customers.Where(a => a.Gender == "female").CountAsync();

            return customerCount;
        }

        // PUT: api/Customer/5
        [HttpPut("changepassword/{id}")]
        public async Task<ActionResult<Customer>> ChangeCustomerPassword(int id, [FromBody] ChangePassword param)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return BadRequest();
            }

            if (customer.Password != param.OldPassword)
            {
                return NotFound("mismatch");
            }

            customer.Password = param.NewPassword;
            customer.DateUpdated = DateTime.Now;

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            customer.Password = null;

            return customer;
        }
    }
}
