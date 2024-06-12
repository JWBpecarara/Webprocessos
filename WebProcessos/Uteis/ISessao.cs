using WebProcessos.Models;

namespace WebProcessos.Uteis
{
    public interface ISessao
    {
        void CriarSesaoDeUsuario(UsuarioModel usuario);
        void RemoverSessaoDeUsuario();
        UsuarioModel BuscarSessaoDeUsuario();
    }
}
