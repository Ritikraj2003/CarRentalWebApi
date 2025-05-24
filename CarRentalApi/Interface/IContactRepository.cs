using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IContactRepository
    {
       
        Task<ServiceResponse<Contact>> GetByIdAsync(int id);
        Task<ServiceResponse<List<Contact>>> GetAllAsync();
        Task<ServiceResponse<Contact>> CreateAsync(Contact contact);
        Task<ServiceResponse<Contact>> UpdateAsync( int id,Contact contact);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
