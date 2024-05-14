using MySql.Data.MySqlClient;
using ProjetoPixelPlace.Entities;
using System.Collections.Generic;

namespace ProjetoPixelPlace.Models
{
    public class UsuarioModel
    {
        private MySqlConnection conexaoBD = null;

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

        public List<Usuario> getAllUser()
        {
            List<Usuario> users = new List<Usuario>();          
            byte[] imagem = null; 

            MySqlCommand comando = new MySqlCommand("Select * from usuario", conexaoBD = abreConexao());
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {

                if (!reader.IsDBNull(reader.GetOrdinal("imagem"))){
                    imagem = (byte[])reader["imagem"];
                }
                
                Usuario usuario = new Usuario(int.Parse(reader["idUsuario"].ToString()),
                reader["nomeUser"].ToString(),
                reader["Email"].ToString(),
                reader["Senha"].ToString(),
                 imagem,
                 reader["isADM"].ToString());

                users.Add(usuario);
               
            }
            conexaoBD.Close();
            return users;
        }


        public string inserirUsuario(Usuario usuario)
        {
            string mensagem = "";

            conexaoBD = abreConexao();

            MySqlCommand mySqlCommand = new MySqlCommand("INSERT INTO usuario(nomeUser, email, senha) VALUES (@nome, @email, @senha)", conexaoBD);
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
            conexaoBD.Close();
            return mensagem;
        }
        public Usuario ValidaUser(string email, string senha)
        {
            Usuario user;
            byte[] imagem = null;

            conexaoBD = abreConexao();

            MySqlCommand command = new MySqlCommand("SELECT * FROM USUARIO WHERE email = @email AND senha = @senha", conexaoBD);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@senha", senha);
            MySqlDataReader reader = command.ExecuteReader();

            //caso encontre algo
            while (reader.Read())
            {   

                if (!reader.IsDBNull(reader.GetOrdinal("imagem")))
                {
                    imagem = (byte[])reader["imagem"];
                }

                int idUsuario = (int)reader["idUsuario"];
                string NomeUsuario = (string)reader["nomeUser"];
                string emailUser = (string)reader["email"];
                string senhaU = (string)reader["senha"];
                string isAdm = (string)reader["isAdm"];

                user = new Usuario(idUsuario, NomeUsuario, emailUser, senhaU, imagem, isAdm);

                return user;
            }
            conexaoBD.Close();
            return null;
        }
        
    }  
}
