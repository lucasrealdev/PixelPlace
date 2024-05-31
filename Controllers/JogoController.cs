using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjetoPixelPlace.Controllers
{
    public class JogoController : Controller
    {

        //injecao da classe model
        private JogoModel jogoModel = new JogoModel();
        private ListaDesejoModel listaDesejoModel = new ListaDesejoModel();

        [ServiceFilter(typeof(Autenticao))]
        public ActionResult IndexADM()
        {
            if (HttpContext.Session.GetString("adm") != null)
            {
                return View(jogoModel.getAllJogos());
            }
            //no index retorna todos os jogos.
            return RedirectToAction("Index");
        }

        
        public ActionResult Index()
        {
            //no index retorna todos os jogos.
            return View(jogoModel.getAllJogos());
        }

        [ServiceFilter(typeof(Autenticao))]
        public ActionResult Loja()
        {
            //no index retorna todos os jogos.
            return View(jogoModel.getAllJogos());

            TempData["message"] = "boa karaiii";
        }
        //aqui é quando ele pesquisar algum jogo na loja pelo nome. 
        [HttpPost]
        [ServiceFilter(typeof(Autenticao))]
        public ActionResult Loja(string NomeJogo)
        {

            if (!string.IsNullOrEmpty(NomeJogo))
            {
                return View(jogoModel.getAllJogos(NomeJogo));
            }
            //no index retorna todos os jogos.
            return View(jogoModel.getAllJogos());
        }


        [ServiceFilter(typeof(Autenticao))]
        // GET: JogoController/Details/5
        public ActionResult Details(int id)
        {
            return View(jogoModel.getJogo(id));
        }

        // GET: JogoController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("adm") == null)
            {
                return RedirectToAction("Logar", "Usuario");
            }
            //aqui retorna a view pela primeira vez
            return View();
        }

        // POST: JogoController/Create
        //quando clickar no submit, entrara nesse metodo, aonde realizara o cadastro.
        [ServiceFilter(typeof(Autenticao))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string nome, string descricao, string categoria, double preco, double desconto, DateTime data_lancamento, int numero_estrelas, int numero_avaliacao, string desenvolvedora)
        {
           
            //aqui eu crio a vario result, a qual mostrar a mensagem de retorno no metodo create jogo
            string result = "";
            //crio uma imagem, talvez possa ser que não estou usando...
            
            try
            {
                //aqui eu pego a imagem enviada no corpo do request
                foreach (IFormFile arq in Request.Form.Files)
                {
                    //se o arquivo for do tipo imagem, eu deixo salvar, caso for de outro tipo, ele retorna um erro, ja que a imagem será null...
                    if (arq.ContentType.Contains("image"))
                    {
                        //crio um arquivo de memoria para a imagem
                        MemoryStream memoryStream = new MemoryStream();
                        //transfiro a imagem para essa memory
                        arq.CopyTo(memoryStream);
                        //depois deixo em array de bytes
                        byte[] imagem = memoryStream.ToArray();

                        //crio um jogo que sera adicionado com os campos
                        var jogoAdd = new Jogo(null, nome, imagem, descricao, categoria, preco, desconto, data_lancamento, numero_avaliacao, numero_estrelas, desenvolvedora);
                        //passo para o resultado o return do jogoADD (cadastrado com sucesso ou erro)
                        result = jogoModel.inserirJogo(jogoAdd);
                    }
                    else
                    {
                        result = "Imagem com erro"; 
                    }
                }
                
                //caso o resultado for sucesso, eu retorno para a pagina que lista, caso não eu retorno para mesma, porem com o erro na chamada,
                //precisa colocar esse erro, em um span que apareca, como erro ao cadastrar.

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
        //GET: tela da biblioteca
        [ServiceFilter(typeof(Autenticao))]
        public ActionResult Biblioteca()
        {
            Usuario u;
            string usuarioJson = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(usuarioJson))
            {
                u = JsonConvert.DeserializeObject<Usuario>(usuarioJson);

                //uma alternativa pra pegar diretamente o valor do id, ja que ele dava erro 
                string id = u.IdUsuario.ToString();

                return View(jogoModel.getBibliotecaUser(int.Parse(id)));
            }
            return View();
        }

        //GET: tela da ListaDesejo
        [ServiceFilter(typeof(Autenticao))]

        public ActionResult ListaDesejo()
        {
            Usuario u;
            string usuarioJson = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(usuarioJson))
            {
                u = JsonConvert.DeserializeObject<Usuario>(usuarioJson);

               

                return View(listaDesejoModel.pegarTodosDesejos((int) u.IdUsuario));
            }
            return View();
        }

        [ServiceFilter(typeof(Autenticao))]
        //aqui eu passo o id do jogo 
        public ActionResult Adicionar(int id)
        {

            string userJson = HttpContext.Session.GetString("user");


            Usuario user = JsonConvert.DeserializeObject<Usuario>(userJson);

            int idUser = (int)user.IdUsuario;

            listaDesejoModel.inserirDesejo(id, idUser);
            
            return View(nameof(ListaDesejo));
        }


        // GET: JogoController/Edit/5
        public ActionResult Comprar(int id)
        {
            return View();
        }

        // POST: JogoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(Autenticao))]
        public ActionResult Comprar(int idJogo, int idUsuario)
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
        public ActionResult Delete(int idJogo, IFormCollection collection)
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
