using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class VehicleModel
    {
        public VehicleModel()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public int MakerId { get; set; }
        public string? DisplayName { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual VehicleMaker? Maker { get; set; } = null!;
        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
