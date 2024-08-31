using SocialMedia.Domain.Entities;
 
namespace SocialMedia.Domain.Repositry
{
    public interface IRoleRepositry
    {
        Task<Roles> GetByIdAsync(int id);
        Task<List<Roles>> GetAllAsync();
        Task AddAsync(Roles role);
        Task UpdateAsync(Roles role);
        Task DeleteAsync(int id);
    }
}
