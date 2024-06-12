using Microsoft.AspNetCore.Mvc;
using WebProcessos.Models;
using WebProcessos.Repositorio;
using WebProcessos.Uteis;

namespace WebProcessos.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IEtapaRepositorio _EtapaRepositorio;
        private readonly IServicoRepositorio _ServicoRepositorio;
        private readonly ISessao _sessao;



        private static List<EtapaModel> _listaEtapa = new List<EtapaModel>();
        public ServicoController(IEtapaRepositorio etapaRepositorio, IServicoRepositorio servicoRepositorio, ISessao sessao)
        {
            _EtapaRepositorio = etapaRepositorio;
            _ServicoRepositorio = servicoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

            _listaEtapa.Clear();
            ViewModelServico servico = new ViewModelServico();
            servico.ListaServico = _ServicoRepositorio.Buscartodos(Usuario.Id);
            return View(servico);
        }

        public IActionResult ServicoCadastro()
        {
            var viewModel = new ViewModelServico
            {
                ListaEtapa = _listaEtapa
            };
            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            ViewModelServico ViewModel = new ViewModelServico();
            ViewModel.Servico = _ServicoRepositorio.GetByID(id);
            ViewModel.ListaEtapa = _EtapaRepositorio.GetByServicoID(id);
            _listaEtapa = _EtapaRepositorio.GetByServicoID(id);
            return View("ServicoCadastro", ViewModel); ;
        }

        [HttpPost]
        public IActionResult AddListaEtapa(string nome, string descricao)
        {
            var novaEtapa = new EtapaModel
            {
                Nome = nome ?? "",
                Descricao = descricao ?? ""
            };
            if (novaEtapa.Nome != "" && novaEtapa.Descricao != "")
            {
                novaEtapa.Id = _listaEtapa.Count > 0 ? _listaEtapa.Max(e => e.Id) + 1 : 1;
                _listaEtapa.Add(novaEtapa);
                return Json(new { id = novaEtapa.Id, nome = novaEtapa.Nome, descricao = novaEtapa.Descricao });
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult RemoveEtapa(int id)
        {
            var etapa = _listaEtapa.FirstOrDefault(e => e.Id == id);
            if (etapa != null)
            {
                _listaEtapa.Remove(etapa);
            }
            return Ok();
        }

        public IActionResult ModalExcluir(int Id)
        {
            ServicoModel servico = new ServicoModel();
            servico.Id = Id;    
            return PartialView("ModalExcluir", servico);
        }

        [HttpPost]
        public IActionResult Excluir(ServicoModel servico)
        {
            _ServicoRepositorio.Excluir(servico);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddNewService (ViewModelServico viewModel)
        {
            if (viewModel.Servico.Id == 0)
            {
                if (viewModel == null)
                    return BadRequest();

                UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

                viewModel.Servico.UsuarioId = Usuario.Id;

                int ServicoID = _ServicoRepositorio.adicionar(viewModel.Servico);

                List<EtapaModel> newList = new List<EtapaModel>();

                foreach (EtapaModel NewEtapa in _listaEtapa)
                {
                    NewEtapa.ServicoID = ServicoID;
                    NewEtapa.Id = 0;
                    newList.Add(NewEtapa);
                }
                _EtapaRepositorio.adicionar(newList);
                _listaEtapa.Clear();
                return RedirectToAction("ServicoCadastro");
            }
            _ServicoRepositorio.Atualizar(viewModel.Servico);

            List<EtapaModel> ListaBanco = new List<EtapaModel>();
            List<EtapaModel> NovaLista = new List<EtapaModel>();

            ListaBanco = _EtapaRepositorio.GetByServicoID(viewModel.Servico.Id);

            var removedItems = ListaBanco.Where(originalItem => !_listaEtapa.Any(modifiedItem => modifiedItem.Id == originalItem.Id)).ToList();
            var addedItems = _listaEtapa.Where(modifiedItem => !ListaBanco.Any(originalItem => originalItem.Id == modifiedItem.Id)).ToList();


            foreach (EtapaModel NewEtapa in addedItems)
            {
                NewEtapa.ServicoID = viewModel.Servico.Id;
                NewEtapa.Id = 0;
                NovaLista.Add(NewEtapa);
            }


            _EtapaRepositorio.adicionar(NovaLista);
            _EtapaRepositorio.Excluir(removedItems);

            _listaEtapa.Clear();
            return RedirectToAction("ServicoCadastro");

        }
    }
}
