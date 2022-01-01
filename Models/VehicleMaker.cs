using System;
using System.Collections.Generic;

namespace royal_car_rentals_web_api.Models
{
    public partial class VehicleMaker
    {
        public VehicleMaker()
        {
            VehicleModels = new HashSet<VehicleModel>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<VehicleModel>? VehicleModels { get; set; }
        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
