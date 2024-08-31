
using SocialMedia.Application.Role.Roledto;
using SocialMedia.Application.Role.RoleDto;
 
using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Role
{
    public interface IRoleService
    {
        Task<Roles> GetByIdAsync(int id);
        Task<List<RoleResponse>> GetAllAsync();
        Task AddAsync(RoleDtos role);
        Task UpdateAsync(Roles role);
        Task DeleteAsync(int id);
    }
}
