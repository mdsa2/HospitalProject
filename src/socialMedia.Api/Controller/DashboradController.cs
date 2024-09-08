using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Appointment.AppointmentDtos;
using SocialMedia.Application.Appointment.Service;
using SocialMedia.Domain.Filters;

namespace SocialMedia.APi.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboradController  : ControllerBase
    {

        private readonly IAppointmentService _appointmentService;
        public DashboradController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("GetALlApointment")]
/*        [Authorize(Roles = "Admin")]  
*/        public async Task<IActionResult> GetAllAppointments([FromQuery] AppointmentFilter filter)
        {
                var appointments = await _appointmentService.GetAllApointment(filter);
                return Ok(appointments);
              
        }

         [HttpGet("{id}")]
      /*  [Authorize(Roles = "Admin")]  */
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {id} not found.");
                }
                return Ok(appointment);
            }
          
      
        
        

 
        [HttpPut("{id}")]
     /*   [Authorize(Roles = "Admin")] */
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentUpdate appointmentDto)
        {
            
 
                var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                {
                    return NotFound($"Appointment with ID {id} not found.");
                }

                await _appointmentService.UpdateAppointmentAsync(id, appointmentDto);
            return Ok(existingAppointment); 
            
         
             
                
            }
        }
    }


