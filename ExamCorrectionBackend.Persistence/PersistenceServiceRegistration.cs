using System;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Domain.Entities;
using ExamCorrectionBackend.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExamCorrectionBackend.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ExamCorrectionContext>(options =>
                options.UseSqlServer("Server=mssql;Database=ExamCorrection;User ID=SA;Password=myPassword?;"));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IExamTaskRepository, ExamTaskRepository>();
            services.AddScoped<IStudentSolutionRepository, StudentSolutionRepository>();
            services.AddScoped<IDatasetRepository, DatasetRepository>();
            return services;
        }
    }
}
