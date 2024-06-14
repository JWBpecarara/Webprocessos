using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public interface IServicoRepositorio
    {
        void adicionar(ServicoModel Servico);
        List<ServicoModel> Buscartodos(int UsuarioId);
        ServicoModel Excluir(ServicoModel Servico);
        ServicoModel GetByID(int Id);
        ServicoModel Atualizar(ServicoModel Servico);
    }
}
