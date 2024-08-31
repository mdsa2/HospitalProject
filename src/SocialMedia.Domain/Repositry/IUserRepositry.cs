using SocialMedia.Domain.Entities;

namespace SocialMedia.Domain.Repositry
{
    public interface IUserRepositry
    {
      public  Task<User> GetUserById(int Id);
      public  Task<User> GetUserByEmail(string Email);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<User> DeleteUser(User user);
    
    }
}
