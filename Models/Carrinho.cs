using MySql.Data.MySqlClient;

namespace ProjetoPixelPlace.Models
{
    public class Carrinho
    {
        private int idUser;
        private int idJogo;
        

        public Carrinho()
        {
        }

        public Carrinho(int idUser, int idJogo)
        {
            this.idUser = idUser;
            this.idJogo = idJogo;
        }

        public int IdUser { get => idUser; set => idUser = value; }
        public int IdJogo { get => idJogo; set => idJogo = value; }
       


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


        public string inserirCarrinho()
        {
            MySqlConnection mySqlConnection = abreConexao();

            MySqlCommand comando = new MySqlCommand("Insert into carrinho(Usuario_idUsuario, Jogo_idJogo) values(@Usuario_idUsuario, @Jogo_idJogo)", mySqlConnection);
            comando.Parameters.AddWithValue("@Usuario_idUsuario", IdUser);
            comando.Parameters.AddWithValue("@Jogo_idJogo", IdJogo);

            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return $"Não foi possivel inserir no carrinho {ex.Message}";
            }

            return "Inserido no carrinho com sucesso.";
        }

        public List<Carrinho> CarrinhoUser()
        {
            List<Carrinho> carrinhos = new List<Carrinho>();

            MySqlConnection mySqlConnection = abreConexao();

            MySqlCommand comando = new MySqlCommand("Select * from carrinho where Usuario_idUsuario = @id ", mySqlConnection);
            comando.Parameters.AddWithValue("@id", IdUser);

            MySqlDataReader comandoDataReader = comando.ExecuteReader();

            while (comandoDataReader.Read())
            {
                IdUser = comandoDataReader.GetInt32("Usuario_idUsuario");
                IdJogo = comandoDataReader.GetInt32("Jogo_idJogo");

                carrinhos.Add(new Carrinho(IdUser, IdJogo));
            }

            return carrinhos;
        }
        public string RetirarJogoCarrinho(int id, int idUser)
        {
            MySqlConnection mySqlConnection = abreConexao();

            MySqlCommand comando = new MySqlCommand("Delete from carrinho where Jogo_idJogo = @id AND Usuario_idUsuario = @iduser ", mySqlConnection);
            comando.Parameters.AddWithValue("@id", id);
            
            comando.Parameters.AddWithValue("@iduser", idUser);


            try
            {
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                return $"Não foi possivel retirar do carrinho {ex.Message}";
            }

            return "Retirado do carrinho com sucesso";
        }
    }
}
