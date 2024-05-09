namespace ProjetoPixelPlace.Entities
{
    public class Usuario
    {
        private int? idUsuario;
        private string nomeUsuario;
        private string urlImage;
        private string email;
        private string senha;
        private object value;
        private string nomeUser;
        private string imageUrl;

        public Usuario(int? idUsuario, string nomeUsuario, string urlImage, string email, string senha)
        {
            this.idUsuario = idUsuario;
            this.nomeUsuario = nomeUsuario;
            this.urlImage = urlImage;
            this.email = email;
            this.senha = senha;
        }

      

        public string NomeUsuario { get => nomeUsuario; set => nomeUsuario = value; }
        public string UrlImage { get => urlImage; set => urlImage = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public int? IdUsuario { get => idUsuario; }

    }
}
