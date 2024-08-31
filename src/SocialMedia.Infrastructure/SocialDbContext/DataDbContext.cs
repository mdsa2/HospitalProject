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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            

            base.OnModelCreating(modelBuilder);
        }
    }
  

}