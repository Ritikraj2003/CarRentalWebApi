using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IBookingRepository
    {
        Task<ServiceResponse<Booking>> GetByIdAsync(int id);
        Task<ServiceResponse<List<Booking>>> GetAllBookingAsync();
        Task<ServiceResponse<Booking>> AddBookingAsync(Booking booking);
        Task<ServiceResponse<Booking>> UpdateBookingAsync( Booking booking);
        Task<ServiceResponse<bool>> DeleteByIdAsyncAsync(int id);
    }
}
