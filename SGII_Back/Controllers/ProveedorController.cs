using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : Controller
    {
        DbProveedor dbProveedor = new DbProveedor();
        // GET: api/Proveedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsProveedor>>> Get([FromQuery] FetchDataProveedor fetchData)
        {
            try
            {
                List<ClsProveedor> items = await dbProveedor.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Proveedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsProveedor>> Get(int id)
        {
            try
            {
                ClsProveedor item = await dbProveedor.ObtenerPorIdAsync(id);
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

        // POST api/Proveedor
        [HttpPost]
        public async Task<ActionResult<ClsProveedor>> Post([FromBody] ClsProveedor item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbProveedor.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Proveedor/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsProveedor item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsProveedor itemExistente = await dbProveedor.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbProveedor.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Proveedor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsProveedor itemExistente = await dbProveedor.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbProveedor.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
