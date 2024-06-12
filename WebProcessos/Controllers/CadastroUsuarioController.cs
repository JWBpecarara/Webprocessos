using Microsoft.AspNetCore.Mvc;
using WebProcessos.Models;
using WebProcessos.Repositorio;

namespace WebProcessos.Controllers
{
    public class CadastroUsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _UsuarioRepositorio;

        public CadastroUsuarioController(IUsuarioRepositorio UsuarioRepositorio)
        {
            _UsuarioRepositorio = UsuarioRepositorio;

        }

        public IActionResult CadastroUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUsuario (CadastroUsuarioModel view)
        {
            try
            {
                if (view == null)
                    return BadRequest();

                if (view.SenhaConf == view.Usuario.Senha)
                {
                    _UsuarioRepositorio.AddUsuario(view.Usuario);
                    return RedirectToAction("Login", "Login");
                }            
                return BadRequest("Asenha devem ser iguais");
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
