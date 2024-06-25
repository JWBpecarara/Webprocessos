using WebProcessos.Data;
using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
	public class OrdemServicoRepositorio : IOrdemServicoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public OrdemServicoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

		public int adicionar(OrdemServicoModel os)
		{
             _bancoContext.OrdemServico.Add(os);
             _bancoContext.SaveChanges();
             return os.Id;
        }

        public void Excluir(int id)
        {
            OrdemServicoModel os = GetByID(id);

            if (os == null) throw new System.Exception("Erro ao Excluir cliente");

            os.Excluido = true;

            _bancoContext.OrdemServico.Update(os);
            _bancoContext.SaveChanges();

        }

        public void Finalizar(int id)
        {

            OrdemServicoModel os = GetByID(id);

            if (os == null) throw new System.Exception("Erro na atulização do cliente");

            os.Status = "Finalizado";


            _bancoContext.OrdemServico.Update(os);
            _bancoContext.SaveChanges();
        }

        public OrdemServicoModel GetByID(int Id)
        {
            return _bancoContext.OrdemServico.FirstOrDefault(x => x.Id == Id && x.Excluido == false);
        }



        public ViewModelGerenciamento GetviewViewGerenciamento(int IDOrdemServico)
        {
            ViewModelGerenciamento ViewModelGerenciamento = (from os in _bancoContext.OrdemServico
                                                            join oe in _bancoContext.OrdemServico_Etapa on os.Id equals oe.OrdemServicoID
                                                            join s in _bancoContext.Servico on os.ServicoID equals s.Id
                                                            join c in _bancoContext.Cliente on os.ClienteID equals c.Id
                                                            join e in _bancoContext.Etapa on oe.EtapaID equals e.Id
                                                            where os.Id == IDOrdemServico && os.Excluido == false
                                                            select new ViewModelGerenciamento
                                                            {
                                                                ServicoNome = s.NomeServico,
                                                                ClienteNome = c.Nome,
                                                                CPF = c.CPF,
                                                                Telefone = c.Telefone,
                                                                Preco = os.Preco,
                                                                OsId = os.Id,
                                                                Status = os.Status
                                                            }).First();

            List<EtapaGerenciamento> ListEtapas = (from os in _bancoContext.OrdemServico
                                                  join oe in _bancoContext.OrdemServico_Etapa on os.Id equals oe.OrdemServicoID
                                                  join e in _bancoContext.Etapa on oe.EtapaID equals e.Id
                                                  where os.Id == IDOrdemServico && os.Excluido == false
                                                  select new EtapaGerenciamento
                                                  {
                                                    OrdemServico_EtapaID = oe.Id,
                                                    EtapaNome = e.Nome,
                                                    Status = oe.Status
                                                  }).ToList();

            ViewModelGerenciamento.ListEtapas = ListEtapas;

            return ViewModelGerenciamento;
        }

        public List<ViewModelOS> OsLIST(int UsuarioId)
        {
            List<ViewModelOS> ViewModelOS = (from os in _bancoContext.OrdemServico
                                            join c in _bancoContext.Cliente on os.ClienteID equals c.Id
                                            join s in _bancoContext.Servico on os.ServicoID equals s.Id
                                            where  os.Excluido == false && c.UsuarioId == UsuarioId && s.UsuarioId == UsuarioId 
                                             select new ViewModelOS()
                                            {
                                                OsId = os.Id,
                                                NomeCliente = c.Nome,
                                                CPF = c.CPF,
                                                Telefone = c.Telefone,
                                                NomeServico  = s.NomeServico,
                                                Preco = os.Preco,
                                                Status = os.Status
                                             }).ToList();

            return ViewModelOS;
        }
    }
}
