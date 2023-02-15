using DesafioAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUsuario")]
        public IEnumerable<Usuario> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Usuario
            {
                Id = Random.Shared.Next(0, 55),
                Email = "cliente@email.com",
                Nome = "Nome usuario"
            }).ToArray();
        }
    }
}