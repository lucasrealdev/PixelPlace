using MySql.Data.MySqlClient;

namespace ProjetoPixelPlace.Models
{
    public class ListaDesejoModel
    {

        private int idJogo;
        private int idUser;

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
