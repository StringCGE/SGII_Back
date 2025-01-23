using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroFacturaController : Controller
    {
        DbRegistroFactura dbRegistroFactura = new DbRegistroFactura();
        // GET: api/RegistroFactura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsRegistroFactura>>> Get([FromQuery] FetchDataRegistroFactura fetchData)
        {
            try
            {
                List<ClsRegistroFactura> items = await dbRegistroFactura.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/RegistroFactura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsRegistroFactura>> Get(int id)
        {
            try
            {
                ClsRegistroFactura item = await dbRegistroFactura.ObtenerPorIdAsync(id);
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

        // POST api/RegistroFactura
        [HttpPost]
        public async Task<ActionResult<ClsRegistroFactura>> Post([FromBody] ClsRegistroFactura item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbRegistroFactura.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/RegistroFactura/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsRegistroFactura item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsRegistroFactura itemExistente = await dbRegistroFactura.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbRegistroFactura.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/RegistroFactura/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsRegistroFactura itemExistente = await dbRegistroFactura.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbRegistroFactura.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
