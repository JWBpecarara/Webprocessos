using WebProcessos.Data;
using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public class ServicoRepositorio : IServicoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ServicoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public int adicionar(ServicoModel Servico)
        {
            _bancoContext.Servico.Add(Servico);
            _bancoContext.SaveChanges();
            return Servico.Id;
        }

        public ServicoModel Atualizar(ServicoModel Servico)
        {

            ServicoModel ServicoModelDB = GetByID(Servico.Id);

            if (ServicoModelDB == null) throw new System.Exception("Erro na atulização do cliente");

            ServicoModelDB.NomeServico = Servico.NomeServico;
            ServicoModelDB.Preco = Servico.Preco;
            ServicoModelDB.Descricao = Servico.Descricao;

            _bancoContext.Servico.Update(ServicoModelDB);
            _bancoContext.SaveChanges();

            return ServicoModelDB;
            
        }

        public List<ServicoModel> Buscartodos(int UsuarioId)
        {
            return _bancoContext.Servico.Where(x => x.Excluido == false && x.UsuarioId == UsuarioId).ToList();
        }

        public ServicoModel Excluir(ServicoModel Servico)
        {
            ServicoModel ServicoModelDB = GetByID(Servico.Id);

            if (ServicoModelDB == null) throw new System.Exception("Erro ao Excluir cliente");

            ServicoModelDB.Excluido = true;

            _bancoContext.Servico.Update(ServicoModelDB);
            _bancoContext.SaveChanges();

            return ServicoModelDB;
        }


        public ServicoModel GetByID(int Id)
        {
            return _bancoContext.Servico.FirstOrDefault(x => x.Id == Id && x.Excluido == false);
        }
    }
}
