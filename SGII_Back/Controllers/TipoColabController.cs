using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoColabController : Controller
    {
        DbTipoColab dbTipoColab = new DbTipoColab();
        // GET: api/TipoColab
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsTipoColab>>> Get([FromQuery] FetchDataTipoColab fetchData)
        {
            try
            {
                List<ClsTipoColab> items = await dbTipoColab.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/TipoColab/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsTipoColab>> Get(int id)
        {
            try
            {
                ClsTipoColab item = await dbTipoColab.ObtenerPorIdAsync(id);
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

        // POST api/TipoColab
        [HttpPost]
        public async Task<ActionResult<ClsTipoColab>> Post([FromBody] ClsTipoColab item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbTipoColab.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/TipoColab/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsTipoColab item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsTipoColab itemExistente = await dbTipoColab.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbTipoColab.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/TipoColab/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsTipoColab itemExistente = await dbTipoColab.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbTipoColab.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
