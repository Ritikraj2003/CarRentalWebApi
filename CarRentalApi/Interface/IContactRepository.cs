using CarRentalApi.Models;

namespace CarRentalApi.Interface
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(Guid id);
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact> DeleteAsync(Guid id);
        Task<Contact> UpdateAsync(Contact contact);
    }
}
