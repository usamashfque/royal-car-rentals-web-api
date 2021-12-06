using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? BookingId { get; set; }
        public int? TotalAmount { get; set; }
        public int? PaidAmount { get; set; }
        public int? DiscountAmount { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
