using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProcessos.Models
{
    public class OrdemServico_EtapaModel
    {
        [Key]
        public int Id { get; set; }
        public int OrdemServicoID { get; set; }
        [ForeignKey("OrdemServicoID")]
        public virtual OrdemServicoModel OrdemServico { get; }
        public int EtapaID { get; set; }
        [ForeignKey("EtapaID")]
        public virtual EtapaModel Etapa { get; }
        public string Status { get; set; }
        public DateTime DataFinalizado { get; set; }

    }
}
