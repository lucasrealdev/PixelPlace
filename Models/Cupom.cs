using MySql.Data.MySqlClient;
using System;

namespace ProjetoPixelPlace.Models
{
    public class Cupom
    {
        private string cupom;
        private int porcentagem_desconto;

        public Cupom() { }

        public string CupomId { get => cupom; set => cupom = value; }
        public int PorcentagemDesconto { get => porcentagem_desconto; set => porcentagem_desconto = value; }

        public MySqlConnection AbreConexao()
        {
            MySqlConnection conexao;
            try
            {
                conexao = CriadorConexao.getConexao("ConexaoPadrao");
                conexao.Open();
                return conexao;
            }
            catch (Exception)
            {
                conexao = CriadorConexao.getConexao("casa");
                conexao.Open();
                return conexao;
            }
        }

        public int GetCupom(string nomeCupom)
        {
            Cupom cupom = null;

            using (var mySqlConnection = AbreConexao())
            using (var comando = new MySqlCommand("SELECT * FROM cupons_desconto WHERE cupom = @cupom", mySqlConnection))
            {
                comando.Parameters.AddWithValue("@cupom", nomeCupom);

                using (var reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cupom = new Cupom
                        {
                            CupomId = reader.GetString("cupom"),
                            PorcentagemDesconto = reader.GetInt32("porcentagem_desconto")
                        };
                    }
                }
            }

            return cupom?.PorcentagemDesconto ?? 0;
        }
    }
}
