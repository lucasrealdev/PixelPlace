using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    public class DesejoController : Controller
    {
        

        ListaDesejoModel model = new ListaDesejoModel();

        [ServiceFilter(typeof(Autenticao))]
        public ActionResult Index()
        {
              return View();
        }



        [ServiceFilter(typeof(Autenticao))]
        //aqui eu passo o id do jogo 
        public ActionResult Adicionar(int id)
        {

            string userJson = HttpContext.Session.GetString("user");


            Usuario user = JsonConvert.DeserializeObject<Usuario>(userJson);

            int idUser = (int) user.IdUsuario;
            

            return View(nameof(Index));
        }




    }
}
