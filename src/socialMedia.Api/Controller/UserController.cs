using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Users.Service;
using SocialMedia.Application.Users.UserDtos;

namespace SocialMedia.APi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) :ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<UserDto> LoginUser([FromBody] LoginForm loginForm)
        {
            var userlogin = await userService.Login(loginForm);
            return userlogin;
        }
        [HttpGet]
        [Route("GetUserById")]
        public async Task<UserDto> GetUserById ( int Id)
        {
            var userlogin = await userService.GetUserById(Id);
            return userlogin;
        }
        [HttpPut]
        [Route("Update")]
        public async Task<UserDto> UpdateUser( int Id, [FromBody] UpdateUserDto updateUserDto)
        {
            var users = await userService.GetUserById(Id);
            return users;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<UserDto> RegisterUser([FromBody]RegisterForm registerForm)
        {
            var users = await userService.Register(registerForm);
            return users;
        }
        [HttpPost("enable-2fa")]
        public async Task<IActionResult> EnableTwoFactor([FromBody] string Email)
        {
            try
            {
                await userService.EnableTwoFactorAuthentication(Email);
                return Ok(new { Message = "Two-Factor Authentication enabled." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost("request-2fa-code")]
        public async Task<IActionResult> RequestTwoFactorCode([FromBody] RequestTwoFactorDto requestDto)
        {
            try
            {
                var code = await userService.GenerateAndSendTwoFactorCode(requestDto.Email);
                return Ok(new { Message = "2FA code sent to your email." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("verify-2fa-code")]
        public async Task<IActionResult> VerifyTwoFactorCode([FromBody] VerifyTwoFactorDto verifyDto)
        {
            try
            {
                var isValid = await userService.VerifyTwoFactorCode(verifyDto.Email, verifyDto.Code);
                if (isValid)
                {
                    return Ok();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost("request-password-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] string email)
        {
            await userService.GenerateAndSendPasswordResetCode(email);
            return Ok(new { message = "Password reset code sent." });
        }

        [HttpPost("verify-reset-code")]
        public async Task<IActionResult> VerifyResetCode([FromBody] VerifyResetCodeRequest request)
        {
            var isValid = await userService.VerifyPasswordResetCode(request.Email, request.ResetCode);
            if (!isValid)
            {
                return BadRequest(new { message = "Invalid or expired reset code." });
            }
            return Ok(new { message = "Reset code is valid." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequests request)
        {
            await userService.ResetPassword(request.Email, request.ResetCode, request.NewPassword);
            return Ok(new { message = "Password has been reset." });
        }
    }
}

