using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProcessos.Models
{
    public class OrdemServicoModel
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public float Preco { get; set; }
        public bool Excluido { get; set; }
        public int ServicoID { get; set; }
        [ForeignKey("ServicoID")]
        public virtual ServicoModel Servico { get; }
        public int ClienteID { get; set; }
        [ForeignKey("ClienteID")]
        public virtual ClienteModel Cliente { get; }
    }
}
