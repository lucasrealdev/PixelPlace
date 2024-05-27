using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    public class TransacaoController : Controller
    {
        Transacao transacao = new Transacao();

        [ServiceFilter(typeof(Autenticao))]
        public ActionResult Transacao()
        {

            string userJson = HttpContext.Session.GetString("user");

            Usuario u = JsonConvert.DeserializeObject<Usuario>(userJson);

            transacao.User_id = (int) u.IdUsuario;
            return View(transacao.getAllTransacaoOfUser());
        }
    }
}
