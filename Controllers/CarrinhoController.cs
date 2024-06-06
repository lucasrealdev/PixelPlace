using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    public class CarrinhoController : Controller
    {

        Carrinho carrinho = new Carrinho();
        Cupom cupom = new Cupom();

        [ServiceFilter(typeof(Autenticao))]
        public IActionResult Carrinho(string NomeCupom)
        {

            string userJson = HttpContext.Session.GetString("user");
            if (userJson == null)
            {
                return View();
            }

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);

            carrinho.IdUser = (int)u.IdUsuario;
            ViewData["porcentagemCupom"] = cupom.GetCupom(NomeCupom);
            return View(carrinho.CarrinhoUser());
        }
        [ServiceFilter(typeof(Autenticao))]
        //Aqui vou inserir no carrinho 
        public IActionResult InserirCarrinho(int id)
        {
            string userJson = HttpContext.Session.GetString("user");

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);

            carrinho.IdJogo = id;
            carrinho.IdUser = (int)u.IdUsuario;

            string message = carrinho.InserirCarrinho();
            if (message == "Inserido no carrinho com sucesso.")
                return RedirectToAction(nameof(Carrinho));

            return View(message);
        }
        public IActionResult Delete(int idJogo)
        {
            string userJson = HttpContext.Session.GetString("user");

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);


            string message = carrinho.RetirarJogoCarrinho(idJogo, (int)u.IdUsuario);
            if (message == "Retirado do carrinho com sucesso")
                return RedirectToAction(nameof(Carrinho));

            return View(message);
        }


    }
}