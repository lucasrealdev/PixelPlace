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
        //Oi pra quem tiver lendo, espero que voce não entenda nada, e q eu esteja r.i.p


        //model do banco de dados
        UsuarioModel model = new UsuarioModel();


        
        //colocar como parametrom, email e senha, ver se existe esse usuario, caso existir, coloca ele em uma session
        public ActionResult Logar()
        {
            
            // era pra ser model.ValidaUser(), mas criei usuario de teste
            //criei um usuario teste
            Usuario user = new Usuario(null,"Joao","1234","kaio","1234");
            
            //coloquei ele numa session
            HttpContext.Session.SetString("user",JsonConvert.SerializeObject(user));

            //e retornei a index de listar, mas deveria validar se ele realmente esta logado;
            return RedirectToAction("Index");
        }

        //index cadastrar, aqui vira o model.inserirUsuario...
        public ActionResult Create()
        {
            return View();
        }
        
        public ActionResult Index()
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
        public ActionResult Create(string nomeUsuario, string UrlImage, string email, string senha)
        {  
            try
            {
                Usuario u = new Usuario(null, nomeUsuario, UrlImage, email, senha);

                var msg = model.inserirUsuario(u);

                if (msg == "Usuário cadastrado com sucesso")
                return RedirectToAction(nameof(Index));

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
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
