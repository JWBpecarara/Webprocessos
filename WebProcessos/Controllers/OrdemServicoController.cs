using Microsoft.AspNetCore.Mvc;
using WebProcessos.Models;
using WebProcessos.Repositorio;
using WebProcessos.Uteis;

namespace WebProcessos.Controllers
{
    public class OrdemServicoController : Controller
    {
        private readonly IServicoRepositorio _ServicoRepositorio;
        private readonly IClienteRepositorio _ClienteRepositorio;
        private readonly IEtapaRepositorio _EtapaRepositorio;
        private readonly IOrdemServico_EtapaRepositorio _OrdemServico_EtapaRepositorio;
        private readonly IOrdemServicoRepositorio _OrdemServico;



        private readonly ISessao _sessao;

        private static List<EtapaModel> _listaEtapaCadastrada = new List<EtapaModel>();
        private static int _idpagina;




        public OrdemServicoController(IServicoRepositorio servicoRepositorio, ISessao sessao, IClienteRepositorio ClienteRepositorio, IEtapaRepositorio EtapaRepositorio, IOrdemServico_EtapaRepositorio OrdemServico_EtapaRepositorio, IOrdemServicoRepositorio OrdemServicoRepositorio)
        {
            _ServicoRepositorio = servicoRepositorio;
            _sessao = sessao;
            _ClienteRepositorio = ClienteRepositorio;
            _EtapaRepositorio = EtapaRepositorio;
            _OrdemServico_EtapaRepositorio = OrdemServico_EtapaRepositorio;
            _OrdemServico = OrdemServicoRepositorio;

        }

        public IActionResult Index()
        {
            _listaEtapaCadastrada.Clear();

            UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

            var os = _OrdemServico.OsLIST(Usuario.Id);
            return View(os);
        }


        public IActionResult ModalExcluir(int Id)
        {
            ServicoModel servico = new ServicoModel();
            servico.Id = Id;
            return PartialView("ModalExcluir", servico);
        }

        public IActionResult ModalExcluirOS(int Id)
        {
            OrdemServicoModel os = new OrdemServicoModel();
            os.Id = Id;
            return PartialView("ModalExcluirOS", os);
        }


        [HttpPost]
        public IActionResult ExcluirOS(OrdemServicoModel os)
        {
            _OrdemServico.Excluir(os.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ExcluirEtapa(EtapaModel etapa)
        {
            _EtapaRepositorio.Excluir(etapa.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Gerenciamento(int IDOrdemServico)
        {
            _idpagina = IDOrdemServico == 0 ? _idpagina : IDOrdemServico;

            var view = _OrdemServico.GetviewViewGerenciamento(_idpagina);

            return View(view);
        }

        public IActionResult OrdemServico()
        {
            ViewModelOrdemServico ViewOS = new ViewModelOrdemServico();
            UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

            ViewOS.ListServico = _ServicoRepositorio.Buscartodos(Usuario.Id);
            ViewOS.ListCliente = _ClienteRepositorio.Buscartodos(Usuario.Id);

            _listaEtapaCadastrada = _EtapaRepositorio.Buscartodos(Usuario.Id);

            ViewOS.ListEtapa = _listaEtapaCadastrada;

            return View(ViewOS);
        }

        public IActionResult tapaModal()
        {
            return PartialView("AddEtapaModal");
        }


        [HttpPost]
        public IActionResult addEtapa(EtapaModel Etapa)
        {
            if (Etapa.Nome != null)
            {
                UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

                Etapa.UsuarioID = Usuario.Id;

                _EtapaRepositorio.adicionar(Etapa);

                return RedirectToAction("OrdemServico");
            }
            return RedirectToAction("OrdemServico");
        }

        [HttpPost]
        public IActionResult AddNewOS(ViewModelOrdemServico model)
        {
            if (model.Cliente == null)
                return RedirectToAction("Index");

            if (model.Servico == null)
                return RedirectToAction("Index");

            if (model.Preco == null)
                return RedirectToAction("Index");

            OrdemServicoModel os = new OrdemServicoModel();
            os.ClienteID = model.Cliente;
            os.ServicoID = model.Servico;
            os.Preco = model.Preco;
            os.Observacao = model.Observacao;
            os.Status = "Aguardando";

            int OrdemServicoID = _OrdemServico.adicionar(os);

            if (model.EtapasOS == null)
                return RedirectToAction("Index");


            _OrdemServico_EtapaRepositorio.adicionar(model.EtapasOS, OrdemServicoID);


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Vigente(int Id)
        {
            OrdemServico_EtapaModel etapa = _OrdemServico_EtapaRepositorio.GetByID(Id);
            bool existVigente = _OrdemServico_EtapaRepositorio.Vigente(etapa.OrdemServicoID);

            if (existVigente == false)
            {
                if (etapa.Status != "finalizado" && etapa.Status != "vigente")
                {

                    etapa.Status = "vigente";

                    _OrdemServico_EtapaRepositorio.Atualizar(etapa);

                    return RedirectToAction("Gerenciamento", _idpagina);
                }
                return RedirectToAction("Gerenciamento", _idpagina);
            }
            return RedirectToAction("Gerenciamento", _idpagina);
        }

        [HttpPost]
        public IActionResult Desvincular(int Id)
        {
            OrdemServico_EtapaModel etapa = _OrdemServico_EtapaRepositorio.GetByID(Id);

            if (etapa != null)
            {
                etapa.Status = "";

                _OrdemServico_EtapaRepositorio.Atualizar(etapa);

                return RedirectToAction("Gerenciamento", _idpagina);
            }
            return RedirectToAction("Gerenciamento", _idpagina);
        }

        [HttpPost]
        public IActionResult Finalizar(int Id)
        {
            OrdemServico_EtapaModel etapa = _OrdemServico_EtapaRepositorio.GetByID(Id);

            if (etapa.Status == "vigente")
            {

                etapa.Status = "finalizado";

                _OrdemServico_EtapaRepositorio.Atualizar(etapa);

                return RedirectToAction("Gerenciamento", _idpagina);
            }
            return RedirectToAction("Gerenciamento", _idpagina);
        }

        [HttpPost]
        public IActionResult Atualizar(int Id)
        {
            _OrdemServico.Finalizar(Id);
            return RedirectToAction("Index");
        }

    }
}
