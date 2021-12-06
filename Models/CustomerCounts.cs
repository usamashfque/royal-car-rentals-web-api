namespace royal_car_rentals_web_api.Models
{
    public class CustomerCounts
    {
        public int TotalCustomers { get; set; }

        public int PendingCustomers { get; set; }

        public int ApprovedCustomers { get; set; }

        public int BlockedCustomers { get; set; }

        public int ActiveCustomers { get; set; }

        public int InActiveCustomers { get; set; }

        public int MaleCustomers { get; set; }

        public int FemaleCustomers { get; set; }
    }
}
