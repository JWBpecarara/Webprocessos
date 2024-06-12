using WebProcessos.Data;
using WebProcessos.Models;

namespace WebProcessos.Repositorio
{
    public class VincularServico : IVincularServico
    {

        private readonly BancoContext _bancoContext;
        public VincularServico(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ServicoVinculadoModel adicionar(ServicoVinculadoModel ServicoVinculado)
        {
            _bancoContext.ServicoVinculado.Add(ServicoVinculado);
            _bancoContext.SaveChanges();
            return ServicoVinculado;
        }

        public List<ViewModelVincular> Buscartodos(int UsuarioId)
        {
            List<ViewModelVincular> ViewModelVincular = (from sv in _bancoContext.ServicoVinculado
                                                         join s in _bancoContext.Servico on sv.ServicoID equals s.Id
                                                         join c in _bancoContext.Cliente on sv.ClienteID equals c.Id
                                                         where s.Excluido == false && c.Excluido == false && sv.Excluido == false && c.UsuarioId == UsuarioId
                                                         select new ViewModelVincular()
                                                         {
                                                             ServicoVinId = sv.Id,
                                                             NomeCliente = c.Nome,
                                                             CPF = c.CPF,
                                                             Telefone = c.Telefone,
                                                             Status = sv.Status,
                                                             NomeServico = s.NomeServico,
                                                             Preco = s.Preco
                                                         }).ToList();
            return ViewModelVincular;
        }

        public void Excluir(ServicoVinculadoModel ServicoVinculado)
        {

            ServicoVinculadoModel ServicoVinculadoDB = GetByID(ServicoVinculado.Id);

            if (ServicoVinculadoDB == null) throw new System.Exception("Erro ao Excluir cliente");

            ServicoVinculadoDB.Excluido = true;

            _bancoContext.ServicoVinculado.Update(ServicoVinculadoDB);
            _bancoContext.SaveChanges();                   
        }

        public void Finalizar(int id)
        {

            ServicoVinculadoModel ServicoModelDB = GetByID(id);

            if (ServicoModelDB == null) throw new System.Exception("Erro na atulização do cliente");

            ServicoModelDB.Status = "Finalizado";


            _bancoContext.ServicoVinculado.Update(ServicoModelDB);
            _bancoContext.SaveChanges();
        }

        public ServicoVinculadoModel GetByID(int id)
        {
            return _bancoContext.ServicoVinculado.FirstOrDefault(x => x.Id == id && x.Excluido == false);
        }

    }
}
