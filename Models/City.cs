using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class City
    {
        public City()
        {
            Bookings = new HashSet<Booking>();
            Drivers = new HashSet<Driver>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string? CityName { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }
        public virtual ICollection<Driver>? Drivers { get; set; }
        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
