using AutoMapper;
using SocialMedia.Application.Doctors.DoctorsDtos;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Filters;
using SocialMedia.Domain.Repositry;

namespace SocialMedia.Application.Doctors.Service
{
    public interface IDoctorsService
    {
        Task<List<DoctorsDto>> GetAllDoctors(Docotrsfilter filter);
         Task CreateDoctors(DoctorsForm doctorsForm);
        Task<DoctorsDto> GetById (int id);
        Task UpdateDoctors(int Id,UpdateDoctors updateDoctors);
    }
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepositry _doctorsRepositry;
        private readonly IMapper _mapper;
  
        public DoctorsService(IDoctorsRepositry doctorsRepositry, IMapper mapper)
        {
            _doctorsRepositry = doctorsRepositry;
            _mapper = mapper;
           
        }
        public async Task CreateDoctors(DoctorsForm doctorsForm)
        {
            var doctors = _mapper.Map<Doctor>(doctorsForm);
            await _doctorsRepositry.CreateDoctors(doctors);
          
        }

        public async Task<List<DoctorsDto>> GetAllDoctors(Docotrsfilter filter)
        {
            var doctors = await _doctorsRepositry.GetAllDoctors(filter);
            var mapping =  _mapper.Map<List<DoctorsDto>>(doctors);
            return mapping;
        }

        public async Task<DoctorsDto> GetById(int id)
        {
            var doctors = await _doctorsRepositry.GetById(id);


            return _mapper.Map<DoctorsDto>(doctors);
        }

        public async Task UpdateDoctors(int Id,UpdateDoctors updateDoctors)
        {
            var existingDoctors = await _doctorsRepositry.GetById(Id);
            if (existingDoctors == null)
            {
                throw new Exception("the doctor with this Id not Found");

            }
            existingDoctors.Name = updateDoctors.Name;
            existingDoctors.Title = updateDoctors.Title;
            existingDoctors.startDate = updateDoctors.startDate;
            existingDoctors.EndDate = updateDoctors.EndDate;
           await _doctorsRepositry.UpdateDoctors(Id,existingDoctors);
        }
    }
}
