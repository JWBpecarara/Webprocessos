using WebProcessos.Data;
using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public class EtapaRepositorio : IEtapaRepositorio
    {
        private readonly BancoContext _bancoContext;
        public EtapaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public void adicionar(List<EtapaModel> ListaEtapa)
        {
            foreach (EtapaModel Etapa in ListaEtapa)
            {
                _bancoContext.Etapa.Add(Etapa);
                _bancoContext.SaveChanges();
            }
        }

        public List<EtapaModel> Buscartodos()
        {
            return _bancoContext.Etapa.Where(x => x.Excluido == false).ToList();
        }


        public void Excluir(List<EtapaModel> Etapa)
        {
            foreach (EtapaModel itens in Etapa)
            {
                itens.Excluido = true;

                _bancoContext.Etapa.Update(itens);
                _bancoContext.SaveChanges();
            }
        }

        public EtapaModel Excluir(EtapaModel Etapa)
        {
            throw new NotImplementedException();
        }

        public List<EtapaModel> GetByServicoID(int ServicoID)
        {
            return _bancoContext.Etapa.Where(x => x.ServicoID == ServicoID && x.Excluido == false).ToList();
        }

        public List<EtapaModel> GetEtapasVinculada(int ServicoID, int ServicovinID)
        {
            List<EtapaModel> query = (from s in _bancoContext.ServicoVinculado
                                        join e in _bancoContext.Etapa on s.ServicoID equals e.ServicoID
                                        join ep in _bancoContext.EtapaPasadaModel
                                            on new { EtapaID = e.Id, ServicoVinID = ServicovinID } equals new { EtapaID = ep.EtapaID, ep.ServicoVinID }
                                            into epJoin
                                      from ep in epJoin.DefaultIfEmpty()
                                      where s.ServicoID == ServicoID && e.Excluido == false && s.Excluido == false && s.Id == ServicovinID
                                      select new EtapaModel()
                                        {
                                            Id = e.Id,
                                            Nome = e.Nome,
                                            Descricao = e.Descricao,    
                                            status = ep.Status != null || ep.Status == "" ? ep.Status : "Sem status",
                                            EtapaPasadaID = ep.Id,
                                            ServicoVinID = s.Id,
                                            ServicoID = ServicoID,
                                        }).ToList();
            return query;
        }
    }
}
