using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjetoPixelPlace.Controllers
{
    public class JogoController : Controller
    {
        private JogoModel jogoModel = new JogoModel();
        // GET: JogoController
        public ActionResult Index()
        {
            return View(jogoModel.getAllJogos());
        }

        // GET: JogoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JogoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JogoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string nome, string descricao, string categoria, double preco, double desconto, DateTime data)
        {
            string result = "";
            byte[] image = null;
            try
            {
                foreach (IFormFile arq in Request.Form.Files)
                {
                    if (arq.ContentType.Contains("image"))
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        arq.CopyTo(memoryStream);
                        image = memoryStream.ToArray();

                        var jogoAdd = new Jogo(null, nome, image, descricao, categoria, preco, desconto, data);
                        result = jogoModel.inserirJogo(jogoAdd);
                    }
                }
                
                if (result == "Jogo cadastrado com sucesso")
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(result);
            }
            catch
            {
                return View();
            }
        }

        // GET: JogoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JogoController/Edit/5
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

        // GET: JogoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JogoController/Delete/5
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
