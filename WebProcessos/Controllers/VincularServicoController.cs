using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebProcessos.Models;
using WebProcessos.Repositorio;
using WebProcessos.Uteis;

namespace WebProcessos.Controllers
{
    public class VincularServicoController : Controller
    {
        private static List<EtapaModel> _listaEtapa = new List<EtapaModel>();

        private readonly IEtapaRepositorio _EtapaRepositorio;
        private readonly IEtapaPasadaRepositorio _EtapaPasadaRepositorio;
        private readonly IServicoRepositorio _ServicoRepositorio;
        private readonly IClienteRepositorio _ClienteRepositorio;
        private readonly IVincularServico _VincularServico;
        private readonly ISessao _sessao;


        public VincularServicoController(IEtapaRepositorio etapaRepositorio, IServicoRepositorio servicoRepositorio, IClienteRepositorio ClienteRepositorio, IVincularServico vincularServico, IEtapaPasadaRepositorio EtapaPasadaRepositorio, ISessao sessao)
        {
            _EtapaRepositorio = etapaRepositorio;
            _ServicoRepositorio = servicoRepositorio;
            _ClienteRepositorio = ClienteRepositorio;
            _VincularServico = vincularServico;
            _EtapaPasadaRepositorio = EtapaPasadaRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            _listaEtapa.Clear();

            UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();


            ViewModelModalVincular viewModel = new ViewModelModalVincular();
            viewModel.ListaVinculados = _VincularServico.Buscartodos(Usuario.Id);
            return View(viewModel);
        }

        public IActionResult Gerenciamento(int id)       
        {
            ViewModelGerenciamento ViewModel = new ViewModelGerenciamento();

            if (_listaEtapa.Count == 0)
            {
                ViewModel.ServicoVinculado = _VincularServico.GetByID(id);
                ViewModel.Servico = _ServicoRepositorio.GetByID(ViewModel.ServicoVinculado.ServicoID);
                ViewModel.Cliente = _ClienteRepositorio.GetByID(ViewModel.ServicoVinculado.ClienteID);
                _listaEtapa = _EtapaRepositorio.GetEtapasVinculada(ViewModel.ServicoVinculado.ServicoID, ViewModel.ServicoVinculado.Id);
                ViewModel.ListEtapa = _listaEtapa;

                return View("Gerenciamento", ViewModel);
            }
            EtapaModel etapa = _listaEtapa.FirstOrDefault();
            _listaEtapa.Clear();
            ViewModel.Servico = _ServicoRepositorio.GetByID(etapa.ServicoID);
            ViewModel.Cliente = _ClienteRepositorio.GetByServicoVinculado(etapa.ServicoVinID);
            _listaEtapa = _EtapaRepositorio.GetEtapasVinculada(etapa.ServicoID, etapa.ServicoVinID);
            ViewModel.ListEtapa = _listaEtapa;

            return View("Gerenciamento", ViewModel); 
        }

        [HttpPost]
        public IActionResult Atualizar(ViewModelGerenciamento view)
        {
            EtapaModel etapa = _listaEtapa.FirstOrDefault();
            _VincularServico.Finalizar(etapa.ServicoVinID);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Vigente(int Id)
        {
            EtapaPasadaModel EtapaPasada = new EtapaPasadaModel();
            EtapaModel etapa = new EtapaModel();
            etapa = _listaEtapa.FirstOrDefault(x => x.Id == Id );
            var ServicoVinID = etapa.ServicoVinID;
            EtapaModel etapaVigente = _listaEtapa.FirstOrDefault(x => x.status == "vigente");

            if (etapaVigente == null)
            {
                if (etapa.EtapaPasadaID == null)
                {

                    EtapaPasada.EtapaID = etapa.Id;
                    EtapaPasada.Descricao = etapa.Descricao;
                    EtapaPasada.Status = "vigente";
                    EtapaPasada.ServicoVinID = etapa.ServicoVinID;

                    _EtapaPasadaRepositorio.adicionar(EtapaPasada);

                    return RedirectToAction("Gerenciamento");
                }

                EtapaPasada.Status = "vigente";
                EtapaPasada.Id = etapa.EtapaPasadaID ?? 0;

                _EtapaPasadaRepositorio.Atualizar(EtapaPasada);

                return RedirectToAction("Gerenciamento");
            }
            return RedirectToAction("Gerenciamento");

        }

        [HttpPost]
        public IActionResult Desvincular(int Id)
        {
            EtapaModel etapa = new EtapaModel();
            etapa = _listaEtapa.FirstOrDefault(x => x.Id == Id);

            if (etapa.EtapaPasadaID != null)
            {
                EtapaPasadaModel EtapaPasada = new EtapaPasadaModel();

                EtapaPasada.Status = "";
                EtapaPasada.Id = etapa.EtapaPasadaID ?? 0;

                _EtapaPasadaRepositorio.Atualizar(EtapaPasada);

                return RedirectToAction("Gerenciamento");
            }

            return RedirectToAction("Gerenciamento");
        }

        [HttpPost]
        public IActionResult Finalizar(int Id)
        {
            EtapaModel etapa = new EtapaModel();
            etapa = _listaEtapa.FirstOrDefault(x => x.Id == Id);

            if (etapa.EtapaPasadaID != null)
            {
                EtapaPasadaModel EtapaPasada = new EtapaPasadaModel();

                EtapaPasada.Status = "finalizado";
                EtapaPasada.Id = etapa.EtapaPasadaID ?? 0;

                _EtapaPasadaRepositorio.Atualizar(EtapaPasada);

                return RedirectToAction("Gerenciamento");
            }

            return RedirectToAction("Gerenciamento");
        }


        public IActionResult ModalExcluir(int Id)
        {
            ServicoVinculadoModel servicoVin = new ServicoVinculadoModel();
            servicoVin.Id = Id;
            return PartialView("ModalExcluir", servicoVin);
        }

        [HttpPost]
        public IActionResult Excluir(ServicoVinculadoModel vinculado)
        {
            _VincularServico.Excluir(vinculado);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Vincular(int servicoId, int usuarioId)
        {

            ServicoVinculadoModel servicoVinculado= new ServicoVinculadoModel();
            var Servico = _ServicoRepositorio.GetByID(servicoId);

            servicoVinculado.Status = "Aguardando";
            servicoVinculado.ServicoID = servicoId;
            servicoVinculado.ClienteID = usuarioId;
            servicoVinculado.Descricao = Servico.Descricao;

            _VincularServico.adicionar(servicoVinculado);
            return RedirectToAction("Index");
        }

        public IActionResult ModalVincular()
        {
            UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

            ViewModelModalVincular ViewModelModal = new ViewModelModalVincular();
            ViewModelModal.ListaClientes = _ClienteRepositorio.Buscartodos(Usuario.Id);
            ViewModelModal.ListaServico = _ServicoRepositorio.Buscartodos(Usuario.Id);

            return PartialView("ModalVincular", ViewModelModal);
        }
      
    }
}

