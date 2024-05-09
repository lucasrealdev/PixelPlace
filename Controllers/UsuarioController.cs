using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
 
    public class UsuarioController : Controller
    {
        //Oi lucas, ana ou samuel


        //Injeção do nosso repositorio de dados
        UsuarioModel model = new UsuarioModel();


        public ActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logar(string email, string senha)   //colocar como parametro, email e senha, ver se existe esse usuario, caso existir, coloca ele em uma session
        {
            //verifico se existe, caso n, devolvo null
            var user = model.ValidaUser(email, senha);
            if (user!=null)
            {
                //caso tiver eu coloco na session
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                return RedirectToAction("Listagem");
            }
            return View();           
        }

        public ActionResult Create()
        {
            return View();
        }

        [ServiceFilter(typeof(Autenticao))]
        public ActionResult Listagem()
        {
            //aqui eu vejo se ele realmente pode estar aqui...
            if (HttpContext.Session.GetString("user") != null)
            {
                //caso puder, eu disponibilizo a lista
                List<Usuario> users = model.getAllUser();
                return View(users);
            }
            //caso nao tiver, eu devolvo a lista vazia *(fazer um validacao no index, caso a lista estiver vazia,
            //retornar a tela de erro
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details()
        {
            return View();

        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //aqui é o create do usuario...
        public ActionResult Create(string nomeUsuario, string email, string senha)
        {
            try
            {
                byte[] imagem = null;
                var msg = "";

                //crio um user
                Usuario u = new Usuario(null, nomeUsuario, imagem, email, senha);
                //insiro o user e guardo a resposta..
                msg = model.inserirUsuario(u);

                //se a mensagem for sucedida, mando pro index, caso nao retorno a mensagem de erro pra mesma view, criar um dialogo sobre isso
                if (msg == "Usuário cadastrado com sucesso")
                {

                    //coloquei ele numa session
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(u));

                    // e retorno a imagem
                    return RedirectToAction(nameof(Listagem));
                }

                //para parar de dar erro, somente produzir uma mensagem de erro 
                //utilizo assim por debug somente
                return View(msg);
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Listagem));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Listagem));
            }
            catch
            {
                return View();
            }
        }
    }
}
