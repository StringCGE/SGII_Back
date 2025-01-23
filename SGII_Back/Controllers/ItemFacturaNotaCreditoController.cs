using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemFacturaNotaCreditoController : Controller
    {
        DbItemFacturaNotaCredito dbItemFacturaNotaCredito = new DbItemFacturaNotaCredito();
        // GET: api/ItemFacturaNotaCredito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsItemFacturaNotaCredito>>> Get([FromQuery] FetchDataItemFacturaNotaCredito fetchData)
        {
            try
            {
                List<ClsItemFacturaNotaCredito> items = await dbItemFacturaNotaCredito.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/ItemFacturaNotaCredito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsItemFacturaNotaCredito>> Get(int id)
        {
            try
            {
                ClsItemFacturaNotaCredito item = await dbItemFacturaNotaCredito.ObtenerPorIdAsync(id);
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

        // POST api/ItemFacturaNotaCredito
        [HttpPost]
        public async Task<ActionResult<ClsItemFacturaNotaCredito>> Post([FromBody] ClsItemFacturaNotaCredito item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbItemFacturaNotaCredito.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/ItemFacturaNotaCredito/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsItemFacturaNotaCredito item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsItemFacturaNotaCredito itemExistente = await dbItemFacturaNotaCredito.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbItemFacturaNotaCredito.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/ItemFacturaNotaCredito/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsItemFacturaNotaCredito itemExistente = await dbItemFacturaNotaCredito.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbItemFacturaNotaCredito.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
