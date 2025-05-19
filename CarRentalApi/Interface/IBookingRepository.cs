using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(Booking booking);
        Task<IEnumerable<Booking>> GetAllBookingAsync();
        Task<Booking> GetByIdAsync( Guid id);
        Task<Booking>DeleteByIdAsyncAsync(Guid id);

    }
}
