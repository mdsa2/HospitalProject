using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Appointment.AppointmentDtos
{
    public class AppointmentUpdate
    {
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Description { get; set; }

        public Status status { get; set; } = Status.pending;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
