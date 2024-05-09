namespace ProjetoPixelPlace.Entities
{
    public class Jogo
    {
        int? idJogo;
        string nome;
        byte[] imagem;
        string descricao;
        string categoria;
        double preco;
        double desconto;
        DateTime data_lancamento;
        int numero_avaliacao;
        int numero_estrelas;
        string desenvolvedora;
        int jogo_destaque;

        public Jogo(int? idJogo, string nome, byte[] imagem, string descricao, string categoria, double preco, double desconto, DateTime data_lancamento, int numero_avaliacao, int numero_estrelas, string desenvolvedora, int jogo_destaque)
        {
            this.idJogo = idJogo;
            this.nome = nome;
            this.imagem = imagem;
            this.descricao = descricao;
            this.categoria = categoria;
            this.preco = preco;
            this.desconto = desconto;
            this.data_lancamento = data_lancamento;
            this.numero_avaliacao = numero_avaliacao;
            this.numero_estrelas = numero_estrelas;
            this.desenvolvedora = desenvolvedora;
            this.jogo_destaque = jogo_destaque;
        }

        public int? IdJogo { get => idJogo; set => idJogo = value; }
        public string Nome { get => nome; set => nome = value; }
        public byte[] Imagem { get => imagem; set => imagem = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public double Preco { get => preco; set => preco = value; }
        public double Desconto { get => desconto; set => desconto = value; }
        public DateTime Data_lancamento { get => data_lancamento; set => data_lancamento = value; }
        public int Numero_avaliacao { get => numero_avaliacao; set => numero_avaliacao = value; }
        public int Numero_estrelas { get => numero_estrelas; set => numero_estrelas = value; }
        public string Desenvolvedora { get => desenvolvedora; set => desenvolvedora = value; }
        public int Jogo_destaque { get => jogo_destaque; set => jogo_destaque = value; }
    }
}
