// File: ServiceConfiguration.cs

using BLL.Interfaces;
using BLL.Services;
using DAL.interfaces;
using DAL.ORM;
using Domain.interfaces;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Register services with the dependency injection container
        services.AddScoped<IStudentActions, StudentAction>();
        services.AddScoped<IStudentService, StudentService>();

        services.AddScoped<ITeacherActions, TeacherAction>();
        services.AddScoped<ITeacherService, TeacherService>();

        services.AddScoped<IUserActions, UserAction>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ICourseActions, CourseAction>();
        services.AddScoped<ICourseService, CourseService>();

        services.AddScoped<IStudentEnrollmentActions, StudentEnrollmentAction>();
        services.AddScoped<IStudentEnrollmentService, StudentEnrollmentService>();
    }
}
