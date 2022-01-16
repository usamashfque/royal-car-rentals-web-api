using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class Booking
    {
        public Booking()
        {
            BookingLogs = new HashSet<BookingLog>();
            Feedbacks = new HashSet<Feedback>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public int? CustomerId { get; set; }
        public int? DriverId { get; set; }
        public int? CityId { get; set; }
        public bool? WithDriver { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public int? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EndTime { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual City? City { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Driver? Driver { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        public virtual ICollection<BookingLog> BookingLogs { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
