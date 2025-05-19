using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApi.Models
{
    public class Car
    {
        public Guid? CarId { get; set; } 
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
