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
    public interface IDoctorsRepositry
    {
        Task<PaginatedList<Doctor>> GetAllDoctors(Docotrsfilter filter);
        Task UpdateDoctors(int Id, Doctor doctors);
        Task<Doctor> GetById(int Id);
        Task CreateDoctors(Doctor doctors);
    }
}
