using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProcessos.Models
{
    public class ServicoVinculadoModel
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public int ServicoID { get; set; }
        [ForeignKey("ServicoID")]
        public virtual ServicoModel ServicoModel { get; }
        public int ClienteID { get; set; }
        [ForeignKey("ClienteID")]
        public bool Excluido { get; set; }
        public virtual ClienteModel ClienteModel { get;}
    }
}
