using MySql.Data.MySqlClient;

namespace ProjetoPixelPlace.Models
{
    public class CriadorConexao
    {
        public CriadorConexao()
        {
        }

        public static MySqlConnection getConexao(string nomeConexao)
        {
            
            MySqlConnection conexao = new MySqlConnection(Configuration().GetConnectionString(nomeConexao));

            return conexao;
        }

        private static IConfigurationRoot Configuration()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder().SetBasePath(
                    Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }
    }
}
