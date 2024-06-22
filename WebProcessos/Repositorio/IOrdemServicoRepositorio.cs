using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
	public interface IOrdemServicoRepositorio
	{
		public int adicionar(OrdemServicoModel os);
		public List<ViewModelOS> OsLIST(int UsuarioId);
		public ViewModelGerenciamento GetviewViewGerenciamento(int IDOrdemServico);
        void Excluir(int id);
        void Finalizar(int id);
    }
}
