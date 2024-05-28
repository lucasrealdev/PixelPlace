using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ServicesUsuario : ControllerBase
    {
        UsuarioModel model = new UsuarioModel();

        [HttpPost]
        public ActionResult inserirUser([FromBody] Usuario usuario)
        {
            var msg = model.inserirUsuario(new Usuario(null, usuario.NomeUsuario, usuario.Email, usuario.Senha, null, ""));


            if (msg != "Usuário cadastrado com sucesso")
            {
                return BadRequest(new { mensagem = msg });
            }
            return Ok(new { mensagem = msg });
        }

        [HttpGet("{id}")]
        public ActionResult getUser(int id)
        {
            var u = model.getUser(id);


            if (u == null)
            {
                return BadRequest(new { mensagem = "Usuario Não foi encontrado" });
            }
            return Ok(new { u});
        }

        [HttpPost]
        public ActionResult ValidarUser([FromBody] string email, string senha)
        {
            var u = model.ValidaUser(email,senha);

            if (u == null)
            {
                return BadRequest(new { mensagem = "Usuario Não foi encontrado" });
            }
            return Ok(new { u });
        }
    }
}
