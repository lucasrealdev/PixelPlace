using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Entities;

namespace ProjetoPixelPlace.Models
{
    public class JogoModel
    {
        
        private MySqlConnection conexaoBD;

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
            conexao.Close();
            return conexao;
        }

        public Jogo getJogo(int id)
        { 
            Jogo jogo = null;
            conexaoBD = abreConexao();

            MySqlCommand query = new MySqlCommand("Select * from jogo where idJogo = @id", conexaoBD);
            query.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                jogo = new Jogo(
                    idJogo: Convert.ToInt32(reader["idJogo"]),
                    nome: reader["nome"].ToString(),
                    imagem: (byte[])reader["imagem"],
                    descricao: reader["descricao"].ToString(),
                    categoria: reader["categoria"].ToString(),
                    preco: Convert.ToDouble(reader["preco"]),
                    desconto: Convert.ToDouble(reader["desconto"]),
                    data_lancamento: Convert.ToDateTime(reader["data_lancamento"]),
                    numero_avaliacao: Convert.ToInt32(reader["num_avaliacao"]),
                    numero_estrelas: Convert.ToInt32(reader["num_estrelas"]),
                    desenvolvedora: reader["desenvolvedora"].ToString()
                );
            }

            reader.Close();
            conexaoBD.Close();

            return jogo;

        }
        public List<Jogo> getAllJogos()
        {
            List<Jogo> jogoList = new List<Jogo>();
            byte[] imagem = null;
            conexaoBD = abreConexao();

            MySqlCommand query = new MySqlCommand("Select * from jogo", conexaoBD);
            MySqlDataReader reader = query.ExecuteReader();
            

            while(reader.Read()) {

                if (!reader.IsDBNull(reader.GetOrdinal("imagem"))){
                    imagem = (byte[])reader["imagem"];
                }
                Jogo jogo = new Jogo(int.Parse(reader["idJogo"].ToString()),
                    reader["nome"].ToString(),
                    imagem,
                    reader["descricao"].ToString(),
                    reader["categoria"].ToString(),
                    Double.Parse(reader["preco"].ToString()),
                    Double.Parse(reader["desconto"].ToString()),
                    DateTime.Parse(reader["data_lancamento"].ToString()),
                    int.Parse(reader["num_avaliacao"].ToString()),
                    int.Parse(reader["num_estrelas"].ToString()),
                    reader["desenvolvedora"].ToString());
                 
                jogoList.Add(jogo);
                
            }
            conexaoBD.Close();
            return jogoList;
        }
        public string inserirJogo(Jogo jogo) 
        {
            string mensagem;

            conexaoBD = abreConexao();
            MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO Jogo(nome, imagem, descricao, categoria,preco,desconto,data_lancamento,num_avaliacao, num_estrelas,desenvolvedora) VALUES (@nome, @imagem, @descricao, @categoria,@preco,@desconto,@data_lancamento,@num_avaliacao, @num_estrelas, @desenvolvedora)", conexaoBD);        
            mySqlCommand.Parameters.AddWithValue("@nome", jogo.Nome);
            mySqlCommand.Parameters.AddWithValue("@imagem", jogo.Imagem);
            mySqlCommand.Parameters.AddWithValue("@descricao", jogo.Descricao);
            mySqlCommand.Parameters.AddWithValue("@categoria", jogo.Categoria);
            mySqlCommand.Parameters.AddWithValue("@preco", jogo.Preco);
            mySqlCommand.Parameters.AddWithValue("@desconto", jogo.Desconto);
            //validacao para enviar a data certa para o BD;
            mySqlCommand.Parameters.AddWithValue("@data_lancamento", jogo.Data_lancamento.ToString("yyyy-MM-dd HH:mm"));
            mySqlCommand.Parameters.AddWithValue("@num_avaliacao", jogo.Numero_avaliacao);
            mySqlCommand.Parameters.AddWithValue("@num_estrelas", jogo.Numero_estrelas);
            mySqlCommand.Parameters.AddWithValue("@desenvolvedora", jogo.Desenvolvedora);


            int rowsAffected = mySqlCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    mensagem = "Jogo cadastrado com sucesso";
                }
                else
                {
                    mensagem = "Falha ao cadastrar jogo ";
                }
                conexaoBD.Close();
           return mensagem;
        }
        public List<Jogo> getBibliotecaUser(int idUser)
        {
            List<int> idJogos = new List<int>();
            List<Jogo> jogoList = new List<Jogo>();

            conexaoBD = abreConexao();

            MySqlCommand query = new MySqlCommand("Select Jogo_idJogo from Biblioteca where Usuario_idUsuario = @idUser", conexaoBD);
            query.Parameters.AddWithValue("@idUser", idUser);

            MySqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                int idJogo =  int.Parse(reader["Jogo_idJogo"].ToString());

                idJogos.Add(idJogo);
            }


            foreach (var id in idJogos)
            {
                jogoList.Add(getJogo(id));
            }
    
            conexaoBD.Close();
            return jogoList;
        }
    }
}

