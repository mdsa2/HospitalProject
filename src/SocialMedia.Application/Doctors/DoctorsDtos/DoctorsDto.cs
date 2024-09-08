using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Doctors.DoctorsDtos
{
    public class DoctorsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
