using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmisorItemController : Controller
    {
        DbEmisorItem dbEmisorItem = new DbEmisorItem();
        // GET: api/EmisorItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsEmisorItem>>> Get([FromQuery] FetchDataEmisorItem fetchData)
        {
            try
            {
                List<ClsEmisorItem> items = await dbEmisorItem.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/EmisorItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsEmisorItem>> Get(int id)
        {
            try
            {
                ClsEmisorItem item = await dbEmisorItem.ObtenerPorIdAsync(id);
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

        // POST api/EmisorItem
        [HttpPost]
        public async Task<ActionResult<ClsEmisorItem>> Post([FromBody] ClsEmisorItem item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbEmisorItem.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/EmisorItem/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsEmisorItem item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsEmisorItem itemExistente = await dbEmisorItem.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbEmisorItem.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/EmisorItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsEmisorItem itemExistente = await dbEmisorItem.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbEmisorItem.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
