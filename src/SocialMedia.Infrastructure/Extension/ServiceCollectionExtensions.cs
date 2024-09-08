
 
using AutoMapper.Configuration;
 
 
 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SocialMedia.Application.Appointment.AppointmentDtos;
using SocialMedia.Application.Appointment.Service;
using SocialMedia.Application.Common;
using SocialMedia.Application.FileService;
using SocialMedia.Application.Role;
using SocialMedia.Application.Role.Roledto;
using SocialMedia.Application.Users.Service;
using SocialMedia.Application.Users.UserDtos;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositry;
using SocialMedia.Infrastructure.Repositry;
using SocialMedia.Infrstrucutre.Repositry;
using SocialMedia.Infrstrucutre.SocialDbContext;


namespace SocialMedia.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionstring);
            services.AddDbContext<DataDbContext>(options => options.UseNpgsql(connectionstring));
          

            services.AddScoped<IUserRepositry, UserRepositry>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleRepositry, RoleRepositry>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IfileService, fileservice>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddScoped<IAppointmentRepositry, AppointmentRepositry>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddSignalR();
            services.AddTransient<IEmailService, EmailService>();
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(AppointmentProfile));
            services.AddAutoMapper(typeof(RoleProfile));
            
        }
    }
}
