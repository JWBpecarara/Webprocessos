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


    }
}
