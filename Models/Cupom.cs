﻿using MySql.Data.MySqlClient;
using System;

namespace ProjetoPixelPlace.Models
{
    public class Cupom
    {
        private int cupom;
        private int porcentagem_desconto;

        public Cupom() { }

        public int CupomId { get => cupom; set => cupom = value; }
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
            }

            return conexao;
        }

        public Cupom GetCupom(int idCupom)
        {
            MySqlConnection mySqlConnection = AbreConexao();

            MySqlCommand comando = new MySqlCommand("SELECT * FROM cupons_desconto WHERE cupom = @cupom", mySqlConnection);
            comando.Parameters.AddWithValue("@cupom", idCupom);

            MySqlDataReader reader = comando.ExecuteReader();

            Cupom cupom = null;

            if (reader.Read())
            {
                cupom = new Cupom
                {
                    CupomId = reader.GetInt32("cupom"),
                    PorcentagemDesconto = reader.GetInt32("porcentagem_desconto")
                };
            }

            reader.Close();
            mySqlConnection.Close();

            return cupom;
        }
    }
}