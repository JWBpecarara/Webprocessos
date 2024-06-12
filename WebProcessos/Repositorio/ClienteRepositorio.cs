using WebProcessos.Data;
using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ClienteRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;   
        }

        public ClienteModel adicionar(ClienteModel cliente)
        {
            _bancoContext.Cliente.Add(cliente);
            _bancoContext.SaveChanges();    
            return cliente;
        }

        public ClienteModel Atualizar(ClienteModel cliente)
        {
            ClienteModel clienteModelDB = GetByID(cliente.Id);

            if (clienteModelDB == null) throw new System.Exception("Erro na atulização do cliente");

            clienteModelDB.Nome = cliente.Nome;
            clienteModelDB.SobreNome = cliente.SobreNome;
            clienteModelDB.Email = cliente.Email;
            clienteModelDB.CPF = cliente.CPF;
            clienteModelDB.Telefone = cliente.Telefone;

            _bancoContext.Cliente.Update(clienteModelDB);
            _bancoContext.SaveChanges();

            return clienteModelDB;
        }

        public List<ClienteModel> Buscartodos(int UsuarioId)
        {
            return _bancoContext.Cliente.Where(x => x.Excluido == false && x.UsuarioId == UsuarioId).ToList();
        }

        public ClienteModel Excluir(ClienteModel cliente)
        {
            ClienteModel clienteModelDB = GetByID(cliente.Id);

            if (clienteModelDB == null) throw new System.Exception("Erro ao Excluir cliente");

            clienteModelDB.Excluido = true;

            _bancoContext.Cliente.Update(clienteModelDB);
            _bancoContext.SaveChanges();

            return clienteModelDB;
        }

        public ClienteModel GetByID(int Id)
        {
            return _bancoContext.Cliente.FirstOrDefault(x => x.Id == Id && x.Excluido == false);
        }

        public ClienteModel GetByServicoVinculado(int Id)
        {
            var clienteId = (from s in _bancoContext.ServicoVinculado
                             join c in _bancoContext.Cliente on s.ClienteID equals c.Id
                             where s.Id == Id
                             select new ClienteModel()
                             {
                                Id = c.Id, 
                                Nome = c.Nome,
                                SobreNome = c.SobreNome,
                                Email = c.Email,
                                CPF = c.CPF,
                                Telefone = c.Telefone
                             }).FirstOrDefault();

            return clienteId; 
        }
    }
}
