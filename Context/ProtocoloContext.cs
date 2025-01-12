using Microsoft.EntityFrameworkCore;
using TesteDevDbm.Models;

namespace TesteDevDbm.Context
{
    public class ProtocoloContext : DbContext
    {
        public ProtocoloContext(DbContextOptions<ProtocoloContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<StatusProtocolo> StatusProtocolos { get; set; }
        public DbSet<Protocolo> Protocolos { get; set; }
        public DbSet<ProtocoloFollow> ProtocolosFollow { get; set; }
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatusProtocolo>().HasData(
                new StatusProtocolo { IdStatus = 1, NomeStatus = "Aberto" },
                new StatusProtocolo { IdStatus = 2, NomeStatus = "Em Andamento" },
                new StatusProtocolo { IdStatus = 3, NomeStatus = "Fechado" }
            );
        }
    }
}