using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Entities
{
    public class BaseEntity<TId>
    {
        public TId Id { get; set; }


        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
