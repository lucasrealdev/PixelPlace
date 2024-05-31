using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Entities;
using System;
using System.Collections.Generic;

namespace ProjetoPixelPlace.Models
{
    public class UsuarioModel
    {
        private MySqlConnection conexaoBD;

        public MySqlConnection abreConexao()
        {
            MySqlConnection con;
            try
            {
                con = CriadorConexao.getConexao("ConexaoPadrao");
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                con = CriadorConexao.getConexao("casa");
                con.Open();
                return con;
            }
        }

        public Usuario getUser(int id)
        {
            byte[] imagem = null;

            using (var conexao = abreConexao())
            using (var comando = new MySqlCommand("Select * from usuario where idUsuario = @id", conexao))
            {
                comando.Parameters.AddWithValue("@id", id);
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("imagem")))
                        {
                            imagem = (byte[])reader["imagem"];
                        }

                        return new Usuario(
                            int.Parse(reader["idUsuario"].ToString()),
                            reader["nomeUser"].ToString(),
                            reader["Email"].ToString(),
                            reader["Senha"].ToString(),
                            imagem,
                            reader["isADM"].ToString()
                        );
                    }
                }
            }

            return null;
        }

        public List<Usuario> getAllUser()
        {
            List<Usuario> users = new List<Usuario>();
            byte[] imagem = null;

            using (var conexao = abreConexao())
            using (var comando = new MySqlCommand("Select * from usuario", conexao))
            using (var reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("imagem")))
                    {
                        imagem = (byte[])reader["imagem"];
                    }

                    var usuario = new Usuario(
                        int.Parse(reader["idUsuario"].ToString()),
                        reader["nomeUser"].ToString(),
                        reader["Email"].ToString(),
                        reader["Senha"].ToString(),
                        imagem,
                        reader["isADM"].ToString()
                    );

                    users.Add(usuario);
                }
            }

            return users;
        }

        public string inserirUsuario(Usuario usuario)
        {
            string mensagem = "";

            using (var conexao = abreConexao())
            using (var mySqlCommand = new MySqlCommand("INSERT INTO usuario(nomeUser, email, senha) VALUES (@nome, @email, @senha)", conexao))
            {
                mySqlCommand.Parameters.AddWithValue("@nome", usuario.NomeUsuario);
                mySqlCommand.Parameters.AddWithValue("@email", usuario.Email);
                mySqlCommand.Parameters.AddWithValue("@senha", usuario.Senha);
                int rowsAffected = mySqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    mensagem = "Usuário cadastrado com sucesso";
                }
                else
                {
                    mensagem = "Falha ao cadastrar usuário";
                }
            }

            return mensagem;
        }

        public Usuario ValidaUser(string email, string senha)
        {
            Usuario user = null;
            byte[] imagem = null;

            using (var conexao = abreConexao())
            using (var command = new MySqlCommand("SELECT * FROM USUARIO WHERE email = @email AND senha = @senha", conexao))
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@senha", senha);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("imagem")))
                        {
                            imagem = (byte[])reader["imagem"];
                        }

                        user = new Usuario(
                            (int)reader["idUsuario"],
                            (string)reader["nomeUser"],
                            (string)reader["email"],
                            (string)reader["senha"],
                            imagem,
                            (string)reader["isAdm"]
                        );

                        return user;
                    }
                }
            }

            return null;
        }

        internal string getUser()
        {
            throw new NotImplementedException();
        }
    }
}
