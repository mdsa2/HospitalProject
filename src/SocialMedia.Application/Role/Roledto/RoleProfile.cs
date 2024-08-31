using AutoMapper;
using SocialMedia.Application.Role.RoleDto;
using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Role.Roledto
{
    public class RoleProfile:Profile
    {
        public RoleProfile() {
            CreateMap<Roles, RoleDtos>().ReverseMap();
            CreateMap<Roles, RoleResponse>();
        
        }
    }
}
