using Newtonsoft.Json;
using WebProcessos.Models;

namespace WebProcessos.Uteis
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _contextAccessor = httpContext;
        }

        public UsuarioModel BuscarSessaoDeUsuario()
        {
            string sessao = _contextAccessor.HttpContext.Session.GetString("sessaoUsuario");
            
            if (string.IsNullOrEmpty(sessao)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessao); 
        }

        public void CriarSesaoDeUsuario(UsuarioModel usuario)
        {
            string JsonUsuario = JsonConvert.SerializeObject(usuario);

            _contextAccessor.HttpContext.Session.SetString("sessaoUsuario", JsonUsuario);
        }

        public void RemoverSessaoDeUsuario()
        {
            _contextAccessor.HttpContext.Session.Remove("sessaoUsuario");
        }
    }
}
