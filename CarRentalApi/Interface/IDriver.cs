using CarRentalApi.Models;


namespace CarRentalApi.Interface
{
    public interface IDriverRepository
    {
        Task<ServiceResponse<Driver>> GetDriverByIdAsync(Guid id);
        Task<ServiceResponse<List<Driver>>> GetAllDriversAsync();
        Task<ServiceResponse<Driver>> AddDriverAsync(Driver driver);
        Task<ServiceResponse<Driver>> UpdateDriverAsync(Driver driver);
        Task<ServiceResponse<bool>> DeleteDriverAsync(Guid id);
    }
}
