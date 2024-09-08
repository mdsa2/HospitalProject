namespace SocialMedia.Domain.Entities
{
    public class Doctor:BaseEntity<int>
    {
      
        public string? Name { get; set; }
        public string? Title { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}
