using DesafioAPI.Context;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost(Name = "AddUsuario")]
        public bool Add()
        {
            using (var db = new UsuarioContext())
            {
                Usuario usuario = new Usuario();
                usuario.Nome = "Teste usuario";
                usuario.Email = "cliente@email.com";
                usuario.Senha = "ad21sa3d21sad54sa6d54a6s54d";

                db.Usuarios.Add(usuario);
                db.SaveChanges();

                return true;
            }

        }
    }
}