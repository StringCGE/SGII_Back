using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoLaboralController : Controller
    {

        DbEstadoLaboral dbEstadoLaboral = new DbEstadoLaboral();
        // GET: api/EstadoLaboral
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsEstadoLaboral>>> Get([FromQuery] FetchDataEstadoLaboral fetchData)
        {
            try
            {
                List<ClsEstadoLaboral> items = await dbEstadoLaboral.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/EstadoLaboral/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsEstadoLaboral>> Get(int id)
        {
            try
            {
                ClsEstadoLaboral item = await dbEstadoLaboral.ObtenerPorIdAsync(id);
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

        // POST api/EstadoLaboral
        [HttpPost]
        public async Task<ActionResult<ClsEstadoLaboral>> Post([FromBody] ClsEstadoLaboral item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbEstadoLaboral.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/EstadoLaboral/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsEstadoLaboral item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsEstadoLaboral itemExistente = await dbEstadoLaboral.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbEstadoLaboral.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/EstadoLaboral/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsEstadoLaboral itemExistente = await dbEstadoLaboral.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbEstadoLaboral.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
