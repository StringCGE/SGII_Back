using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmisorController : Controller
    {
        DbEmisor dbEmisor = new DbEmisor();
        // GET: api/Emisor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsEmisor>>> Get([FromQuery] FetchDataEmisor fetchData)
        {
            try
            {
                List<ClsEmisor> items = await dbEmisor.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Emisor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsEmisor>> Get(int id)
        {
            try
            {
                ClsEmisor item = await dbEmisor.ObtenerPorIdAsync(id);
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

        // POST api/Emisor
        [HttpPost]
        public async Task<ActionResult<ClsEmisor>> Post([FromBody] ClsEmisor item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbEmisor.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Emisor/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsEmisor item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsEmisor itemExistente = await dbEmisor.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbEmisor.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Emisor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsEmisor itemExistente = await dbEmisor.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbEmisor.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
