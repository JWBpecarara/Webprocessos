using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public interface IUsuarioRepositorio
    {

        UsuarioModel GetLogin(string login);
        UsuarioModel AddUsuario(UsuarioModel usuarioModel);
    }
}
