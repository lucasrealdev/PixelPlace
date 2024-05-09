using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Entities;

namespace ProjetoPixelPlace.Models
{
    public class JogoModel
    {
        private MySqlConnection con;
        public List<Jogo> getAllJogos()
        {
            List<Jogo> jogoList = new List<Jogo>(); 
            try
            {
                con = CriadorConexao.getConexao("ConexaoPadrao");
                con.Open();
            }
            catch (Exception ex) {
                con = CriadorConexao.getConexao("casa");
                con.Open();
            }

            MySqlCommand query = new MySqlCommand("Select * from jogo", con);
            MySqlDataReader reader = query.ExecuteReader();

            while(reader.Read()) {
                Jogo jogo = new Jogo(int.Parse(reader["idJogo"].ToString()),
                    reader["nome"].ToString(),
                    (byte[])reader["imagemCapa"],
                    reader["descricao"].ToString(),
                    reader["categoria"].ToString(),
                    Double.Parse(reader["preco"].ToString()),
                    Double.Parse(reader["desconto"].ToString()),
                    DateTime.Parse(reader["data"].ToString()));
                 
                jogoList.Add(jogo);
                
            }
            con.Close();
            return jogoList;
        }
        public string inserirJogo(Jogo jogo)
        {
            string mensagem = "";
           
            
            try
            {
                using (MySqlConnection con = CriadorConexao.getConexao("ConexaoPadrao"))
                {
                    con.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO Jogo(nome, imagemCapa, descricao, categoria,preco,desconto,data) VALUES (@nome, @imagemCapa, @descricao,@categoria,@preco,@desconto,@data)", con))
                    {
                        mySqlCommand.Parameters.AddWithValue("@nome", jogo.Nome);



                        mySqlCommand.Parameters.AddWithValue("@imagemCapa", jogo.ImagemCapa);
                        mySqlCommand.Parameters.AddWithValue("@descricao", jogo.Descricao);
                        mySqlCommand.Parameters.AddWithValue("@categoria", jogo.Categoria);
                        mySqlCommand.Parameters.AddWithValue("@preco", jogo.Preco);
                        mySqlCommand.Parameters.AddWithValue("@desconto", jogo.Desconto);
                        //validacao para enviar a data certa para o BD;
                        mySqlCommand.Parameters.AddWithValue("@data", jogo.Data.ToString("yyyy-MM-dd HH:mm"));

                       
                        int rowsAffected = mySqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            mensagem = "Jogo cadastrado com sucesso";
                        }
                        else
                        {
                            mensagem = "Falha ao cadastrar jogo";
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                mensagem = "Ocorreu um erro ao cadastrar o jogo: " + ex.Message;
            }
            
            return mensagem;

        }
    }
}
