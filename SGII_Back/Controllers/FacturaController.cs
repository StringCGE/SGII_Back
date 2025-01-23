using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : Controller
    {
        DbFactura dbFactura = new DbFactura();
        // GET: api/Factura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsFactura>>> Get([FromQuery] FetchDataFactura fetchData)
        {
            try
            {
                List<ClsFactura> items = await dbFactura.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Factura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsFactura>> Get(int id)
        {
            try
            {
                ClsFactura item = await dbFactura.ObtenerPorIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Factura
        [HttpPost]
        public async Task<ActionResult<ClsFactura>> Post([FromBody] ClsFactura item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbFactura.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Factura/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsFactura item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsFactura itemExistente = await dbFactura.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbFactura.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Factura/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsFactura itemExistente = await dbFactura.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbFactura.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
