using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIdentificacionController : Controller
    {
        DbTipoIdentificacion dbTipoIdentificacion = new DbTipoIdentificacion();
        // GET: api/TipoIdentificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsTipoIdentificacion>>> Get([FromQuery] FetchDataTipoIdentificacion fetchData)
        {
            try
            {
                List<ClsTipoIdentificacion> items = await dbTipoIdentificacion.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/TipoIdentificacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsTipoIdentificacion>> Get(int id)
        {
            try
            {
                ClsTipoIdentificacion item = await dbTipoIdentificacion.ObtenerPorIdAsync(id);
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

        // POST api/TipoIdentificacion
        [HttpPost]
        public async Task<ActionResult<ClsTipoIdentificacion>> Post([FromBody] ClsTipoIdentificacion item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbTipoIdentificacion.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/TipoIdentificacion/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsTipoIdentificacion item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsTipoIdentificacion itemExistente = await dbTipoIdentificacion.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbTipoIdentificacion.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/TipoIdentificacion/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsTipoIdentificacion itemExistente = await dbTipoIdentificacion.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbTipoIdentificacion.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
