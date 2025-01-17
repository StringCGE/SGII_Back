using Dominio;
using Dominio.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionParticipanteController : Controller
    {
        DbFuncionParticipante dbFuncionParticipante = new DbFuncionParticipante();
        // GET: api/FuncionParticipante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsFuncionParticipante>>> Get([FromQuery] FetchDataFuncionParticipante fetchData)
        {
            try
            {
                List<ClsFuncionParticipante> items = await dbFuncionParticipante.ListarAsync(fetchData);
                return Ok(items);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET api/FuncionParticipante/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsFuncionParticipante>> Get(int id)
        {
            try
            {
                ClsFuncionParticipante item = await dbFuncionParticipante.ObtenerPorIdAsync(id);
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

        // POST api/FuncionParticipante
        [HttpPost]
        public async Task<ActionResult<ClsFuncionParticipante>> Post([FromBody] ClsFuncionParticipante item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbFuncionParticipante.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/FuncionParticipante/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsFuncionParticipante item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsFuncionParticipante itemExistente = await dbFuncionParticipante.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbFuncionParticipante.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/FuncionParticipante/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsFuncionParticipante itemExistente = await dbFuncionParticipante.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbFuncionParticipante.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
