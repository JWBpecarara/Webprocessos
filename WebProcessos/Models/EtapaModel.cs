using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProcessos.Models
{
    public class EtapaModel
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int ServicoID { get; set; }
        [ForeignKey("ServicoID")]
        public virtual ServicoModel ServicoModel { get; }
        public bool Excluido { get; set; }
        [NotMapped]
        public string? status { get; set; }
        [NotMapped]
        public int  ServicoVinID { get; set; }
        [NotMapped]
        public int? EtapaPasadaID { get; set; }


    }
}
