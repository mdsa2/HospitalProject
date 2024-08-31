using AutoMapper;
using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Users.UserDtos
{
    public class UserProfile:Profile
    {
        public UserProfile() {
        CreateMap<User, UserDto>();
        CreateMap<LoginForm, User>();
        CreateMap<UpdateUserDto, User>();
        
        }    
       
    }
}
