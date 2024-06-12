using Microsoft.AspNetCore.Mvc;
using WebProcessos.Models;
using WebProcessos.Repositorio;
using WebProcessos.Uteis;

namespace WebProcessos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;
        private readonly ISessao _sessao;



        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao )
        {
            _UsuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Login()
        {
            if(_sessao.BuscarSessaoDeUsuario() != null)  return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {

            _sessao.RemoverSessaoDeUsuario();

            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _UsuarioRepositorio.GetLogin(loginModel.Login);

                    if (usuario.Senhavalida(loginModel.Senha))
                    {
                        _sessao.CriarSesaoDeUsuario(usuario);
                        return RedirectToAction("Index", "Home");
                    }

                    return BadRequest();

                }
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
