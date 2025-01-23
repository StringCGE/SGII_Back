using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaNotaCreditoController : Controller
    {
        DbFacturaNotaCredito dbFacturaNotaCredito = new DbFacturaNotaCredito();
        // GET: api/FacturaNotaCredito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsFacturaNotaCredito>>> Get([FromQuery] FetchDataFacturaNotaCredito fetchData)
        {
            try
            {
                List<ClsFacturaNotaCredito> items = await dbFacturaNotaCredito.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/FacturaNotaCredito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsFacturaNotaCredito>> Get(int id)
        {
            try
            {
                ClsFacturaNotaCredito item = await dbFacturaNotaCredito.ObtenerPorIdAsync(id);
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

        // POST api/FacturaNotaCredito
        [HttpPost]
        public async Task<ActionResult<ClsFacturaNotaCredito>> Post([FromBody] ClsFacturaNotaCredito item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                int result = await dbFacturaNotaCredito.CrearAsync(item);
                if (result == 1)
                {
                    return CreatedAtAction(nameof(Get), new { id = item.id }, item);
                }
                else
                {
                    return BadRequest("No se inserto la factura");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/FacturaNotaCredito/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsFacturaNotaCredito item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsFacturaNotaCredito itemExistente = await dbFacturaNotaCredito.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbFacturaNotaCredito.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/FacturaNotaCredito/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsFacturaNotaCredito itemExistente = await dbFacturaNotaCredito.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbFacturaNotaCredito.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
