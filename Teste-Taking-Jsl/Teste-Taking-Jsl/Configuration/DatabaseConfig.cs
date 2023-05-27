using Microsoft.EntityFrameworkCore;
using Teste_Taking_Jsl.Data.Contexts;
using Teste_Taking_Jsl.Helpers;

namespace BRBSolucoes.Viagens.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<TakingJslContext>(options =>
            {
                options.UseMySql(ConnectionHelper.GetConnection(configuration), ServerVersion.AutoDetect(ConnectionHelper.GetConnection(configuration)));
                options.EnableSensitiveDataLogging();
            });
        }
    }
}