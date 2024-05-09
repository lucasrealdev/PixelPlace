using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Entities;
using System.Collections.Generic;

namespace ProjetoPixelPlace.Models
{
    public class UsuarioModel
    {
        //REMODELAR ESSA CLASSE TA HORRIVEL....

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
        public Object ValidaUser(string email, string password)
        {
            Usuario user;

            MySqlConnection con;
            try
            {
                con = CriadorConexao.getConexao("ConexaoPadrao");
                con.Open();
                con.Close();

            }
            catch (Exception ex)
            {
                con = CriadorConexao.getConexao("casa");
                con.Open();
            }



            MySqlCommand command = new MySqlCommand("SELECT * FROM USUARIO WHERE email = @email AND senha = @senha", con);
            command.Parameters.AddWithValue("@email", 123);
            command.Parameters.AddWithValue("@senha", 123);
            MySqlDataReader reader = command.ExecuteReader();

            //caso encontre algo
            while (reader.Read())
            {
                int idUsuario = (int)reader["idUsuario"];
                string NomeUsuario = (string)reader["nomeUser"];
                string urlImage = (string)reader["urlImage"];
                string emailUser = (string)reader["email"];
                string senha = (string)reader["senha"];

                user = new Usuario(idUsuario, NomeUsuario, urlImage, emailUser, senha);

                return user;
            }
            return "Usuario não encontrado!";
        }
    }  
}
