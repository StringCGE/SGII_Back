using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NacionalidadController : Controller
    {
        DbNacionalidad dbNacionalidad = new DbNacionalidad();
        // GET: api/Nacionalidad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsNacionalidad>>> Get([FromQuery] FetchDataNacionalidad fetchData)
        {
            try
            {
                List<ClsNacionalidad> items = await dbNacionalidad.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/Nacionalidad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsNacionalidad>> Get(int id)
        {
            try
            {
                ClsNacionalidad item = await dbNacionalidad.ObtenerPorIdAsync(id);
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

        // POST api/Nacionalidad
        [HttpPost]
        public async Task<ActionResult<ClsNacionalidad>> Post([FromBody] ClsNacionalidad item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbNacionalidad.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Nacionalidad/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsNacionalidad item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsNacionalidad itemExistente = await dbNacionalidad.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbNacionalidad.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Nacionalidad/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsNacionalidad itemExistente = await dbNacionalidad.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbNacionalidad.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
