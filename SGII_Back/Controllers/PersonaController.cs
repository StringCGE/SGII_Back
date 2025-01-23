using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : Controller
    {
        DbPersona dbPersona = new DbPersona();
        // GET: api/Persona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsPersona>>> Get([FromQuery] FetchDataPersona fetchData)
        {
            try
            {
                List<ClsPersona> items = await dbPersona.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Persona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsPersona>> Get(int id)
        {
            try
            {
                ClsPersona item = await dbPersona.ObtenerPorIdAsync(id);
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

        // POST api/Persona
        [HttpPost]
        public async Task<ActionResult<ClsPersona>> Post([FromBody] ClsPersona item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbPersona.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Persona/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsPersona item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsPersona itemExistente = await dbPersona.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi ?? 0;
                if (item == null)
                {
                    return NotFound();
                }
                await dbPersona.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Persona/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsPersona itemExistente = await dbPersona.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbPersona.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
