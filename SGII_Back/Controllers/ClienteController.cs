using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        DbCliente dbCliente = new DbCliente();
        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsCliente>>> Get([FromQuery] FetchDataCliente fetchData)
        {
            try
            {
                List<ClsCliente> items = await dbCliente.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsCliente>> Get(int id)
        {
            try
            {
                ClsCliente item = await dbCliente.ObtenerPorIdAsync(id);
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

        // POST api/Cliente
        [HttpPost]
        public async Task<ActionResult<ClsCliente>> Post([FromBody] ClsCliente item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbCliente.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Cliente/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsCliente item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsCliente itemExistente = await dbCliente.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbCliente.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsCliente itemExistente = await dbCliente.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbCliente.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
