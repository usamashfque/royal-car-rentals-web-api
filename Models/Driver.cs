﻿using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int CityId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public bool? Availability { get; set; }
        public string? LicenceNo { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual City? City { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
