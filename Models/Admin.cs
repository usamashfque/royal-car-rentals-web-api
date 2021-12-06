using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
