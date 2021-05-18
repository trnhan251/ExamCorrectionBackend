using System;
using Azure.Storage.Blobs;
using ExamCorrectionBackend.Application.Contracts.Infrastructure;
using ExamCorrectionBackend.Infrastructure.Blob;
using Microsoft.Extensions.DependencyInjection;

namespace ExamCorrectionBackend.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton(x => new BlobServiceClient("Name=ConnectionStrings.BlobStorage"));
            services.AddScoped<IBlobService, BlobService>();
            return services;
        }
    }
}
