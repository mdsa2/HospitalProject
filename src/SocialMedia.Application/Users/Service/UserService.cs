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
        Task<bool> VerifyTwoFactorCode(string email, string code);
        Task<string> GenerateAndSendTwoFactorCode(string email);
        Task EnableTwoFactorAuthentication(string Email);
        Task<string> GenerateAndSendPasswordResetCode(string email);
        Task<bool> VerifyPasswordResetCode(string email, string resetCode);
        Task ResetPassword(string email, string resetCode, string newPassword);
    }
    public class UserService(IUserRepositry _userRepositry, IMapper mapper, ITokenService _tokenService,IRoleRepositry _roleRepositry,IEmailService _emailService) : IUserService
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
       
            var getUser = await _userRepositry.GetUserByEmail(login.Email);

            if (getUser == null)
            {
                throw new Exception("The email was not found.");
            }

            try
            {
             
                if (!BCrypt.Net.BCrypt.Verify(login.Password, getUser.Password))
                {
                    throw new Exception("Incorrect password.");
                }
            }
            catch (BCrypt.Net.SaltParseException ex)
            {
                 
                throw new Exception("Error processing the password. Please contact support.");
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred. Please contact support.");
            }

     
            var role = await _roleRepositry.GetByIdAsync(getUser.RoleId);

      
            var usermap = mapper.Map<UserDto>(getUser);

                                                                      
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

        public async Task<string> GenerateAndSendTwoFactorCode(string email)
        {
            var user = await _userRepositry.GetUserByEmail(email);
            if (user == null || !user.Is2FAEnabled)
            {
                throw new Exception("User not found or 2FA is not enabled.");
            }

            var code = GenerateTwoFactorCode(); 
            user.TwoFactorCode = code;
            user.TwoFactorCodeExpiration = DateTime.UtcNow.AddMinutes(5);  

            await _userRepositry.UpdateUser(user);

        
            await _emailService.SendEmailAsync(user.Email, "Your 2FA Code", $"Your 2FA code is {code}");

            return code;
        }

        public async Task<bool> VerifyTwoFactorCode(string email, string code)
        {
            var user = await _userRepositry.GetUserByEmail(email);
            if (user == null || user.TwoFactorCode != code || user.TwoFactorCodeExpiration < DateTime.UtcNow)
            {
                return false;
            }

          
            user.TwoFactorCode = null;  
            await _userRepositry.UpdateUser(user);
            return true;
        }

        private string GenerateTwoFactorCode()
        {
         
            return new Random().Next(100000, 999999).ToString();
        }
        public async Task EnableTwoFactorAuthentication(string Email)
        {
            var user = await _userRepositry.GetUserByEmail(Email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.Is2FAEnabled = true;
            await _userRepositry.UpdateUser(user);
        }
       
        public async Task<string> GenerateAndSendPasswordResetCode(string email)
        {
            var user = await _userRepositry.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Generate a 6-digit reset code
            var resetCode = new Random().Next(100000, 999999).ToString();

           
            user.PasswordResetToken = resetCode;
            user.PasswordResetTokenExpiration = DateTime.UtcNow.AddMinutes(10); // Reset code valid for 10 minutes
            await _userRepositry.UpdateUser(user);

          
            await _emailService.SendEmailAsync(user.Email, "Password Reset Code", $"Your password reset code is {resetCode}");

            return resetCode;
        }
        public async Task<bool> VerifyPasswordResetCode(string email, string resetCode)
        {
            var user = await _userRepositry.GetUserByEmail(email);
            if (user == null || user.PasswordResetToken != resetCode || user.PasswordResetTokenExpiration < DateTime.UtcNow)
            {
                return false; 
            }

            return true;
        }
        public async Task ResetPassword(string email, string resetCode, string newPassword)
        {
            var user = await _userRepositry.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

           
            if (user.PasswordResetToken != resetCode || user.PasswordResetTokenExpiration < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired reset code.");
            }

      
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

           
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiration = null;

            await _userRepositry.UpdateUser(user);
        }
    }

}
