using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SexoController : Controller
    {
        DbSexo dbSexo = new DbSexo();
        // GET: api/Sexo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsSexo>>> Get([FromQuery] FetchDataSexo fetchData)
        {
            try
            {
                List<ClsSexo> items = await dbSexo.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/Sexo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsSexo>> Get(int id)
        {
            try
            {
                ClsSexo item = await dbSexo.ObtenerPorIdAsync(id);
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

        // POST api/Sexo
        [HttpPost]
        public async Task<ActionResult<ClsSexo>> Post([FromBody] ClsSexo item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbSexo.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Sexo/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsSexo item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                ClsSexo itemExistente = await dbSexo.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbSexo.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Sexo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsSexo itemExistente = await dbSexo.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbSexo.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
