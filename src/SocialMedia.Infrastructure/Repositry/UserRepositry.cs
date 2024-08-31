using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositry;
using SocialMedia.Infrstrucutre.SocialDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrstrucutre.Repositry
{
    public class UserRepositry : IUserRepositry
    {
        private readonly DataDbContext _db;
        public UserRepositry(DataDbContext db)
        {
            _db = db;
        }

        public   async Task<User>  CreateUser(User user)
        {
            _db.Users.Add(user);
         await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
        }
        public async Task<User> GetUserById(int Id)
        {
          return await _db.Users.FindAsync(Id);
          
        }

        public async  Task<User> UpdateUser(User user)
        {
            _db.Users.Update(user);
            await _db   .SaveChangesAsync();
            return user;
        }
    }
}
