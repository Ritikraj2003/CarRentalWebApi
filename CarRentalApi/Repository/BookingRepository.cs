using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            if (booking == null)
            {
                throw new InvalidOperationException("Update failed: Booking is null");
            }

            // Find the existing booking in the database
            var existingBooking = await dbContext.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == booking.BookingId);

            if (existingBooking == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            // Update the properties
            dbContext.Entry(existingBooking).CurrentValues.SetValues(booking);

            // Save the changes to the database
            await dbContext.SaveChangesAsync();

            return existingBooking;
        }

    }
}
