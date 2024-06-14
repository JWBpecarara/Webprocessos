using Microsoft.AspNetCore.Mvc;
using WebProcessos.Models;
using WebProcessos.Repositorio;
using WebProcessos.Uteis;

namespace WebProcessos.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoRepositorio _ServicoRepositorio;
        private readonly ISessao _sessao;



        public ServicoController(IServicoRepositorio servicoRepositorio, ISessao sessao)
        {
            _ServicoRepositorio = servicoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

            ViewModelServico servico = new ViewModelServico();
            servico.ListaServico = _ServicoRepositorio.Buscartodos(Usuario.Id);
            return View(servico);
        }

        public IActionResult ModalExcluir(int Id)
        {
            ServicoModel servico = new ServicoModel();
            servico.Id = Id;    
            return PartialView("ModalExcluir", servico);
        }

        public IActionResult ServicoModal()
        {
            ServicoModel servico = new ServicoModel();
            servico.Id = 0;
            return PartialView("addServicoModal", servico);
        }

        public IActionResult Editar(int Id)
        {
            ServicoModel Usuario = _ServicoRepositorio.GetByID(Id);
            return PartialView("addServicoModal", Usuario);
        }

        [HttpPost]
        public IActionResult Excluir(ServicoModel servico)
        {
            _ServicoRepositorio.Excluir(servico);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddNewService (ServicoModel Servico)
        {
            if (Servico.Id == 0) 
            {
                if (Servico == null)
                    return BadRequest();

                UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

                Servico.UsuarioId = Usuario.Id;

                _ServicoRepositorio.adicionar(Servico);

                return RedirectToAction("Index");
            }

            _ServicoRepositorio.Atualizar(Servico);

            return RedirectToAction("Index");
        }
    }
}
