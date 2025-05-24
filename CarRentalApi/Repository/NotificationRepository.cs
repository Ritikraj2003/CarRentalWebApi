using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;

namespace CarRentalApi.Repository
{
    public class NotificationRepository:INotification
    {
       
        private readonly AppDbContext dbContext;

        public NotificationRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ServiceResponse<Notification>> AddNotification(Notification notification)
        {
          var response = new ServiceResponse<Notification>();
            try
            {
                dbContext.Notifications.Add(notification);
                dbContext.SaveChanges();
                response.Data = notification;
                response.Success = true;
                response.Message = "Notification added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteNotification(int id)
        {
           var response = new ServiceResponse<bool>();
            try
            {
                var notification = await dbContext.Notifications.FindAsync(id);
                if (notification == null)
                {
                    response.Success = false;
                    response.Message = "Notification not found.";
                }
                else
                {
                    dbContext.Notifications.Remove(notification);
                    await dbContext.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Notification deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Notification>>> GetAllNotification()
        {
            var response = new ServiceResponse<List<Notification>>();
            try
            {
                var notifications = dbContext.Notifications.ToList();
                response.Data = notifications;
                response.Success = true;
                response.Message = "Notifications retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Notification>> GetNotificationById(int id)
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                var notification = dbContext.Notifications.Find(id);
                if (notification == null)
                {
                    response.Success = false;
                    response.Message = "Notification not found.";
                }
                else
                {
                    response.Data = notification;
                    response.Success = true;
                    response.Message = "Notification retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<Notification>> UpdateNotification(int id, Notification notification)
        {
            var response = new ServiceResponse<Notification>();
            try
            {
                var existingNotification = dbContext.Notifications.Find(id);
                if (existingNotification == null)
                {
                    response.Success = false;
                    response.Message = "Notification not found.";
                }
                else
                {
                    existingNotification.Message = notification.Message;
                    dbContext.SaveChanges();
                    response.Data = existingNotification;
                    response.Success = true;
                    response.Message = "Notification updated successfully.";
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
