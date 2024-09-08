using AutoMapper;
using SocialMedia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.Appointment.AppointmentDtos
{
    public class AppointmentProfile:Profile
    {
        public AppointmentProfile() { 
        CreateMap<Appointments,AppointmentDto>().ReverseMap();
        CreateMap<AppointmentForm,Appointments>();
        CreateMap<AppointmentUpdate,Appointments>();
        
        
        }
    }
}
