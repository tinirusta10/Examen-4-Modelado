using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarBD.Data.Entidades
{
    public class Renglon
    {
        public int Id { get; set; }


        public int FacturaId { get; set; }
        public Factura Factura { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
