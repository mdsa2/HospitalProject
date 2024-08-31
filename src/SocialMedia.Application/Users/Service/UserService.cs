using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Metadata;
using SocialMedia.Application.Common;
using SocialMedia.Application.Users.UserDtos;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositry;
 
 
namespace SocialMedia.Application.Users.Service
{
    public interface IUserService { 
    public Task<UserDto> Login(LoginForm login);
        Task<UserDto> Register(RegisterForm registerForm);
        Task<User> Delete (int id);
        Task<User> UpdateUser(int Id, UpdateUserDto updateUserDto);
        Task<UserDto> GetUserById (int id );
    }
    public class UserService(IUserRepositry _userRepositry, IMapper mapper, ITokenService _tokenService,IRoleRepositry _roleRepositry) : IUserService
    {
        public Task<User> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var users = await _userRepositry.GetUserById(id);
            if (users == null)
            {
                throw new Exception("User Id not found");
            }
            var userMap = mapper.Map<UserDto>(users);
            return userMap;
        }

        public async Task<UserDto> Login(LoginForm login)
        {
            // Retrieve the user by email
            var getUser = await _userRepositry.GetUserByEmail(login.Email);

            if (getUser == null)
            {
                throw new Exception("The email was not found.");
            }

            try
            {
                // Verify the password
                if (!BCrypt.Net.BCrypt.Verify(login.Password, getUser.Password))
                {
                    throw new Exception("Incorrect password.");
                }
            }
            catch (BCrypt.Net.SaltParseException ex)
            {
                // Log exception details for debugging
                // Example: LogException(ex);
                throw new Exception("Error processing the password. Please contact support.");
            }
            catch (Exception ex)
            {
                // Log other exceptions as needed
                // Example: LogException(ex);
                throw new Exception("An unexpected error occurred. Please contact support.");
            }

            // Retrieve the user's role
            var role = await _roleRepositry.GetByIdAsync(getUser.RoleId);

            // Map the user to DTO
            var usermap = mapper.Map<UserDto>(getUser);

            // Create a token
            usermap.Token = _tokenService.CreateToken(usermap, role);

            return usermap;
        }

        public async Task<UserDto> Register(RegisterForm registerForm)
        {
            var existingUser = await _userRepositry.GetUserByEmail(registerForm.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }

            var newUser = new User
            {
                Email = registerForm.Email,
                FullName = registerForm.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(registerForm.Password),
                Image = registerForm.Image,
                RoleId = registerForm.RoleId,
            };

            var addedUser = await _userRepositry.CreateUser(newUser);
 
            var role = await _roleRepositry.GetByIdAsync(newUser.RoleId);  

            var userMap = mapper.Map<UserDto>(addedUser);

          
            userMap.Token = _tokenService.CreateToken(userMap, role);

            return userMap;
        }
        public async Task<User> UpdateUser(int Id,UpdateUserDto updateUserDto)
        {
           var user = await _userRepositry.GetUserById(Id);
            if (user == null)
            {
                throw new Exception("User not Found");
            }
            if (user.Email != updateUserDto.Email)
            {
                user.Email = updateUserDto.Email;
            }
            if (user.FullName != updateUserDto.FullName)
            {
                user.FullName = updateUserDto.FullName;
            }
            if (!string.IsNullOrEmpty(updateUserDto.password))
            {
                user.Password = updateUserDto.password; 
            }

            if (user.Image != updateUserDto.Iamge)
            {
                user.Image = updateUserDto.Iamge;
            }
            await _userRepositry.UpdateUser(user);
            return user;

        }
    }
}
