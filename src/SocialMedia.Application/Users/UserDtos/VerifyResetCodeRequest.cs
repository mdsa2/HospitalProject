using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Users.UserDtos
{
    public class VerifyResetCodeRequest
    {
        public string Email { get; set; }
        public string ResetCode { get; set; }
    }
}
