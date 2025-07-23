using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApi.Models
{
    public class Car : GeneralData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public carBookingdata? CarBookingDetails { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public bool IsDeleted { get; set; } = false;
    }


    public class carBookingdata
    {
        public string? MinKm { get; set; }
        public string? RatePerKm { get; set; }
        public string? Rate { get; set; }
        public string? extraKm { get; set; }
    }
}
