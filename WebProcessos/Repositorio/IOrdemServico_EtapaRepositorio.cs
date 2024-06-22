using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
	public interface IOrdemServico_EtapaRepositorio
	{
		public void adicionar(List<OrdemServico_EtapaModel> OE, int OrdemServicoID);
		OrdemServico_EtapaModel GetByID(int id);
        void Atualizar(OrdemServico_EtapaModel etapa);
    }
}
