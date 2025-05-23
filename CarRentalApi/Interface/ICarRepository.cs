using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface ICarRepository
    {
        Task<ServiceResponse<Car>> GetByCarId(int id);
        Task<ServiceResponse<List<Car>>> GetAllCarsAsync();
        Task<ServiceResponse<Car>> AddCarAsync(Car car);
        Task<ServiceResponse<Car>> UpdateCarAsync(Car car);
        Task<ServiceResponse<bool>> DeleteByCarId(int id);
    }
}
