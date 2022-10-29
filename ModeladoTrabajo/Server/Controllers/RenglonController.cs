using BazarBD;
using BazarBD.Data.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ModeladoTrabajo.Server.Controllers
{
    [Route("api/Renglon")]
    [ApiController]
    public class RenglonController : ControllerBase
    {
        private readonly BDContext context;

        public RenglonController(BDContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Renglon venta)
        {
            try
            {
                context.Renglones.Add(venta);
                await context.SaveChangesAsync();
                return venta.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Renglon>>> Get()
        {

            var resp = await context.Renglones.Include(x=>x.Factura).Include(x=>x.Producto).Include(x=> x.Factura.Cliente).ToListAsync();

            return resp;

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Renglon>> Get(int id)


        {

            var venta = await context.Renglones.FindAsync(id);


            if (venta == null)
            {
                return NotFound($"No existe venta de id: {id}");
            }
            return venta;
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var venta = context.Renglones.Where(x => x.Id == id).FirstOrDefault();


            if (venta == null)
            {
                return NotFound($"El registro {id} no fue encontrado");
            }

            try
            {


                context.Renglones.Remove(venta);

                context.SaveChanges();
                return Ok($"El registro de {venta.Id} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no pudieron eliminarse por: {e.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Renglon reng)
        {
            if (id != reng.Id)
            {
                return BadRequest("Datos Incorrectos");
            }

            var ventas = context.Renglones.Where(e => e.Id == id).FirstOrDefault();

            if (ventas == null)
            {
                return NotFound("No existe  el cliente buscada");
            }

            ventas.Factura = reng.Factura;
            ventas.Producto = reng.Producto;
            try
            {
                context.Renglones.Update(ventas);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Los datos no han sido actualizados por: {e.Message}");
            }
        }
    }
}
