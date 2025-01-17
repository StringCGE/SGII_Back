using Dominio;
using Dominio.DB;
using SGII_Back.Util;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondicionLaboralController : Controller
    {

        DbCondicionLaboral dbCondicionLaboral = new DbCondicionLaboral();
        // GET: api/CondicionLaboral
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsCondicionLaboral>>> Get([FromQuery] FetchDataCondicionLaboral fetchData)
        {
            try
            {
                List<ClsCondicionLaboral> items = await dbCondicionLaboral.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/CondicionLaboral/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsCondicionLaboral>> Get(int id)
        {
            try
            {
                ClsCondicionLaboral item = await dbCondicionLaboral.ObtenerPorIdAsync(id);
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

        // POST api/CondicionLaboral
        [HttpPost]
        public async Task<ActionResult<ClsCondicionLaboral>> Post([FromBody] ClsCondicionLaboral item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbCondicionLaboral.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/CondicionLaboral/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsCondicionLaboral item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsCondicionLaboral itemExistente = await dbCondicionLaboral.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbCondicionLaboral.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/CondicionLaboral/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsCondicionLaboral itemExistente = await dbCondicionLaboral.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbCondicionLaboral.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
