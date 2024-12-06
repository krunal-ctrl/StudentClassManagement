using Microsoft.EntityFrameworkCore;
using StudentClassManagement.Application.Mappers;
using StudentClassManagement.Application.Services;
using StudentClassManagement.Core.Interfaces;
using StudentClassManagement.Infrastructure.Data;
using StudentClassManagement.Infrastructure.Repositories;

namespace StudentClassManagement;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepo<>));
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IClassRepository, ClassRepository>();
        services.AddScoped<IStudentClassRepository, StudentClassRepository>();
        
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IClassService, ClassService>();
        services.AddScoped<StudentClassService, StudentClassService>();

        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}