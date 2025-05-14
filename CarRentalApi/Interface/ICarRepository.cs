using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> AddCarAsync(Car car, IFormFile imageFile);
        Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
