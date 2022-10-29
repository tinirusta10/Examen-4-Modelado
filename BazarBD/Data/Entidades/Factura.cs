using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarBD.Data.Entidades
{
    public class Factura
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }




        public List<Renglon> Renglones { get; set; }

        public int total { get; set; }

        public DateTime FechaVenta { get; set; }

    }
}
