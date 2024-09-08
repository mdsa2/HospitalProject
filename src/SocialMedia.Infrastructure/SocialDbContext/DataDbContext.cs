using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrstrucutre.SocialDbContext
{
    public class DataDbContext : DbContext
    {
  public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
    {
    }
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> GetRoles { get; set; }
        public DbSet<Appointments> appointments { get; set; }
        public DbSet<Doctor> docotors { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity<int>>())
            {
                
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.Updated = DateTime.UtcNow.AddHours(3);
                }
               
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            

            base.OnModelCreating(modelBuilder);
        }
    }
  

}