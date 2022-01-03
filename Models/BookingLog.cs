using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class BookingLog
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string? Action { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual Booking? Booking { get; set; } = null!;
    }
}
