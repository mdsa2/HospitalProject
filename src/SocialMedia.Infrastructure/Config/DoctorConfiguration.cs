using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Config
{
    public class DoctorConfiguration:IEntityTypeConfiguration<Doctor>
    {
  

        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x=>x.Name).IsRequired();
            builder.Property(x=>x.Title).IsRequired();
            builder.HasMany(x => x.Appointments)
             .WithOne(r => r.doctors)

             .HasForeignKey(x => x.UserId);
        }
    }
}
