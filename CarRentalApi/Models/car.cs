namespace CarRentalApi.Models
{
    public class Car
    {
        public Guid CarId { get; set; } = Guid.NewGuid();
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
    }
}
