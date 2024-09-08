using Invoice.Domain.Util;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domain.Repositry
{
    public interface IAppointmentRepositry
    {
        Task<PaginatedList<Appointments>> GetAllAppointments(AppointmentFilter filter);
        Task UpdateAppointment(int Id ,Appointments appointment);
        Task<Appointments> GetById(int Id);
        Task CreateAppointment(Appointments appointment);
    }
}
