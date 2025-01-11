using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TesteDevDbm.Models;

namespace TesteDevDbm.Context
{
    public class ProtocoloContext : DbContext
    {
        public ProtocoloContext(DbContextOptions<ProtocoloContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get;  set; }
    }
}