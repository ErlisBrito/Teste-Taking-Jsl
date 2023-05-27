namespace Teste_Taking_Jsl.Helpers
{
    public class ConnectionHelper
    {
        public static string GetConnection(IConfiguration configuration)
        {
            return configuration.GetConnectionString("DataBaseTakingJsl");
        }
    }
}
