namespace royal_car_rentals_web_api.Models
{
    public class VehicleCounts
    {
        public int TotalVehicles { get; set; }

        public int PendingVehicles { get; set; }

        public int ApprovedVehicles { get; set; }

        public int BlockedVehicles { get; set; }

        public int AvailableVehicles { get; set; }

        public int BookedVehicles { get; set; }
    }
}
