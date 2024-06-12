namespace WebProcessos.Models
{
    public class ViewModelModalVincular
    {
        public List<ClienteModel> ListaClientes { get; set; }
        public List<ServicoModel> ListaServico { get; set; }
        public List<ViewModelVincular> ListaVinculados { get; set; }
        public ServicoVinculadoModel ServicoVinculado { get; set; }

    }
}
