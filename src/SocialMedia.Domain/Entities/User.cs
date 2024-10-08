﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities
{
    public class User:BaseEntity<int>
    {
        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? Password { get; set; }
        public string? Image {  get; set; }
        public int RoleId { get; set; }
       public Roles? Roles { get; set; }
        public bool Is2FAEnabled { get; set; }
        public string? TwoFactorCode { get; set; }
        public DateTime? TwoFactorCodeExpiration { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiration { get; set; }
        public ICollection<Appointments> appointments { get; set; }
    }
}
