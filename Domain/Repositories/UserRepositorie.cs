using AdministratioSchool.Domain.Entities;
using AdministratioSchool.Infraestructure.Persistence.Contex;
using Microsoft.EntityFrameworkCore;

namespace AdministratioSchool.Domain.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly AppDbContext _context;
        public UserRepositorie(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(User user)
        {
            var userCreated = user;
            if (userCreated == null)
            {
                throw new KeyNotFoundException("User not founded with Id:" + user.Id);
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return userCreated;
        }

        public async Task<User> DeleteUser(int id)
        {
            var userRemoved =  _context.Users.Find(id);
            if (userRemoved == null)
            {
                throw new KeyNotFoundException("User not founded with Id:" + id);
            }
            _context.Users.Remove(userRemoved);
            await _context.SaveChangesAsync();
            return userRemoved;
        }

        public async Task<User> GetUserById(int id)
        {
            var userID = await _context.Users.FindAsync(id);
            if (userID == null)
            {
                throw new KeyNotFoundException("User not founded with Id:" + id);
            }
            return userID;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var userID =  _context.Users.Find(id);
            if (userID == null)
            {
                throw new KeyNotFoundException("User not founded with Id:" + id);
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return userID;
        }
    }
}
