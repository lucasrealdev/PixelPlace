using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Entities
{
    public class Transacao
    {
        private int  user_id;
        private string itens;
        private DateTime data_venda;
        private decimal valor_total;
        private string metodo_pagamento;
        private string tipo_compra;

        public Transacao()
        {

        }
        public Transacao(int user_id, string itens, DateTime data_venda, decimal valor_total, string metodo_pagamento, string tipo_compra)
        {
            this.user_id = user_id;
            this.itens = itens;
            this.data_venda = data_venda;
            this.valor_total = valor_total;
            this.metodo_pagamento = metodo_pagamento;
            this.tipo_compra = tipo_compra;
        }
        
        public int User_id { get => user_id; set => user_id = value; }
        public string Itens { get => itens; set => itens = value; }
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


        public string inserirTransacao(int usuarioId, string jogoId, DateTime dataTransacao, decimal valorTotal, string metodoPagamento, string tipoCompra)
        {
            try
            {
                using (MySqlConnection bd = abreConexao())
                {
                    string query = @"INSERT INTO transacao 
                             (Usuario_idUsuario, itens, data_transacao, valor_total, metodo_pagamento, tipo_compra) 
                             VALUES 
                             (@Usuario_idUsuario, @itens, @data_transacao, @valor_total, @metodo_pagamento, @tipo_compra)";

                    using (MySqlCommand cmd = new MySqlCommand(query, bd))
                    {
                        cmd.Parameters.AddWithValue("@Usuario_idUsuario", usuarioId);
                        cmd.Parameters.AddWithValue("@itens",jogoId);
                        cmd.Parameters.AddWithValue("@data_transacao", dataTransacao.ToString("yyyy-MM-dd HH:mm"));
                        cmd.Parameters.AddWithValue("@valor_total", valorTotal);
                        cmd.Parameters.AddWithValue("@metodo_pagamento", metodoPagamento);
                        cmd.Parameters.AddWithValue("@tipo_compra", tipoCompra);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return "Transação inserida com sucesso!";
                        }
                        else
                        {
                            return "Falha ao inserir transação.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }
        public List<Transacao> getAllTransacao()
        {
            List<Transacao> transacoes = new List<Transacao>();

            using (MySqlConnection mySqlConnection = abreConexao())
            {
                MySqlCommand comando = new MySqlCommand("SELECT * FROM transacao", mySqlConnection);

                using (MySqlDataReader comandoDataReader = comando.ExecuteReader())
                {
                    while (comandoDataReader.Read())
                    {
                        int userId = comandoDataReader.GetInt32("Usuario_idUsuario");
                        string jogoId = comandoDataReader.GetString("itens");
                        DateTime dataVenda = comandoDataReader.GetDateTime("data_transacao");
                        decimal valorTotal = comandoDataReader.GetDecimal("valor_total");
                        string metodoPagamento = comandoDataReader.GetString("metodo_pagamento");
                        string tipoCompra = comandoDataReader.GetString("tipo_compra");

                        transacoes.Add(new Transacao(userId, jogoId, dataVenda, valorTotal, metodoPagamento, tipoCompra));
                    }
                }
            }

            return transacoes;
        }

        public List<Transacao> getAllTransacaoOfUser()
        {
            List<Transacao> transacoes = new List<Transacao>();

            using (MySqlConnection mySqlConnection = abreConexao())
            {
                MySqlCommand comando = new MySqlCommand("SELECT * FROM transacao WHERE Usuario_idUsuario = @id", mySqlConnection);
                comando.Parameters.AddWithValue("@id", User_id);

                using (MySqlDataReader comandoDataReader = comando.ExecuteReader())
                {
                    while (comandoDataReader.Read())
                    {
                        int userId = comandoDataReader.GetInt32("Usuario_idUsuario");
                        string jogoId = comandoDataReader.GetString("itens");
                        DateTime dataVenda = comandoDataReader.GetDateTime("data_transacao");
                        decimal valorTotal = comandoDataReader.GetDecimal("valor_total");
                        string metodoPagamento = comandoDataReader.GetString("metodo_pagamento");
                        string tipoCompra = comandoDataReader.GetString("tipo_compra");

                        transacoes.Add(new Transacao(userId, jogoId, dataVenda, valorTotal, metodoPagamento, tipoCompra));
                    }
                }
            }

            return transacoes;
        }


    }
}
