using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IConfirmBookingRepository
    {
        Task<ServiceResponse<List<ConfirmBooking>>> GetAllAsync();
        Task<ServiceResponse<ConfirmBooking>> GetByIdAsync(int id);
        Task<ServiceResponse<ConfirmBooking>> CreateAsync(ConfirmBooking confirmBooking);
        Task<ServiceResponse<ConfirmBooking>> UpdateAsync(int id, ConfirmBooking updated);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
