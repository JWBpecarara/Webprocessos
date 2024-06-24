using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace WebProcessos.Models
{
    public class ViewModelOrdemServico
    {
        public List<ServicoModel> ListServico { get; set; }
        public List<ClienteModel> ListCliente { get; set; }
        public List<EtapaModel> ListEtapa { get; set; }
        public List<OrdemServico_EtapaModel> EtapasOS { get; set; }
        public int Servico { get; set; }
        public int Cliente { get; set; }
        public string Observacao { get; set; }
        public float Preco => float.Parse(Regex.Replace(this.PrecoRaw, "[R $]", string.Empty));

        [NotMapped]
        public string PrecoRaw { get; set; }
    }
}
