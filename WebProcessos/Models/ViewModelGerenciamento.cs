namespace WebProcessos.Models
{
    public class ViewModelGerenciamento
    {
        public List<EtapaGerenciamento> ListEtapas { get; set; }
        public int OsId { get; set; }
        public string ServicoNome { get; set; }
        public string ClienteNome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public float Preco { get; set; }
    }
}
