namespace ProjetoPixelPlace.Entities
{
    public class Usuario
    {
        private int? idUsuario;
        private string nomeUsuario;
        private string email;
        private string senha;
        private byte[]? imagem;
        private string isADM;

        public Usuario(int? idUsuario, string nomeUsuario, string email, string senha, byte[]? imagem, string isADM)
        {
            this.idUsuario = idUsuario;
            this.nomeUsuario = nomeUsuario;
            this.email = email;
            this.senha = senha;
            this.imagem = imagem;
            this.IsADM = isADM;
        }

        public string NomeUsuario { get => nomeUsuario; set => nomeUsuario = value; }
        public byte[]? Imagem { get => imagem; set => imagem = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public int? IdUsuario { get => idUsuario; }
        public string IsADM { get => isADM; set => isADM = value; }
    }
}
