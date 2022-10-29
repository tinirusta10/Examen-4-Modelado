using BazarBD;
using BazarBD.Data.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ModeladoTrabajo.Server.Controllers
{
    [Route("api/Producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly BDContext context;

        public ProductoController(BDContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {

            var resp = await context.Productos.ToListAsync();
            return resp;

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var venta = await context.Productos
                                         .Where(e => e.Id == id)
                                         .Include(m => m.Renglones)
                                         .FirstOrDefaultAsync();

            if (venta == null)
            {
                return NotFound($"No existe el producto de id: {id}");
            }
            return venta;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Producto venta)
        {
            try
            {
                context.Productos.Add(venta);
                await context.SaveChangesAsync();
                return venta.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Producto produ)
        {
            if (id != produ.Id)
            {
                return BadRequest("No se encuentra la venta");
            }

            var ventas = context.Productos.Where(e => e.Id == id).FirstOrDefault();

            if (ventas == null)
            {
                return NotFound("No existe la venta a modificar");
            }

            ventas.NombreProducto = produ.NombreProducto;
            ventas.PrecioProducto = produ.PrecioProducto;
            ventas.CodigoProducto = produ.CodigoProducto;
            ventas.Stock = produ.Stock;


            try
            {
                context.Productos.Update(ventas);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no han sido actualizados por: {e.Message}");
            }
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            var produ = context.Productos.Where(x => x.Id == id).FirstOrDefault();


            if (produ == null)
            {
                return NotFound($"El registro {id} no fue encontrado");
            }

            try
            {
                context.Productos.Remove(produ);



                context.SaveChanges();
                return Ok($"El registro de {produ.Id} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por: {e.Message}");
            }
        }
    }
}
