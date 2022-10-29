using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarBD.Data.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        [Required]
        public string NombreProducto { get; set; }
        [Required]
        public float PrecioProducto { get; set; }
        [Required]
        public int CodigoProducto { get; set; }
        [Required]
        public int Stock { get; set; }


        public List<Renglon> Renglones { get; set; }

    }
}
