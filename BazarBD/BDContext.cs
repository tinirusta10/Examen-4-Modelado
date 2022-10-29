using BazarBD.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarBD
{
    public class BDContext:DbContext
    {
        public BDContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Renglon> Renglones { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }

}
