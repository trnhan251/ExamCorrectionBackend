using System;
using Microsoft.Extensions.DependencyInjection;

namespace ExamCorrectionBackend.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
