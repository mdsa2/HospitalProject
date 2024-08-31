using Microsoft.EntityFrameworkCore;
 
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositry;
using SocialMedia.Infrstrucutre.SocialDbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositry
{
    public class RoleRepositry(DataDbContext dbContext):IRoleRepositry
    {
        public async Task<Roles> GetByIdAsync(int id)
        {
            return await dbContext.GetRoles.FindAsync(id);
        }

        public async Task<List<Roles>> GetAllAsync()
        {
            return await dbContext.GetRoles.ToListAsync();
        }

        public async Task AddAsync(Roles role)
        {
            dbContext.GetRoles.Add(role);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Roles role)
        {
            dbContext.GetRoles.Update(role);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await dbContext.GetRoles.FindAsync(id);
            if (role != null)
            {
                dbContext.GetRoles.Remove(role);
                await dbContext.SaveChangesAsync();
            }
        }

     
    }
}
