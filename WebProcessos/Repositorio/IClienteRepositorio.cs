using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public interface IClienteRepositorio
    {
        List<ClienteModel> Buscartodos(int UsuarioId);
        ClienteModel adicionar(ClienteModel cliente);
        ClienteModel GetByID(int Id);
        ClienteModel GetByServicoVinculado(int Id);
        ClienteModel Atualizar(ClienteModel cliente);
        ClienteModel Excluir(ClienteModel cliente);

    }
}
