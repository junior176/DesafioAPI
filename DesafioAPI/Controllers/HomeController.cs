using DesafioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("anonimoTeste")]
        [AllowAnonymous]
        public string GetAnonymous() => "Anônimo";

        [HttpGet]
        [Route("autenticadoTeste")]
        [Authorize]
        public string GetAuthenticated() {
            
            return $"Autenticado - {User?.Identity?.Name} ";
        } 
    }
}
