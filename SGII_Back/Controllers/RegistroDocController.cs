using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroDocController : Controller
    {
        DbRegistroDoc dbRegistroDoc = new DbRegistroDoc();
        // GET: api/RegistroDoc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsRegistroDoc>>> Get([FromQuery] FetchDataRegistroDoc fetchData)
        {
            try
            {
                List<ClsRegistroDoc> items = await dbRegistroDoc.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/RegistroDoc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsRegistroDoc>> Get(int id)
        {
            try
            {
                ClsRegistroDoc item = await dbRegistroDoc.ObtenerPorIdAsync(id);
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

        // POST api/RegistroDoc
        [HttpPost]
        public async Task<ActionResult<ClsRegistroDoc>> Post([FromBody] ClsRegistroDoc item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbRegistroDoc.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/RegistroDoc/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsRegistroDoc item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsRegistroDoc itemExistente = await dbRegistroDoc.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbRegistroDoc.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/RegistroDoc/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsRegistroDoc itemExistente = await dbRegistroDoc.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbRegistroDoc.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
