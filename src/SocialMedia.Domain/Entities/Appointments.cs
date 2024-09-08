using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities
{
    public class Appointments:BaseEntity<int>
    {
          public string? UserName { get; set; }
        public string? Title { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Description { get; set; }
  
        public Status status { get; set; }= Status.pending;
        public string? CreatedBy { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
        public int UserId { get; set; }
        public User user { get; set; }
        public int DoctoreId { get; set; }
        public Doctor doctors { get; set; }

    }

}
