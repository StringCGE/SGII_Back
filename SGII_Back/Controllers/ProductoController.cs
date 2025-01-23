using Dominio.DB;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        DbProducto dbProducto = new DbProducto();
        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsProducto>>> Get([FromQuery] FetchDataProducto fetchData)
        {
            try
            {
                List<ClsProducto> items = await dbProducto.ListarAsync(fetchData);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsProducto>> Get(int id)
        {
            try
            {
                ClsProducto item = await dbProducto.ObtenerPorIdAsync(id);
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

        // GET api/Producto/5
        [HttpGet("/api/Producto/all")]
        public async Task<ActionResult<ClsProducto>> GetAll()
        {
            try
            {
                List<ClsProducto> items = await dbProducto.GetAll();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Producto
        [HttpPost]
        public async Task<ActionResult<ClsProducto>> Post([FromBody] ClsProducto item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbProducto.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Producto/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsProducto item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsProducto itemExistente = await dbProducto.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi ?? 0;
                if (item == null)
                {
                    return NotFound();
                }
                await dbProducto.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Producto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsProducto itemExistente = await dbProducto.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbProducto.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
