using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public interface IEtapaRepositorio
    {
        public List<EtapaModel> Buscartodos(int UsuarioID);
        public void adicionar(EtapaModel Etapa);
        void Excluir(int id);
    }
}