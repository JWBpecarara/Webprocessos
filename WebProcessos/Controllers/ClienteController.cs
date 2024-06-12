using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebProcessos.Models;
using WebProcessos.Repositorio;
using WebProcessos.Uteis;

namespace WebProcessos.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ISessao _sessao;

        public ClienteController(IClienteRepositorio clienteRepositorio, ISessao sessao)
        {
            _clienteRepositorio = clienteRepositorio;
            _sessao = sessao;

        }
    

        public IActionResult Index()
        {
            UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();

            ViewModelCliente cliente = new ViewModelCliente();
            cliente.ListaClientes = _clienteRepositorio.Buscartodos(Usuario.Id);            
            return View(cliente);
        }

        public IActionResult ModalExcluir(int Id)
        {
            ClienteModel cliente = new ClienteModel();
            cliente.Id = Id;
            return PartialView("ModalExcluir", cliente);
        }

        public IActionResult Editar(int Id)
        {
            ClienteModel cliente = _clienteRepositorio.GetByID(Id);
            return PartialView("ClienteModal", cliente);
        }
        public IActionResult ExibirModal(int Id)
        {
            ClienteModel cliente = _clienteRepositorio.GetByID(Id);
            return PartialView("ExibirModal", cliente);
        }


        [HttpPost]
        public IActionResult Criar (ViewModelCliente ViewModelCliente)
        {
            UsuarioModel Usuario = _sessao.BuscarSessaoDeUsuario();
            ViewModelCliente.Cliente.UsuarioId = Usuario.Id;
            ViewModelCliente.Cliente.Senha = gerasenha();

            _clienteRepositorio.adicionar(ViewModelCliente.Cliente);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Atualizar(ClienteModel cliente)
        {
            _clienteRepositorio.Atualizar(cliente);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Excluir(ClienteModel cliente)
        {
            _clienteRepositorio.Excluir(cliente);
            return RedirectToAction("Index");
        }

        public string gerasenha()
        {
            // Caracteres que serão usados na senha
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@";

            // Gerar a senha aleatória
            Random random = new Random();
            StringBuilder senha = new StringBuilder();
            for (int i = 0; i < 7; i++)
            {
                int index = random.Next(caracteres.Length);
                senha.Append(caracteres[index]);
            }

            return senha.ToString();
        }
    }
}
