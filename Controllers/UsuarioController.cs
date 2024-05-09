using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel model = new UsuarioModel();
        // GET: UsuarioController
        public ActionResult Index()
        {

            List<Usuario> users = model.getAllUser();
            return View(users);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details()
        {
            return View();
            
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            
            return View();
        }


        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string nomeUsuario, string UrlImage, string email, string senha)
        {
            try
            {
                Usuario u = new Usuario(null, nomeUsuario, UrlImage, email, senha);

                var msg = model.inserirUsuario(u);
                if (msg == "Usuário cadastrado com sucesso")
                return RedirectToAction(nameof(Index));
                else return View(msg);
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
