using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public interface IEtapaRepositorio
    {
        List<EtapaModel> Buscartodos();
        void adicionar(List<EtapaModel> Etapa);
        List<EtapaModel> GetByServicoID(int ServicoID);
        List<EtapaModel> GetEtapasVinculada(int ServicoID, int ServicovinID);
        void  Excluir(List<EtapaModel> Etapa);

    }
}
