using Invoice.Domain.Util;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Filters;
using SocialMedia.Domain.Repositry;
using SocialMedia.Infrstrucutre.SocialDbContext;
using System.Net.WebSockets;

namespace SocialMedia.Infrastructure.Repositry
{
    public class AppointmentRepositry(DataDbContext dataDbContext) : IAppointmentRepositry
    {

        public async Task CreateAppointment(Appointments appointment)
        {
          var create = dataDbContext.appointments.AddAsync(appointment);
            await dataDbContext.SaveChangesAsync();
           
        }

        public async Task<PaginatedList<Appointments>> GetAllAppointments(AppointmentFilter filter)
        {     
            var query = dataDbContext.appointments.AsQueryable();
            return await PaginatedList<Appointments>.CreateAsync(query, filter.PageNumber,filter.PageSize);
        }

        public async Task<Appointments> GetById(int Id)
        {
            return await dataDbContext.appointments.FirstOrDefaultAsync(x => x.Id == Id);
             
        }

        public async Task UpdateAppointment(int Id, Appointments appointment)
        {
         
            dataDbContext.appointments.FindAsync(Id);
            
            await dataDbContext.SaveChangesAsync();
        }
    }
}
