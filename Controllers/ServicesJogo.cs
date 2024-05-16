using Microsoft.AspNetCore.Mvc;
using ProjetoPixelPlace.Entities;
using ProjetoPixelPlace.Models;

namespace ProjetoPixelPlace.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ServicesJogo : ControllerBase
    {
        JogoModel jogoModel = new JogoModel();

        [HttpGet]
        public List<Jogo> Listar()
        {
            return jogoModel.getAllJogos();
        }

        [HttpGet("{id}")]
        public Jogo GetJogo(int id)
        {   
            return jogoModel.getJogo(id);
        }
        
    }
}
