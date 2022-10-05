using SampleAPI.Application.Abstractions.Services;
using SampleAPI.Application.Abstractions.Storage;
using SampleAPI.Application.Abstractions.Token;
using SampleAPI.Infrastructure.Enums;
using SampleAPI.Infrastructure.Services;
using SampleAPI.Infrastructure.Services.Storage;
using SampleAPI.Infrastructure.Services.Storage.Azure;
using SampleAPI.Infrastructure.Services.Storage.Local;
using SampleAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace SampleAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
