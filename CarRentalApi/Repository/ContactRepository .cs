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

        public  async  Task<Contact> CreateAsync(Contact contact)
        {
            // contact.Id = Guid.NewGuid();
            dbContext.Contacts.Add(contact);
            await dbContext.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> DeleteAsync(Guid id)
        {
             var c = await GetByIdAsync(id);
             dbContext.Contacts.Remove(c);
             await dbContext.SaveChangesAsync();
            return c;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await dbContext.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(Guid id)
        {
            return await dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contact> UpdateAsync(Contact contact)
        {
            var existingContact = dbContext.Contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            // Update the properties
            dbContext.Entry(existingContact).CurrentValues.SetValues(contact);

            // Save the changes to the database
            await dbContext.SaveChangesAsync();

            return existingContact;
        }
    }
}
