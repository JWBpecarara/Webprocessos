using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProcessos.Models
{
    public class EtapaPasadaModel
    {
        [Key]
        public int Id { get; set; }
        public int EtapaID { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public int ServicoVinID { get; set; }
        [ForeignKey("ServicoVinID")]
        public virtual ServicoVinculadoModel ServicoVinculadoModel { get; }
        public bool Excluido { get; set; }



    }
}
