using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface INotification
    {
        Task<ServiceResponse<Notification>> GetNotificationById(int id);
        Task<ServiceResponse<List<Notification>>> GetAllNotification();
        Task<ServiceResponse<Notification>> AddNotification(Notification notification);
        Task<ServiceResponse<Notification>> UpdateNotification(int id, Notification notification);
        Task<ServiceResponse<bool>> DeleteNotification(int id);

    }
}
