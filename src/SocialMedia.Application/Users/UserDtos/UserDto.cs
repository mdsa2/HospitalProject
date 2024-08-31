using SocialMedia.Application.Role.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Users.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    
        public string? Image { get; set; }
        public string? Token { get; set; }
        public RoleDtos Role { get; set; }
    }
}
