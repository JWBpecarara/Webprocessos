using WebProcessos.Data;
using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel AddUsuario(UsuarioModel usuarioModel)
        {
            _bancoContext.Usuario.Add(usuarioModel);
            _bancoContext.SaveChanges();
            return usuarioModel;
        }

        public UsuarioModel GetLogin(string login)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Login == login); 
        }
    }
}
