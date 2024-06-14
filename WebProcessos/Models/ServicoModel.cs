using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProcessos.Models
{
    public class ServicoModel
    {
        [Key]
        public int Id { get; set; }
        public string NomeServico { get; set; }
        public string Descricao { get; set; }
        public bool Excluido { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual UsuarioModel Usuario { get; }
    }
}
