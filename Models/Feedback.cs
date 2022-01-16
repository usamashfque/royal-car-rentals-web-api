using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int? BookingId { get; set; }
        public decimal? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
