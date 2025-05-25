using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRentalApi.Models
{
    public class ConfirmBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int DriverId { get; set; }
        public DateTime? ConfirmedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Confirmed";

        public Booking? Booking { get; set; }
        public Driver? Driver { get; set; }
    }
}
