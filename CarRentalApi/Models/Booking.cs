namespace CarRentalApi.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; } = Guid.NewGuid();

        public string cartype { get; set; }
        public string BookingType { get; set; }
        public  int Phone_no { get; set; }
        public string Name {  get; set; }
        public string Email {  get; set; }
        public string PickupLocation {  get; set; }
        public DateOnly PickupDate { get; set; }
        public DateTime PickupTime { get; set; }
        public string DropLocation { get; set; }
        public DateOnly Dropdate { get; set; }
        public DateTime Droptime { get; set; }
        public DateOnly BookingDate { get; set; }
    }
}
