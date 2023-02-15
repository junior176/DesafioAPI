using DesafioAPI.Context;
using DesafioAPI.Models;
using DesafioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }



        [HttpGet]
        [Route("getTokenTeste")]
        public async Task<IActionResult> getTokenTeste()
        {
            
            using (var db = new UsuarioContext())
            {
                Usuario usuario = db.Usuarios.Where(e => e.Id == 2).First();
                return Ok(TokenService.GetToken(usuario));

            }

            return Ok();

        }
        
        /*
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

        }*/
    }
}