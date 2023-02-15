using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioAPI.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
