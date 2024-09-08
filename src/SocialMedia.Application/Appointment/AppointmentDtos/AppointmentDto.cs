using SocialMedia.Domain.Entities;

namespace SocialMedia.Application.Appointment.AppointmentDtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Description { get; set; }

        public Status status { get; set; } = Status.pending;
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
