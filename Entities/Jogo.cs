namespace ProjetoPixelPlace.Entities
{
    public class Jogo
    {
        int? idJogo;
        string nome;
        byte[] imagemCapa;
        string descricao;
        string categoria;
        double preco;
        double desconto;
        DateTime data;

        public Jogo(int? idJogo, string nome, byte[] imagemCapa, string descricao, string categoria, double preco, double desconto, DateTime data)
        {
            this.idJogo = idJogo;
            this.nome = nome;
            this.imagemCapa = imagemCapa;
            this.descricao = descricao;
            this.categoria = categoria;
            this.preco = preco;
            this.desconto = desconto;
            this.data = data;
        }

        public int? IdJogo { get => idJogo; set => idJogo = value; }
        public string Nome { get => nome; set => nome = value; }
        public byte[] ImagemCapa { get => imagemCapa; set => imagemCapa = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public double Preco { get => preco; set => preco = value; }
        public double Desconto { get => desconto; set => desconto = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}
