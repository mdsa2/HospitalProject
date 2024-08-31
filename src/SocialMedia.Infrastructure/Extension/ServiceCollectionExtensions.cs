
 
using AutoMapper.Configuration;
 
 
 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Common;
using SocialMedia.Application.FileService;
using SocialMedia.Application.Role;
using SocialMedia.Application.Users.Service;
using SocialMedia.Application.Users.UserDtos;
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
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(UserProfile));

        }
    }
}
