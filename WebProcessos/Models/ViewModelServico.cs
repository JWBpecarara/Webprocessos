namespace WebProcessos.Models
{
    public class ViewModelServico
    {
        public ServicoModel Servico { get; set; }
        public EtapaModel Etapa { get; set; }
        public List<EtapaModel> ListaEtapa { get; set; }
        public List<ServicoModel> ListaServico { get; set; }
    }
}
