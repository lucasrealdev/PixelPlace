using Microsoft.AspNetCore.Mvc;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ServicesUsuario : ControllerBase
    {
        private readonly UsuarioModel model;

        public ServicesUsuario()
        {
            model = new UsuarioModel();
        }

        [HttpPost]
        public ActionResult InserirUsuario([FromBody] Usuario usuario)
        {
            var msg = model.inserirUsuario(new Usuario(null, usuario.NomeUsuario, usuario.Email, usuario.Senha, null, ""));

            if (msg != "Usuário cadastrado com sucesso")
            {
                return BadRequest(new { mensagem = msg });
            }
            return Ok(new { mensagem = msg });
        }

        [HttpGet("Login")]
        public ActionResult ValidarUsuario([FromQuery] string email, [FromQuery] string senha)
        {
            var u = model.ValidaUser(email, senha);

            if (u == null)
            {
                return BadRequest(new { mensagem = "Usuário não foi encontrado" });
            }
            return Ok(new { usuario = u });
        }
    }
}
