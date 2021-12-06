namespace royal_car_rentals_web_api.Models
{
    public class BookingCounts
    {
        public int TotalBookings { get; set; }

        public int PendingBookings { get; set; }

        public int ScheduledBookings { get; set; }

        public int CompletedBookings { get; set; }

        public int CalcelledBookings { get; set; }
    }
}
