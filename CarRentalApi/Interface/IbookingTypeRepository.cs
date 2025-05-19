using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IbookingTypeRepository
    {
        Task<IEnumerable<BookingType>> GetAllBookingTypeAsync();
        Task<BookingType> AddBookingTypeAsync(BookingType bookingType );

        Task<BookingType> GetByBookingTypeId(Guid id);
        Task<BookingType> DeleteByBookingTypeId(Guid id);
    }
}
