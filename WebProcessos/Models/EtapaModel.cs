using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProcessos.Models
{
    public class EtapaModel
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual UsuarioModel UsuarioModel { get; }
        public bool Excluido { get; set; }

    }
}
