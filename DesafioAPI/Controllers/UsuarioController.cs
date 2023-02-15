using DesafioAPI.Context;
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
            using (var db = new UsuarioContext())
            {
               return db.Usuarios.ToList();
            }

        }
    }
}