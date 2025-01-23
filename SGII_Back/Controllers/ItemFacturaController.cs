using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemFacturaController : Controller
    {
        DbItemFactura dbItemFactura = new DbItemFactura();
        // GET: api/ItemFactura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsItemFactura>>> Get([FromQuery] FetchDataItemFactura fetchData)
        {
            try
            {
                List<ClsItemFactura> items = await dbItemFactura.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/ItemFactura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsItemFactura>> Get(int id)
        {
            try
            {
                ClsItemFactura item = await dbItemFactura.ObtenerPorIdAsync(id);
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

        // POST api/ItemFactura
        [HttpPost]
        public async Task<ActionResult<ClsItemFactura>> Post([FromBody] ClsItemFactura item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbItemFactura.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/ItemFactura/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsItemFactura item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsItemFactura itemExistente = await dbItemFactura.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbItemFactura.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/ItemFactura/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsItemFactura itemExistente = await dbItemFactura.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbItemFactura.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
