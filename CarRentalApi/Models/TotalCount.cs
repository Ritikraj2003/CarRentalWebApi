namespace CarRentalApi.Models
{
    public class TotalCount
    {
        public int Total_Car { get; set; }
        public int Total_Booking { get; set; }
        public int Total_Driver { get; set; }
        public int Total_bookingtype { get; set; }
        public int Total_contacts { get; set; }
        public Bookingfor Bookingfor { get; set; } 
    }

    public class Bookingfor
    {
        public int company { get; set; }
        public int Indiviusal { get; set; }
    }
}
