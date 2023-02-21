using DesafioAPI.Context;
using DesafioAPI.Models;
using DesafioAPI.Services;
using DesafioAPI.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromForm] string Nome, [FromForm] string Email, [FromForm] string Senha)
        {
            using (var db = new UsuarioContext())
            {

                bool existe = db.Usuarios.Where(u => u.Email == Email).Any();

                if(existe) 
                {
                    return Conflict();
                }

                Usuario usuario = new Usuario();
                usuario.Nome = Nome;
                usuario.Email = Email;
                usuario.Senha = Cryptography.MD5Encript(Senha);

                db.Usuarios.Add(usuario);
                db.SaveChanges();

                EmailService.EnviarEmailCadastro(Nome, Email);

                return Ok();
            }

        }

        [HttpPost]
        [Route("ConfirmarEmail")]
        public IActionResult ConfirmarEmail([FromForm] string EmailBase64)
        {

            string email = Encoding.UTF8.GetString(Convert.FromBase64String(EmailBase64));

            using (var db = new UsuarioContext())
            {

                bool existe = db.Usuarios.Where(u => u.Email == email).Any();

                if (!existe)
                {
                    return NotFound();
                }

                Usuario usuario = db.Usuarios.Where(u => u.Email == email).First();
                usuario.Ativo = true;
                db.SaveChanges();

                return Ok();
            }

        }


        [HttpPost]
        [Route("RecuperarSenha")]
        public IActionResult RecuperarSenha([FromForm] string Email)
        {

            using (var db = new UsuarioContext())
            {
                bool existe = db.Usuarios.Where(u => u.Email == Email).Any();

                if (!existe)
                {
                    return NotFound();
                }

                Usuario usuario = db.Usuarios.Where(e => e.Email == Email).First();

                string codRecuperacao = Cryptography.MD5Encript(PrivateKeyJWT.Key + Email);

                usuario.CodigoRecuperarSenha = codRecuperacao;
                db.SaveChanges();
                EmailService.EnviarEmailRecuperarSenha(Email, codRecuperacao);
                return Ok();

            }

        }

        [HttpPost]
        [Route("getEmailRecuperado")]
        public IActionResult getEmailRecuperado([FromForm] string Codigo)
        {

            using (var db = new UsuarioContext())
            {
                bool existe = db.Usuarios.Where(u => u.CodigoRecuperarSenha == Codigo).Any();

                if (!existe)
                {
                    return NotFound();
                }

                Usuario usuario = db.Usuarios.Where(e => e.CodigoRecuperarSenha == Codigo).First();

                return Ok(usuario.Email);

            }

        }

        [HttpPost]
        [Route("AlterarSenha")]
        public IActionResult AlterarSenha([FromForm] string Email, [FromForm] string Senha)
        {

            using (var db = new UsuarioContext())
            {
                bool existe = db.Usuarios.Where(u => u.Email == Email).Any();

                if (!existe)
                {
                    return NotFound();
                }

                Usuario usuario = db.Usuarios.Where(e => e.Email == Email).First();

                usuario.Senha = Cryptography.MD5Encript(Senha);
                usuario.CodigoRecuperarSenha = "";
                db.SaveChanges();
                return Ok();

            }

        }
    }
}