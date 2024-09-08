using Invoice.Domain.Util;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Filters;
using SocialMedia.Domain.Repositry;
using SocialMedia.Infrstrucutre.SocialDbContext;

namespace SocialMedia.Infrastructure.Repositry
{
    public class DoctorsRepositry(DataDbContext dataDbContext) : IDoctorsRepositry
    {
        public async Task CreateDoctors(Doctor doctors)
        {
           dataDbContext.docotors.Add(doctors);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<Doctor>> GetAllDoctors(Docotrsfilter filter)
        {
           var query = dataDbContext.docotors.AsQueryable();
            return await PaginatedList<Doctor>.CreateAsync(query, filter.PageNumber, filter.PageSize);
        }

        public async Task<Doctor> GetById(int Id)
        {
            return await dataDbContext.docotors.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task UpdateDoctors(int Id, Doctor doctors)
        {
            dataDbContext.docotors.FindAsync(Id);

            await dataDbContext.SaveChangesAsync();
        }
    }
}
