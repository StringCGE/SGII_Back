using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCivilController : Controller
    {
        DbEstadoCivil dbEstadoCivil = new DbEstadoCivil();
        // GET: api/EstadoCivil
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsEstadoCivil>>> Get([FromQuery] FetchDataEstadoCivil fetchData)
        {
            try
            {
                List<ClsEstadoCivil> items = await dbEstadoCivil.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/EstadoCivil/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsEstadoCivil>> Get(int id)
        {
            try
            {
                ClsEstadoCivil item = await dbEstadoCivil.ObtenerPorIdAsync(id);
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

        // POST api/EstadoCivil
        [HttpPost]
        public async Task<ActionResult<ClsEstadoCivil>> Post([FromBody] ClsEstadoCivil item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbEstadoCivil.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/EstadoCivil/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsEstadoCivil item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsEstadoCivil itemExistente = await dbEstadoCivil.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbEstadoCivil.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/EstadoCivil/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsEstadoCivil itemExistente = await dbEstadoCivil.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbEstadoCivil.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
