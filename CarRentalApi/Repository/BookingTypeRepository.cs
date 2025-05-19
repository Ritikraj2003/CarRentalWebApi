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

        public async Task<BookingType> DeleteByBookingTypeId(Guid id)
        {
            var c =  await GetByBookingTypeId(id);

            DbContext.BookingTypes.Remove(c);
            await DbContext.SaveChangesAsync();
            return c;

        }

        public async Task<IEnumerable<BookingType>> GetAllBookingTypeAsync()
        {
             return await  DbContext.BookingTypes.ToListAsync();
        }

        public async Task<BookingType> GetByBookingTypeId(Guid id)
        {
             return await DbContext.BookingTypes.FirstOrDefaultAsync(c => c.BookingTypeId == id);
        }
        public async Task<BookingType> UpdateBookingTypeAsync(BookingType bookingType)
        {
            var existingBooking = await GetByBookingTypeId(bookingType.BookingTypeId);
            // Find the existing booking in the database
           
            if (existingBooking == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            // Update the properties
            DbContext.Entry(existingBooking).CurrentValues.SetValues(bookingType);

            // Save the changes to the database
            await DbContext.SaveChangesAsync();

            return existingBooking;
        }
    }
}
