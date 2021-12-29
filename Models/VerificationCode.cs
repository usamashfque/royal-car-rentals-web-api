using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class VerificationCode
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? Code { get; set; }
        public int? Count { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
