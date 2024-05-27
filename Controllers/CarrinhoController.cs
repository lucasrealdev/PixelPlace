using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    public class CarrinhoController : Controller
    {

        Carrinho carrinho =  new Carrinho();

        [ServiceFilter(typeof(Autenticao))]
        public IActionResult Carrinho()
        {
            string userJson = HttpContext.Session.GetString("user");

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);

            carrinho.IdUser = (int) u.IdUsuario;
            return View(carrinho.CarrinhoUser()); 
        }

        public IActionResult Delete(int id)
        {
            string message = carrinho.RetirarJogoCarrinho(id);
            if(message == "Retirado do carrinho com sucesso")
            return RedirectToAction(nameof(Carrinho));
            
            return View(message);
        }
    }
}
