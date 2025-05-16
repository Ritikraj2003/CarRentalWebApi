using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repository
{
    public class BookingTypeRepository: IbookingTypeRepository
    {
        private readonly AppDbContext DbContext;

        public BookingTypeRepository(AppDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public async Task<BookingType> AddBookingTypeAsync(BookingType bookingType)
        {
             bookingType.BookingTypeId = Guid.NewGuid();
            DbContext.BookingTypes.Add(bookingType);
            await DbContext.SaveChangesAsync();
            return bookingType;
        }

        public async Task<IEnumerable<BookingType>> GetAllBookingTypeAsync()
        {
             return await  DbContext.BookingTypes.ToListAsync();
        }
    }
}
