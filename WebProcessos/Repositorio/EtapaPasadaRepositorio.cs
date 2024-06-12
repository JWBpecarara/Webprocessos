using WebProcessos.Data;
using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public class EtapaPasadaRepositorio : IEtapaPasadaRepositorio
    {
        private readonly BancoContext _bancoContext;
        public EtapaPasadaRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public void adicionar(EtapaPasadaModel EtapaPasada)
        {
            _bancoContext.EtapaPasadaModel.Add(EtapaPasada);
            _bancoContext.SaveChanges();
        }

        public void Atualizar(EtapaPasadaModel EtapaPasada)
        {
            EtapaPasadaModel EtapaPasadaModelDB = GetByID(EtapaPasada.Id);

            if (EtapaPasadaModelDB == null) throw new System.Exception("Erro na atulização do cliente");

            EtapaPasadaModelDB.Status = EtapaPasada.Status;

            _bancoContext.EtapaPasadaModel.Update(EtapaPasadaModelDB);
            _bancoContext.SaveChanges();

        }

        public EtapaPasadaModel GetByID(int Id)
        {
            return _bancoContext.EtapaPasadaModel.FirstOrDefault(x => x.Id == Id && x.Excluido == false);
        }
    }
}
