using Microsoft.EntityFrameworkCore;
using WebProcessos.Data;
using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
	public class OrdemServico_EtapaRepositorio : IOrdemServico_EtapaRepositorio
	{
		private readonly BancoContext _bancoContext;
		public OrdemServico_EtapaRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public void adicionar(List<OrdemServico_EtapaModel> OE, int OrdemServicoID)
		{
			foreach (OrdemServico_EtapaModel Etapa in OE) {
				OrdemServico_EtapaModel EtapaNova = new OrdemServico_EtapaModel();

				EtapaNova.EtapaID = Etapa.Id;
				EtapaNova.OrdemServicoID = OrdemServicoID;
				EtapaNova.Status = "Aguardando";	
				_bancoContext.OrdemServico_Etapa.Add(EtapaNova);
			}
			_bancoContext.SaveChanges();
		}

        public OrdemServico_EtapaModel GetByID(int id)
        {
			return _bancoContext.OrdemServico_Etapa.FirstOrDefault(x => x.Id == id);	
		}

		public void Atualizar(OrdemServico_EtapaModel Etapa)
		{
			OrdemServico_EtapaModel EtapOs = GetByID(Etapa.Id);

			if (EtapOs == null) throw new System.Exception("Erro na atulização do cliente");

			EtapOs.Status = Etapa.Status;

			_bancoContext.OrdemServico_Etapa.Update(EtapOs);
			_bancoContext.SaveChanges();

		}

    }
}
