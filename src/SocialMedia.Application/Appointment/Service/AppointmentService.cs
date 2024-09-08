using AutoMapper;
using SocialMedia.Application.Appointment.AppointmentDtos;
using SocialMedia.Application.Common;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Filters;
using SocialMedia.Domain.Repositry;

namespace SocialMedia.Application.Appointment.Service
{
    public interface  IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllApointment(AppointmentFilter filter);
        Task CreateAppointment(AppointmentForm appointmentDto);
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task UpdateAppointmentAsync(int id, AppointmentUpdate appointmentDto);

    }
    public class AppointmentService:IAppointmentService
    {
        private readonly IAppointmentRepositry _appointmentRepositry;
        private readonly IMapper _mapper;
       private readonly INotificationService _notificationService;
        public AppointmentService(IAppointmentRepositry appointmentRepositry, IMapper mapper,INotificationService notificationService)
        {
            _appointmentRepositry = appointmentRepositry;
            _mapper = mapper;
          _notificationService = notificationService;
        }
        public async Task<List<AppointmentDto>> GetAllApointment(AppointmentFilter filter)
        {
            var getAppointment = await _appointmentRepositry.GetAllAppointments(filter);


            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(getAppointment);

            return appointmentDtos;
        }
            public async Task CreateAppointment(AppointmentForm appointmentDto)
        {
             var appointment = _mapper.Map<Appointments>(appointmentDto);

          
         await _appointmentRepositry.CreateAppointment(appointment);
           
        }
        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
          
            var appointment = await _appointmentRepositry.GetById(id);

        
            return _mapper.Map<AppointmentDto>(appointment);
        }
       public async Task UpdateAppointmentAsync(int id, AppointmentUpdate appointmentDto)
        {
            var existingAppointment = await _appointmentRepositry.GetById(id);

            if (existingAppointment != null)
            {
                var previousStatus = existingAppointment.status;

                _mapper.Map(appointmentDto, existingAppointment);

                await _appointmentRepositry.UpdateAppointment(id, existingAppointment);

                if (existingAppointment.status != previousStatus)
                {
                    string message = existingAppointment.status switch
                    {
                        Status.Approved => $"Your appointment has been approved on {existingAppointment.AppointmentDate}.",
                        Status.Rejected => "Your appointment has been Rejected.",
                        Status.Updated => $"Your appointment has been updated on {existingAppointment.AppointmentDate}.",
                        Status.Completed => $"Your appointment has been completed on {existingAppointment.AppointmentDate}.",
                        _ => "Your appointment status has been updated."
                    };

                    await _notificationService.SendNotificationAsync(existingAppointment.Id.ToString(), message);
                }
            }
        }

    }
}
