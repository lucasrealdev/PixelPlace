using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    public class CarrinhoController : Controller
    {

        Carrinho carrinho =  new Carrinho();
        Cupom cupom = new Cupom();  

        [ServiceFilter(typeof(Autenticao))]
        public IActionResult Carrinho(string NomeCupom)
        {

            string userJson = HttpContext.Session.GetString("user");
            if(userJson == null)
            {
                return View();
            }

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);

            carrinho.IdUser = (int) u.IdUsuario;
            ViewData["porcentagemCupom"] = cupom.GetCupom(NomeCupom);
            return View(carrinho.CarrinhoUser()); 
        }

        public IActionResult Delete(int idJogo)
        {
            string userJson = HttpContext.Session.GetString("user");

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);


            string message = carrinho.RetirarJogoCarrinho(idJogo,(int) u.IdUsuario);
            if(message == "Retirado do carrinho com sucesso")
            return RedirectToAction(nameof(Carrinho));
            
            return View(message);
        }
       

    }
}
