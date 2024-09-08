using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Config
{
    public class AppointmentConfigure:IEntityTypeConfiguration<Appointments>
    {
        public void Configure(EntityTypeBuilder<Appointments> builder)
        {

            builder.Property(x=>x.Id).IsRequired();
            builder.Property(x=>x.UserName).IsRequired();
            builder.Property(x=>x.Title).IsRequired();
            builder.Property(x=>x.Description).IsRequired();
            builder.HasOne(x => x.user)
                .WithMany(r => r.appointments)
                
                .HasForeignKey(x => x.UserId);
        }
    }
}
