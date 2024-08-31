using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Users.UserDtos
{
    public class RegisterForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "FullName must be at least 3 characters")]
        public string FullName { get; set; }
        public string Image {  get; set; }
        public int RoleId { get; set; }

       
    }
}
