using Teste_Taking_Jsl.CrossCutting.IoC;

namespace BRBSolucoes.Viagens.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentException(null, nameof(services));
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
