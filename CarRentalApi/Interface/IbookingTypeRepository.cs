using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IbookingTypeRepository
    {
        Task<IEnumerable<BookingType>> GetAllBookingTypeAsync();
        Task<BookingType> AddBookingTypeAsync(BookingType bookingType );
    }
}
