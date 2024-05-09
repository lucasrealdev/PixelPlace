using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Entities;
using System.Collections.Generic;

namespace ProjetoPixelPlace.Models
{
    public class UsuarioModel
    {


        public List<Usuario> getAllUser()
        {

            List<Usuario> users = new List<Usuario>();
            MySqlConnection con;
            try
            {
                con = CriadorConexao.getConexao("ConexaoPadrao");
                con.Open();

            }
            catch (Exception ex)
            {
                con = CriadorConexao.getConexao("casa");
                con.Open();
            }

            MySqlCommand comando = new MySqlCommand("Select * from usuario", con);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Usuario usuario = new Usuario(int.Parse(reader["idUsuario"].ToString()),
                    reader["NomeUser"].ToString(),
                    reader["UrlImage"].ToString(),
                    reader["Email"].ToString(),
                    reader["Senha"].ToString());

                users.Add(usuario);
            }
            con.Close();
            return users;
        }


        public string inserirUsuario(Usuario usuario)
        {
            string mensagem = "";
            try
            {
                using (MySqlConnection con = CriadorConexao.getConexao("ConexaoPadrao"))
                {
                    con.Open();
                    using (MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO usuario(nomeUser, urlImage, email, senha) VALUES (@nome, @urlImage, @email, @senha)", con))
                    {
                        mySqlCommand.Parameters.AddWithValue("@nome", usuario.NomeUsuario);
                        mySqlCommand.Parameters.AddWithValue("@urlImage", usuario.UrlImage);
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
                }
            }
            catch (Exception ex)
            {
                mensagem = "Ocorreu um erro ao cadastrar o usuário: " + ex.Message;
            }
            return mensagem;
        }
    }
}