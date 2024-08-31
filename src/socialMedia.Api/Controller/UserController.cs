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
    }
}
