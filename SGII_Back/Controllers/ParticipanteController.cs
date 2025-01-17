using Dominio;
using Dominio.DB;
using SGII_Back.Util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipanteController : Controller
    {
        DbParticipante dbParticipante = new DbParticipante();

        // GET: api/Participante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsParticipante>>> Get([FromQuery] FetchDataParticipante fetchData)
        {
            try
            {
                List<ClsParticipante> items = await dbParticipante.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Participante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsParticipante>> Get(int id)
        {
            try
            {
                ClsParticipante item = await dbParticipante.ObtenerPorIdAsync(id);
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

        // POST api/Participante
        [HttpPost]
        public async Task<ActionResult<ClsParticipante>> Post([FromBody] ClsParticipante item)
        {
            //return BadRequest("Probando");
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbParticipante.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Participante/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsParticipante item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsParticipante itemExistente = await dbParticipante.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                item.id = itemExistente.id; // Asegura que se use el ID correcto
                await dbParticipante.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Participante/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsParticipante itemExistente = await dbParticipante.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbParticipante.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

