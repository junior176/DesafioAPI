using DesafioAPI.Context;
using DesafioAPI.Models;
using DesafioAPI.Services;
using DesafioAPI.Utils;
using Microsoft.AspNetCore.Authorization;
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


        [HttpPost]
        [Route("Logar")]
        public IActionResult Logar([FromForm] string Email, [FromForm] string Senha)
        {

            using (var db = new UsuarioContext())
            {
                bool existe = db.Usuarios.Where(u => u.Email == Email).Any();

                if (!existe)
                {
                    return NotFound();
                }

                Usuario usuario = db.Usuarios.Where(e => e.Email == Email).First();

                if (usuario.Senha != Cryptography.MD5Encript(Senha)) return Unauthorized();
                if (usuario.Ativo) 
                {
                    Random generator = new Random();
                    int codigo = generator.Next(100000, 999999);

                    usuario.CodigoLogin = codigo.ToString();
                    db.SaveChanges();

                    EmailService.EnviarEmailLogin(codigo.ToString());
                    return Ok(TokenService.GetToken(usuario));
                }
                else
                {
                    return  StatusCode(StatusCodes.Status403Forbidden);
                }
                

            }

        }

        [HttpPost]
        [Route("ValidarCodigo")]
        [Authorize]
        public IActionResult ValidarCodigo([FromForm] string Email, [FromForm] string codigo)
        {

            using (var db = new UsuarioContext())
            {
                bool existe = db.Usuarios.Where(u => u.Email == Email).Any();

                if (!existe)
                {
                    return NotFound();
                }

                Usuario usuario = db.Usuarios.Where(e => e.Email == Email).First();

                if (usuario.CodigoLogin != codigo) 
                { 
                    return Unauthorized(); 
                }
                else
                {
                    usuario.CodigoLogin = "";
                    db.SaveChanges();
                    return Ok(usuario.Nome);
                }

            }

        }

        [HttpGet]
        [Route("getTokenTeste")]
        public async Task<IActionResult> getTokenTeste()
        {
            
            using (var db = new UsuarioContext())
            {
                Usuario usuario = db.Usuarios.Where(e => e.Id == 2).First();
                return usuario.Ativo ? Ok(TokenService.GetToken(usuario)) : StatusCode(StatusCodes.Status403Forbidden);

            }

        }
        

    }
}