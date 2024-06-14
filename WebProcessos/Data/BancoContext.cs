using Microsoft.EntityFrameworkCore;
using WebProcessos.Models;

namespace WebProcessos.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> opitions) : base(opitions)
        {
        }

        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<OrdemServicoModel> OrdemServico { get; set; }
        public DbSet<OrdemServico_EtapaModel> OrdemServico_Etapa { get; set; }
        public DbSet<EtapaModel> Etapa { get; set; }
        public DbSet<ServicoModel> Servico{ get; set; }

    }
}
