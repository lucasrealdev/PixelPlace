namespace ProjetoPixelPlace.Entities
{
    public class Usuario
    {
        private int? idUsuario;
        private string nomeUsuario;
        private string email;
        private string senha;
        private byte[]? imagem;


        public Usuario(int? idUsuario, string nomeUsuario, byte[]? imagem, string email, string senha)
        {
            this.idUsuario = idUsuario;
            this.nomeUsuario = nomeUsuario;
            this.imagem = imagem;
            this.email = email;
            this.senha = senha;
        }

      

        public string NomeUsuario { get => nomeUsuario; set => nomeUsuario = value; }
        public byte[]? Imagem { get => imagem; set => imagem = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public int? IdUsuario { get => idUsuario; }

    }
}
