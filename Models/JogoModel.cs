using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using System;
using System.Collections.Generic;

namespace ProjetoPixelPlace.Models
{
    public class JogoModel
    {
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
                return conexao;
            }
        }

        public List<Jogo> getAllJogos(string NomeJogo)
        {
            List<Jogo> jogoList = new List<Jogo>();
            byte[] imagem = null;

            using (var conexaoBD = abreConexao())
            using (var query = new MySqlCommand("Select * from jogo where nome = @nomeJogo", conexaoBD))
            {
                query.Parameters.AddWithValue("@nomeJogo", NomeJogo);
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("imagem")))
                        {
                            imagem = (byte[])reader["imagem"];
                        }
                        var jogo = new Jogo(
                            int.Parse(reader["idJogo"].ToString()),
                            reader["nome"].ToString(),
                            imagem,
                            reader["descricao"].ToString(),
                            reader["categoria"].ToString(),
                            double.Parse(reader["preco"].ToString()),
                            double.Parse(reader["desconto"].ToString()),
                            DateTime.Parse(reader["data_lancamento"].ToString()),
                            int.Parse(reader["num_avaliacao"].ToString()),
                            int.Parse(reader["num_estrelas"].ToString()),
                            reader["desenvolvedora"].ToString()
                        );

                        jogoList.Add(jogo);
                    }
                }
            }

            return jogoList;
        }

        public Jogo getJogo(int id)
        {
            Jogo jogo = null;

            using (var conexaoBD = abreConexao())
            using (var query = new MySqlCommand("Select * from jogo where idJogo = @id", conexaoBD))
            {
                query.Parameters.AddWithValue("@id", id);
                using (var reader = query.ExecuteReader())
                {
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
                }
            }


            return jogo;
        }
        public Jogo getJogo(string nomeJogo)
        {
            Jogo jogo = null;

            using (var conexaoBD = abreConexao())
            using (var query = new MySqlCommand("SELECT * FROM jogo WHERE nome = @nomeJogo", conexaoBD))
            {
                query.Parameters.AddWithValue("@nomeJogo", nomeJogo);
                using (var reader = query.ExecuteReader())
                {
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
                }
            }

            return jogo;
        }

        public List<Jogo> getAllJogos()
        {
            List<Jogo> jogoList = new List<Jogo>();
            byte[] imagem = null;

            using (var conexaoBD = abreConexao())
            using (var query = new MySqlCommand("Select * from jogo", conexaoBD))
            using (var reader = query.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("imagem")))
                    {
                        imagem = (byte[])reader["imagem"];
                    }
                    var jogo = new Jogo(
                        int.Parse(reader["idJogo"].ToString()),
                        reader["nome"].ToString(),
                        imagem,
                        reader["descricao"].ToString(),
                        reader["categoria"].ToString(),
                        double.Parse(reader["preco"].ToString()),
                        double.Parse(reader["desconto"].ToString()),
                        DateTime.Parse(reader["data_lancamento"].ToString()),
                        int.Parse(reader["num_avaliacao"].ToString()),
                        int.Parse(reader["num_estrelas"].ToString()),
                        reader["desenvolvedora"].ToString()
                    );

                    jogoList.Add(jogo);
                }
            }

            return jogoList;
        }

        public string inserirJogo(Jogo jogo)
        {
            string mensagem;

            using (var conexaoBD = abreConexao())
            using (var mySqlCommand = new MySqlCommand("INSERT INTO Jogo(nome, imagem, descricao, categoria, preco, desconto, data_lancamento, num_avaliacao, num_estrelas, desenvolvedora) VALUES (@nome, @imagem, @descricao, @categoria, @preco, @desconto, @data_lancamento, @num_avaliacao, @num_estrelas, @desenvolvedora)", conexaoBD))
            {
                mySqlCommand.Parameters.AddWithValue("@nome", jogo.Nome);
                mySqlCommand.Parameters.AddWithValue("@imagem", jogo.Imagem);
                mySqlCommand.Parameters.AddWithValue("@descricao", jogo.Descricao);
                mySqlCommand.Parameters.AddWithValue("@categoria", jogo.Categoria);
                mySqlCommand.Parameters.AddWithValue("@preco", jogo.Preco);
                mySqlCommand.Parameters.AddWithValue("@desconto", jogo.Desconto);
                mySqlCommand.Parameters.AddWithValue("@data_lancamento", jogo.Data_lancamento.ToString("yyyy-MM-dd HH:mm"));
                mySqlCommand.Parameters.AddWithValue("@num_avaliacao", jogo.Numero_avaliacao);
                mySqlCommand.Parameters.AddWithValue("@num_estrelas", jogo.Numero_estrelas);
                mySqlCommand.Parameters.AddWithValue("@desenvolvedora", jogo.Desenvolvedora);

                int rowsAffected = mySqlCommand.ExecuteNonQuery();
                mensagem = rowsAffected > 0 ? "Jogo cadastrado com sucesso" : "Falha ao cadastrar jogo";
            }

            return mensagem;
        }

        public List<Jogo> getBibliotecaUser(int idUser)
        {
            List<int> idJogos = new List<int>();
            List<Jogo> jogoList = new List<Jogo>();

            using (var conexaoBD = abreConexao())
            using (var query = new MySqlCommand("Select Jogo_idJogo from Biblioteca where Usuario_idUsuario = @idUser", conexaoBD))
            {
                query.Parameters.AddWithValue("@idUser", idUser);
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idJogo = int.Parse(reader["Jogo_idJogo"].ToString());
                        idJogos.Add(idJogo);
                    }
                }
            }

            foreach (var id in idJogos)
            {
                jogoList.Add(getJogo(id));
            }

            return jogoList;
        }
        public string InserirJogoNaBiblioteca(int idUsuario, int idJogo)
        {
            try
            {
                    

                using (var conexaoBD = abreConexao())
                using (var comando = new MySqlCommand("INSERT INTO Biblioteca (Usuario_idUsuario, Jogo_idJogo) VALUES (@idUsuario, @idJogo)", conexaoBD))
                {
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@idJogo", idJogo);

                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        return "Jogo adicionado à biblioteca com sucesso!";
                    }
                    else
                    {
                        return "Falha ao adicionar jogo à biblioteca.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Erro ao inserir jogo na biblioteca: " + ex.Message;
            }
        }

        public List<Jogo> getListaDesejoUser(int idUser)
        {
            List<int> idJogos = new List<int>();
            List<Jogo> jogoList = new List<Jogo>();

            using (var conexaoBD = abreConexao())
            using (var query = new MySqlCommand("Select Jogo_idJogo from favoritos where Usuario_idUsuario = @idUser", conexaoBD))
            {
                query.Parameters.AddWithValue("@idUser", idUser);
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idJogo = int.Parse(reader["Jogo_idJogo"].ToString());
                        idJogos.Add(idJogo);
                    }
                }
            }

            foreach (var id in idJogos)
            {
                jogoList.Add(getJogo(id));
            }

            return jogoList;
        }
    }
}
