using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public interface IVincularServico
    {
        ServicoVinculadoModel adicionar(ServicoVinculadoModel ServicoVinculado);
        List<ViewModelVincular> Buscartodos(int UsuarioId);
        void Excluir(ServicoVinculadoModel ServicoVinculado);
        ServicoVinculadoModel GetByID (int id);
        void Finalizar(int id);
    }
}
