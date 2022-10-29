using BazarBD;
using BazarBD.Data.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ModeladoTrabajo.Server.Controllers
{
    [Route("api/Factura")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly BDContext context;

        public FacturaController(BDContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Factura>>> Get()
        {
            
            var resp = await context.Facturas.Include(x => x.Cliente).Include(m=> m.Renglones).ToListAsync();
                                   
            return resp;

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Factura>> Get(int id)
        {
            var venta = await context.Facturas
                                         .Where(e => e.Id == id)
                                         .Include(m => m.Cliente)
                                         .Include(m => m.Renglones)
                                         .FirstOrDefaultAsync();

            if (venta == null)
            {
                return NotFound($"No existe venta de id: {id}");
            }
            return venta;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Factura venta)
        {
            try
            {
                context.Facturas.Add(venta);
                await context.SaveChangesAsync();
                return venta.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Factura venta)
        {
            if (id != venta.Id)
            {
                return BadRequest("No existe esa venta");
            }

            var ventas = context.Facturas.Where(e => e.Id == id).FirstOrDefault();

            if (ventas == null)
            {
                return NotFound("No existe la venta buscada");
            }

            ventas.Cliente = venta.Cliente;
            ventas.FechaVenta = venta.FechaVenta;
            try
            {
                context.Facturas.Update(ventas);
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
            var venta = context.Facturas.Where(x => x.Id == id).FirstOrDefault();


            if (venta == null)
            {
                return NotFound($"El registro {id} no fue encontrado");
            }

            try
            {


                context.Facturas.Remove(venta);

                context.SaveChanges();
                return Ok($"El registro de {venta.Id} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por: {e.Message}");
            }
        }

    }
}
