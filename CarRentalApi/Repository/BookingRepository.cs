using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repository
{
    public class BookingRepository: IBookingRepository
    {
        private readonly AppDbContext dbContext;

        public BookingRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            booking.BookingId = Guid.NewGuid();
            dbContext.Bookings.Add(booking);
            await dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking> DeleteByIdAsyncAsync(Guid id)
        {
            var c= await dbContext.Bookings.FindAsync(id);

            dbContext.Bookings.Remove(c);
            await dbContext.SaveChangesAsync();
            return c;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingAsync()
        {
            return await dbContext.Bookings.ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(Guid id)
        {
            return await dbContext.Bookings.FirstOrDefaultAsync(c => c.BookingId == id);
        }
    }
}
