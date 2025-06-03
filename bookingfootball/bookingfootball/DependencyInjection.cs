
using bookingfootball;
using bookingfootball.Constract;
using bookingfootball.Db_QL;
using bookingfootball.Interfaces;
using bookingfootball.Persistence.Repositories;
using bookingfootball.Persistence.Repository;
using bookingfootball.Service;
using Microsoft.AspNetCore.Identity;

namespace bookingfootball
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher<TaiKhoan>, PasswordHasher<TaiKhoan>>();

            return services;
        }
    }
}
