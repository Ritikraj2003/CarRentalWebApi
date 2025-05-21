using CarRentalApi.DbContext;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.EntityFrameworkCore;


namespace CarRentalApi.Repository
{
    public class AuthenticationRepository : IAuthentication

    {
        private readonly AppDbContext dbContext;

        public AuthenticationRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> Add(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }



        public async Task UpdateUser(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
