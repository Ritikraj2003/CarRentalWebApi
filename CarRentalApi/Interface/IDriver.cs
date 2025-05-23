using CarRentalApi.Models;


namespace CarRentalApi.Interface
{
    public interface IDriverRepository
    {
        Task<ServiceResponse<Driver>> GetDriverByIdAsync(int id);
        Task<ServiceResponse<List<Driver>>> GetAllDriversAsync();
        Task<ServiceResponse<Driver>> AddDriverAsync(Driver driver);
        Task<ServiceResponse<Driver>> UpdateDriverAsync(int id,Driver driver);
        Task<ServiceResponse<bool>> DeleteDriverAsync(int id);
    }
}
