using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public interface IEtapaPasadaRepositorio
    {
        void adicionar(EtapaPasadaModel Servico);
        void Atualizar(EtapaPasadaModel Servico);
        EtapaPasadaModel GetByID(int Id);

    }
}
