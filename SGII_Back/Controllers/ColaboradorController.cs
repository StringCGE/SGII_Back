using Dominio;
using Dominio.DB;
using SGII_Back.Util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : Controller
    {
        DbColaborador dbColaborador = new DbColaborador();

        // GET: api/Colaborador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsColaborador>>> Get([FromQuery] FetchDataColaborador fetchData)
        {
            try
            {
                List<ClsColaborador> items = await dbColaborador.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Colaborador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsColaborador>> Get(int id)
        {
            try
            {
                ClsColaborador item = await dbColaborador.ObtenerPorIdAsync(id);
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

        // POST api/Colaborador
        [HttpPost]
        public async Task<ActionResult<ClsColaborador>> Post([FromBody] ClsColaborador item)
        {
            //return BadRequest("Probando");
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbColaborador.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Colaborador/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsColaborador item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsColaborador itemExistente = await dbColaborador.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                item.id = itemExistente.id; // Asegura que se use el ID correcto
                await dbColaborador.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Colaborador/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsColaborador itemExistente = await dbColaborador.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbColaborador.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
