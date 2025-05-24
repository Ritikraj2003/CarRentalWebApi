using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Repository
{
    public class ContactRepository:IContactRepository
    {
        private readonly AppDbContext dbContext;

        public ContactRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ServiceResponse<Contact>> CreateAsync(Contact contact)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                dbContext.Contacts.Add(contact);
                await dbContext.SaveChangesAsync();

                response.Data = contact;
                response.Success = true;
                response.Message = "Success";
                if (response.Success)
                {
                    Notification notification = new Notification
                    {
                        NotificationFromId = contact.Id,
                        CreatedOn = DateTime.UtcNow,
                        NotificationStatus = false,
                        Message = "New Order Created"

                    };
                    dbContext.Notifications.Add(notification);
                    await dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                response.Success = false;   
                response.Message = ex.Message;  
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var res = await dbContext.Contacts.FindAsync(id);
                if (res != null)
                {
                    dbContext.Remove(res);
                    await dbContext.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Deleted";
                }
                else
                {
                    response.Success = false;
                    response.Message = "dataNot Found";
                }
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<Contact>>> GetAllAsync()
        {
              var response= new ServiceResponse<List<Contact>>();
            try
            {
                var res = await dbContext.Contacts.ToListAsync();

                response.Data= res;
                response.Success = true;
                response.Message = "Sucess";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Contact>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                var res = await dbContext.Contacts.FindAsync(id);
                response.Data=res; 
                response.Success = true;
                response.Message = "Success";
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;    
        }

        public async Task<ServiceResponse<Contact>> UpdateAsync( int id,Contact contact)
        {
            var response = new ServiceResponse<Contact>();
            try
            {
                var existing = await dbContext.Contacts.FindAsync(id);
                if (existing != null)
                {
                    dbContext.Entry(existing).CurrentValues.SetValues(contact);
                    // Save the changes to the database
                    await dbContext.SaveChangesAsync();
                    response.Data = existing;
                    response.Success = true;
                    response.Message = "Updated";
                    
                }
            }
            
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        
    }
}
