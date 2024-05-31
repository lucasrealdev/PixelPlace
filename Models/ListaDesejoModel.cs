using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Entities;

namespace ProjetoPixelPlace.Models
{
    public class ListaDesejoModel
    {

        private int idJogo;
        private int idUser;


        private JogoModel jogoM = new JogoModel();

        public ListaDesejoModel()
        {
        }

        public ListaDesejoModel(int idJogo, int idUser)
        {
            this.idJogo = idJogo;
            this.idUser = idUser;
        }
        public MySqlConnection abreConexao()
        {
            MySqlConnection conexao;
            try
            {
                conexao = CriadorConexao.getConexao("ConexaoPadrao");
                conexao.Open();
                return conexao;
            }
            catch (Exception ex)
            {
                conexao = CriadorConexao.getConexao("casa");
                conexao.Open();

            }

            return conexao;
        }

        public List<Jogo> pegarTodosDesejos(int idUser)
        {
            List<Jogo> jogos = new List<Jogo>();

            try
            {
                using (MySqlConnection bd = abreConexao())
                {
                    string query = "SELECT idJogo FROM lista_desejo WHERE idUser = @idUser";
                    using (MySqlCommand cmd = new MySqlCommand(query, bd))
                    {
                        cmd.Parameters.AddWithValue("@idUser", idUser);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idJogo = reader.GetInt32("idJogo");
                                Jogo jogo = jogoM.getJogo(idJogo);
                                if (jogo != null)
                                {
                                    jogos.Add(jogo);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, rethrow it, or return an empty list or error message as needed)
                Console.WriteLine("Erro: " + ex.Message);
            }

            return jogos;
        }

        public string inserirDesejo(int idJogo, int idUser)
        {
            MySqlConnection bd = abreConexao();

            try
            {
               

                string query = "INSERT INTO lista_desejo (idJogo, idUser) VALUES (@idJogo, @idUser)";
                MySqlCommand cmd = new MySqlCommand(query, bd);
                cmd.Parameters.AddWithValue("@idJogo", idJogo);
                cmd.Parameters.AddWithValue("@idUser", idUser);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    return "Desejo inserido com sucesso!";
                }
                else
                {
                    return "Falha ao inserir desejo.";
                }
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
            finally
            {
                if (bd != null && bd.State == System.Data.ConnectionState.Open)
                {
                    bd.Close();
                }
            }
        }

        public int IdJogo { get => idJogo; set => idJogo = value; }
        public int IdUser { get => idUser; set => idUser = value; }
    }
}
