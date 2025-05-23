using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IbookingTypeRepository
    {
        Task<IEnumerable<BookingType>> GetAllBookingTypeAsync();
        Task<BookingType> AddBookingTypeAsync(BookingType bookingType );

        Task<BookingType> GetByBookingTypeId(int id);
        Task<BookingType> DeleteByBookingTypeId(int id);
        Task<BookingType> UpdateBookingTypeAsync(BookingType bookingType);
    }
}
