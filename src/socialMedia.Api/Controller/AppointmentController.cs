using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Appointment.AppointmentDtos;
using SocialMedia.Application.Appointment.Service;

namespace SocialMedia.APi.Controller
{
    
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpPost]
      
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentForm appointmentDto)
        {
         

           
             await  _appointmentService.CreateAppointment(appointmentDto);
                return  Ok(appointmentDto);
            
        
        }
    }
}
