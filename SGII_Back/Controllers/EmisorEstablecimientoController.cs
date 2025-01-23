using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmisorEstablecimientoController : Controller
    {
        DbEmisorEstablecimiento dbEmisorEstablecimiento = new DbEmisorEstablecimiento();
        // GET: api/EmisorEstablecimiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsEmisorEstablecimiento>>> Get([FromQuery] FetchDataEmisorEstablecimiento fetchData)
        {
            try
            {
                List<ClsEmisorEstablecimiento> items = await dbEmisorEstablecimiento.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/EmisorEstablecimiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsEmisorEstablecimiento>> Get(int id)
        {
            try
            {
                ClsEmisorEstablecimiento item = await dbEmisorEstablecimiento.ObtenerPorIdAsync(id);
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

        // POST api/EmisorEstablecimiento
        [HttpPost]
        public async Task<ActionResult<ClsEmisorEstablecimiento>> Post([FromBody] ClsEmisorEstablecimiento item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbEmisorEstablecimiento.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/EmisorEstablecimiento/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsEmisorEstablecimiento item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsEmisorEstablecimiento itemExistente = await dbEmisorEstablecimiento.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbEmisorEstablecimiento.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/EmisorEstablecimiento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsEmisorEstablecimiento itemExistente = await dbEmisorEstablecimiento.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbEmisorEstablecimiento.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
