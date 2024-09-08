using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Filters
{
    public class AppointmentFilter:MasterFilter
    {
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public DateTime? AppointmentDate { get; set; }
        

        public Status? status { get; set; } = Status.pending;
    }
}
