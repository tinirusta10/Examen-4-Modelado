using BazarBD;
using BazarBD.Data.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ModeladoTrabajo.Server.Controllers
{
    [Route("api/Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly BDContext context;

        public ClienteController(BDContext context)
        {

            this.context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {

            var resp = await context.Clientes.ToListAsync();
            return resp;

        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Cliente venta)
        {
            try
            {
                context.Clientes.Add(venta);
                await context.SaveChangesAsync();
                return venta.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {

            var venta = await context.Clientes.FindAsync(id);


            if (venta == null)
            {
                return NotFound($"No existe venta de id: {id}");
            }
            return venta;
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Cliente client)
        {
            if (id != client.Id)
            {
                return BadRequest("Datos Incorrectos");
            }

            var ventas = context.Clientes.Where(e => e.Id == id).FirstOrDefault();

            if (ventas == null)
            {
                return NotFound("No existe  el cliente buscada");
            }

            ventas.NombreCliente = client.NombreCliente;
            ventas.ApellidoCliente = client.ApellidoCliente;
            try
            {
                context.Clientes.Update(ventas);
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
            var venta = context.Clientes.Where(x => x.Id == id).FirstOrDefault();


            if (venta == null)
            {
                return NotFound($"El registro {id} no fue encontrado");
            }

            try
            {

                context.Clientes.Remove(venta);

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
