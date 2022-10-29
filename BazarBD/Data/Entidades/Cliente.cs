using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarBD.Data.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string NombreCliente { get; set; }
        [Required]
        public string ApellidoCliente { get; set; }



        List<Factura> Facturas { get; set; }
    }
}
