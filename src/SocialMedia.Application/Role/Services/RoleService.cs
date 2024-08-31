
using AutoMapper;
using SocialMedia.Application.Role.Roledto;
using SocialMedia.Application.Role.RoleDto;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepositry _roleRepositry;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepositry roleRepositry, IMapper mapper)
        {
            _roleRepositry = roleRepositry;
            _mapper = mapper;   
        }
        public Task<Roles> GetByIdAsync(int id)
        {
            return _roleRepositry.GetByIdAsync(id);
        }
        public async Task<List<RoleResponse>> GetAllAsync()
        {
            var rolesDto = await _roleRepositry.GetAllAsync(); 
            var rolesResponse = _mapper.Map<List<RoleResponse>>(rolesDto);  
            return rolesResponse;
        }

        public Task AddAsync(RoleDtos role)
        {
            var roles = new Roles
            {
                Name = role.Name,



            };
            return _roleRepositry.AddAsync(roles);
        }

        public Task UpdateAsync(Roles role)
        {
            return _roleRepositry.UpdateAsync(role);
        }

        public Task DeleteAsync(int id)
        {
            return _roleRepositry.DeleteAsync(id);
        }

    }
}

