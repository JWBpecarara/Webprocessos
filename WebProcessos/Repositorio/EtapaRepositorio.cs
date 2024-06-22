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


        public void Excluir(int id)
        {
            EtapaModel etapa = GetByID(id);

            if (etapa == null) throw new System.Exception("Erro ao Excluir cliente");

            etapa.Excluido = true;

            _bancoContext.Etapa.Update(etapa);
            _bancoContext.SaveChanges();

        }


        public EtapaModel GetByID(int Id)
        {
            return _bancoContext.Etapa.FirstOrDefault(x => x.Id == Id && x.Excluido == false);
        }


        public void adicionar(EtapaModel Etapa)
        {
            _bancoContext.Etapa.Add(Etapa);
            _bancoContext.SaveChanges();
        }

        public List<EtapaModel> Buscartodos(int UsuarioID)
        {
            return _bancoContext.Etapa.Where(x => x.Excluido == false && x.UsuarioID == UsuarioID).ToList();
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
