using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    public class TransacaoController : Controller
    {
         private Transacao transacao = new Transacao();
         private JogoModel jogoModel = new JogoModel();

        [ServiceFilter(typeof(Autenticao))]
        public ActionResult Transacao()
        {

            string userJson = HttpContext.Session.GetString("user");

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);

            transacao.User_id = (int) u.IdUsuario;
            return View(transacao.getAllTransacaoOfUser());
        }

        [ServiceFilter(typeof(Autenticao))]
        [HttpPost]
        public ActionResult InserirTransacao(int idjogos, string itens, decimal valorTotal, string metodoPagamento, string tipoCompra)
        {
            // Recuperar o usuário da sessão
            string userJson = HttpContext.Session.GetString("user");

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);

            // Criar um novo objeto de transação e definir suas propriedades
            Transacao transacao = new Transacao
            {
                User_id = (int)u.IdUsuario,
                Itens = itens,
                Data_venda = DateTime.Now,
                Valor_total = valorTotal,
                Metodo_pagamento = metodoPagamento,
                Tipo_compra = tipoCompra
            };

            // Chamar o método inserirTransacao e capturar o resultado
            string resultado = transacao.inserirTransacao(transacao.User_id, transacao.Itens, transacao.Data_venda, transacao.Valor_total, transacao.Metodo_pagamento, transacao.Tipo_compra);

            // Verificar o resultado e retornar a view apropriada
            if (resultado == "Transação inserida com sucesso!")
            {
                Carrinho carrinho = new Carrinho();

                carrinho.RetirarJogoCarrinho(idjogos, transacao.User_id);
                jogoModel.InserirJogoNaBiblioteca(transacao.User_id, idjogos);
                return RedirectToAction(nameof(Transacao)); // Retorna uma view de sucesso
            }
            else
            {
                ViewBag.ErrorMessage = resultado;
                return View("Error"); // Retorna uma view de erro com a mensagem de erro
            }
        }
    }
}
