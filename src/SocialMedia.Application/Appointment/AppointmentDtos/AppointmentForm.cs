using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Appointment.AppointmentDtos
{
    public class AppointmentForm
    {
        public string? UserName { get; set; }
        public string? Title { get; set; }
          public int UserId { get; set; }
        public string? Description { get; set; }
 
    }
}
