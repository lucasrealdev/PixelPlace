using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Entities
{
    public class Transacao
    {
        private int  user_id;
        private int jogo_id;
        private DateTime data_venda;
        private decimal valor_total;
        private string metodo_pagamento;
        private string tipo_compra;

        public Transacao()
        {

        }
        public Transacao(int user_id, int jogo_id, DateTime data_venda, decimal valor_total, string metodo_pagamento, string tipo_compra)
        {
            this.user_id = user_id;
            this.jogo_id = jogo_id;
            this.data_venda = data_venda;
            this.valor_total = valor_total;
            this.metodo_pagamento = metodo_pagamento;
            this.tipo_compra = tipo_compra;
        }
        
        public int User_id { get => user_id; set => user_id = value; }
        public int Jogo_id { get => jogo_id; set => jogo_id = value; }
        public DateTime Data_venda { get => data_venda; set => data_venda = value; }
        public decimal Valor_total { get => valor_total; set => valor_total = value; }
        public string Metodo_pagamento { get => metodo_pagamento; set => metodo_pagamento = value; }
        public string Tipo_compra { get => tipo_compra; set => tipo_compra = value; }

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


        public string inserirTransacao()
        {
            MySqlConnection mySqlConnection = abreConexao();

            MySqlCommand comando = new MySqlCommand("Insert into transacao(Usuario_idUsuario, Jogo_idJogo, data_transacao, valor_total, metodo_pagamento, tipo_compra) values(@Usuario_idUsuario, @Jogo_idJogo, @data_transacao, @valor_total, @metodo_pagamento, @tipo_compra)", mySqlConnection);
            comando.Parameters.AddWithValue("@Usuario_idUsuario", User_id);
            comando.Parameters.AddWithValue("@Jogo_idJogo", Jogo_id);
            

            comando.Parameters.AddWithValue("@data_transacao", Data_venda.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@valor_total", Valor_total);
            comando.Parameters.AddWithValue("@metodo_pagamento", Metodo_pagamento);
            comando.Parameters.AddWithValue("@tipo_compra", Tipo_compra);
            try
            {
                comando.ExecuteNonQuery();
            }
            catch(Exception ex) {
                return $"Erro ao inserir no banco de dados, erro : {ex.ToString}";
            }

            return "Transação salva com sucesso!";
        }
        public List<Transacao> getAllTransacao()
        {

            List<Transacao> transacaoes = new List<Transacao>();

            MySqlConnection mySqlConnection = abreConexao();

            MySqlCommand comando = new MySqlCommand("Select * from transacao", mySqlConnection);

            MySqlDataReader comandoDataReader = comando.ExecuteReader();
            while(comandoDataReader.Read())
            {
                User_id = comandoDataReader.GetInt32("Usuario_idUsuario");
                Jogo_id = comandoDataReader.GetInt32("Jogo_idJogo");
                Data_venda= comandoDataReader.GetDateTime("data_transacao");
                Valor_total = comandoDataReader.GetDecimal("valor_total");
                Metodo_pagamento = comandoDataReader.GetString("metodo_pagamento");
                Tipo_compra = comandoDataReader.GetString("tipo_compra");

                transacaoes.Add(new Transacao(User_id, Jogo_id, Data_venda, Valor_total, Metodo_pagamento, Tipo_compra));
            }
            return transacaoes;
        }
        public List<Transacao> getAllTransacaoOfUser()
        {

            List<Transacao> transacaoes = new List<Transacao>();

            MySqlConnection mySqlConnection = abreConexao();

            MySqlCommand comando = new MySqlCommand("Select * from transacao where Usuario_idUsuario = @id ", mySqlConnection);
            comando.Parameters.AddWithValue("@id", User_id);

            MySqlDataReader comandoDataReader = comando.ExecuteReader();
            while (comandoDataReader.Read())
            {
                User_id = comandoDataReader.GetInt32("Usuario_idUsuario");
                Jogo_id = comandoDataReader.GetInt32("Jogo_idJogo");
                Data_venda = comandoDataReader.GetDateTime("data_transacao");
                Valor_total = comandoDataReader.GetDecimal("valor_total");
                Metodo_pagamento = comandoDataReader.GetString("metodo_pagamento");
                Tipo_compra = comandoDataReader.GetString("tipo_compra");

                transacaoes.Add(new Transacao(User_id, Jogo_id, Data_venda, Valor_total, Metodo_pagamento, Tipo_compra));
            }
            return transacaoes;
        }


    }
}
