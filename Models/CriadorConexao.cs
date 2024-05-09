using MySql.Data.MySqlClient;

namespace ProjetoPixelPlace.Models
{
    public class CriadorConexao
    {
       

        //metodo que cria pega a conexao a partir do nome dado
        public static MySqlConnection getConexao(string nomeConexao)
        {
            
            MySqlConnection conexao = new MySqlConnection(Configuration().GetConnectionString(nomeConexao));

            return conexao;
        }

        //metodo que pega do appsetings
        private static IConfigurationRoot Configuration()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder().SetBasePath(
                    Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }
    }
}
