using System.ComponentModel.DataAnnotations;

namespace WebProcessos.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public bool Excluido { get; set; }

        public bool Senhavalida(string senha) 
        {
            return Senha == senha;
        }

    }
}
