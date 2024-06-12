namespace WebProcessos.Models
{
    public class ViewModelGerenciamento
    {
        public  ServicoModel Servico { get; set; }
        public ServicoVinculadoModel ServicoVinculado { get; set; }
        public ClienteModel Cliente { get; set; }
        public List<EtapaModel> ListEtapa { get; set; }
    }
}
