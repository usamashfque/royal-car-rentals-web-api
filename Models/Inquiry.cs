using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class Inquiry
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
