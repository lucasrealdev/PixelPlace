using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ProjetoPixelPlace.Models
{
    public class Carrinho
    {
        private int idUser;
        private int idJogo;

        public Carrinho() { }

        public Carrinho(int idUser, int idJogo)
        {
            this.idUser = idUser;
            this.idJogo = idJogo;
        }

        public int IdUser { get => idUser; set => idUser = value; }
        public int IdJogo { get => idJogo; set => idJogo = value; }

        public MySqlConnection AbreConexao()
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
                return conexao;
            }
        }

        public string InserirCarrinho()
        {
            using (MySqlConnection mySqlConnection = AbreConexao())
            {
                if (VerificaCarrinho())
                    return $"Você já possui este jogo no carrinho";

                MySqlCommand comando = new MySqlCommand("INSERT INTO carrinho (Usuario_idUsuario, Jogo_idJogo) VALUES (@Usuario_idUsuario, @Jogo_idJogo)", mySqlConnection);
                comando.Parameters.AddWithValue("@Usuario_idUsuario", IdUser);
                comando.Parameters.AddWithValue("@Jogo_idJogo", IdJogo);

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return $"Não foi possível inserir no carrinho: {ex.Message}";
                }
            }

            return "Inserido no carrinho com sucesso.";
        }

        public bool VerificaCarrinho()
        {
            using (MySqlConnection mySqlConnection = AbreConexao())
            {
                MySqlCommand comando = new MySqlCommand("SELECT COUNT(*) FROM carrinho WHERE Usuario_idUsuario = @idU AND Jogo_idJogo = @idJ", mySqlConnection);
                comando.Parameters.AddWithValue("@idU", IdUser);
                comando.Parameters.AddWithValue("@idJ", IdJogo);

                int count = Convert.ToInt32(comando.ExecuteScalar());
                return count > 0;
            }
        }

        public List<Carrinho> CarrinhoUser()
        {
            List<Carrinho> carrinhos = new List<Carrinho>();

            using (MySqlConnection mySqlConnection = AbreConexao())
            {
                MySqlCommand comando = new MySqlCommand("SELECT * FROM carrinho WHERE Usuario_idUsuario = @id", mySqlConnection);
                comando.Parameters.AddWithValue("@id", IdUser);

                using (MySqlDataReader comandoDataReader = comando.ExecuteReader())
                {
                    while (comandoDataReader.Read())
                    {
                        int idUser = comandoDataReader.GetInt32("Usuario_idUsuario");
                        int idJogo = comandoDataReader.GetInt32("Jogo_idJogo");

                        carrinhos.Add(new Carrinho(idUser, idJogo));
                    }
                }
            }

            return carrinhos;
        }

        public string RetirarJogoCarrinho(int id, int idUser)
        {
            using (MySqlConnection mySqlConnection = AbreConexao())
            {
                MySqlCommand comando = new MySqlCommand("DELETE FROM carrinho WHERE Jogo_idJogo = @id AND Usuario_idUsuario = @iduser", mySqlConnection);
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@iduser", idUser);

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return $"Não foi possível retirar do carrinho: {ex.Message}";
                }
            }

            return "Retirado do carrinho com sucesso";
        }
    }
}
