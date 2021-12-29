using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int MakerId { get; set; }
        public int ModelId { get; set; }
        public int CityId { get; set; }
        public long? ModelYear { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Color { get; set; }
        public string? Status { get; set; }
        public bool? Availability { get; set; }
        public long? Price { get; set; }
        public string? ImagesPath { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual VehicleMaker Maker { get; set; } = null!;
        public virtual VehicleModel Model { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
