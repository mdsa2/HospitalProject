using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Doctors.DoctorsDtos;
using SocialMedia.Application.Doctors.Service;
using SocialMedia.Domain.Filters;

namespace SocialMedia.APi.Controller
{

    public class DoctorsControllers : ControllerBase
    {
        private readonly IDoctorsService _doctorsService;
        public DoctorsControllers(IDoctorsService doctorsService)
        {
            _doctorsService = doctorsService;
        }
        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetlAllDoctors([FromQuery] Docotrsfilter filte)
        {
            var result = await _doctorsService.GetAllDoctors(filte);
            return Ok(result);
        }
        [HttpPost("CreateDoctors")]
        public async Task<IActionResult> CreateDoctors([FromBody] DoctorsForm doctorsForm)
        {
            await _doctorsService.CreateDoctors(doctorsForm);
            return Ok(doctorsForm);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id )
        {
            var result = await _doctorsService.GetById(Id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateDoctor(int Id ,[FromBody]UpdateDoctors updateDoctors)
        {
            var existingUpdate = await _doctorsService.GetById(Id);
            if (existingUpdate == null)
            {
                throw new Exception($"the existing Id Docctors not found {Id}");

            }
            await _doctorsService.UpdateDoctors(Id, updateDoctors);
            return Ok(existingUpdate);
        }
    }
}
