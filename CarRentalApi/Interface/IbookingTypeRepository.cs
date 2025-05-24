using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IbookingTypeRepository
    {
        Task<ServiceResponse<BookingType>> GetByBookingTypeId(int id);
        Task<ServiceResponse<List<BookingType>>> GetAllBookingTypeAsync();
        Task<ServiceResponse<BookingType>> AddBookingTypeAsync(BookingType bookingType);
        Task<ServiceResponse<BookingType>> UpdateBookingTypeAsync(int id,BookingType bookingType);
        Task<ServiceResponse<bool>> DeleteByBookingTypeId(int id);
    }
}
