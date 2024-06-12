using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebProcessos.Models;

namespace WebProcessos.ViwesComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            string sessao = HttpContext.Session.GetString("sessaoUsuario");

            if (string.IsNullOrEmpty(sessao)) return null; 

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessao);
            
            return View(usuario);
        }
    }
}
