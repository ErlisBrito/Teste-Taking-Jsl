using Microsoft.Extensions.DependencyInjection;
using Teste_Taking_Jsl.Application.Interfaces;
using Teste_Taking_Jsl.Application.Services;
using Teste_Taking_Jsl.Data.Contexts;
using Teste_Taking_Jsl.Data.Repository;
using Teste_Taking_Jsl.Domain.Interfaces;

namespace Teste_Taking_Jsl.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            RegisterServicesApplication(services);
            RegisterServicesInfraData(services);
        }

        /// <summary>
        /// Application 
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterServicesApplication(IServiceCollection services)
        {
            services.AddScoped<IClienteAppService, ClienteAppService>();
        }

        /// <summary>
        /// Infra - Data 
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterServicesInfraData(IServiceCollection services)
        {
            services.AddDbContext<TakingJslContext>(ServiceLifetime.Transient);
            services.AddScoped<IClienteRepository, ClienteRepository>();
        }
    }
}