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

        public async Task<ServiceResponse<BookingType>> AddBookingTypeAsync(BookingType bookingType)
        {
            var response = new ServiceResponse<BookingType>();
            try
            {
                DbContext.BookingTypes.Add(bookingType);
                await DbContext.SaveChangesAsync();
                response.Data = bookingType;
                response.Success = true;
                response.Message = "Successfully updated";
            }
            catch (Exception ex) 
                {
                response.Success = false;   
                response.Message = ex.Message;
                }
            return response;    

        }

        public async Task<ServiceResponse<bool>> DeleteByBookingTypeId(int id)
        {
             var response = new ServiceResponse<bool>();
            try
            {
                var bookingtype = await DbContext.BookingTypes.SingleOrDefaultAsync(x => x.BookingTypeId == id);
                if (bookingtype != null)
                {
                    DbContext.Remove(bookingtype);
                    await DbContext.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "successfully Deleted";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public  async Task<ServiceResponse<List<BookingType>>> GetAllBookingTypeAsync()
        {
            var response = new ServiceResponse<List<BookingType>>();
            try
            {
                var res =  await DbContext.BookingTypes.ToListAsync();
                response.Data = res;
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex) 
            { 
            response.Success= false;
            response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<BookingType>> GetByBookingTypeId(int id)
        {
            var response = new ServiceResponse<BookingType>();
            try
            {
                var res = await DbContext.BookingTypes.FirstOrDefaultAsync(x => x.BookingTypeId == id);
                if (res != null)
                {
                    response.Data = res;
                    response.Success = true;
                    response.Message = "Sucess";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Booking type not found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }


        public async Task<ServiceResponse<BookingType>> UpdateBookingTypeAsync( int id,BookingType bookingType)
        {
            var response = new ServiceResponse<BookingType>();
            try
            {
                var res = await DbContext.BookingTypes.FirstOrDefaultAsync(x => x.BookingTypeId == id);
                if (res == null)
                {
                    response.Success = false;
                    response.Message = "Booking not found.";
                    return response;
                }

                res.type=bookingType.type;
                await DbContext.SaveChangesAsync();
                response.Data = res;
                response.Success=true; 
                response.Message="Updated";
            }
            catch (Exception ex)
            {
                response.Success=false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
