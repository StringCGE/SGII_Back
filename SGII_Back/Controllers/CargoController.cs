using Dominio;
using Dominio.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : Controller
    {
        DbCargo dbCargo = new DbCargo();
        // GET: api/Cargo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClsCargo>>> Get([FromQuery] FetchDataCargo fetchData)
        {
            try
            {
                List<ClsCargo> items = await dbCargo.ListarAsync(fetchData);
                return Ok(items);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET api/Cargo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClsCargo>> Get(int id)
        {
            try
            {
                ClsCargo item = await dbCargo.ObtenerPorIdAsync(id);
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

        // POST api/Cargo
        [HttpPost]
        public async Task<ActionResult<ClsCargo>> Post([FromBody] ClsCargo item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo");
                }
                await dbCargo.CrearAsync(item);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Cargo/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClsCargo item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("El item no puede ser nulo"); // Si el item es nulo, devuelve 400
                }
                ClsCargo itemExistente = await dbCargo.ObtenerPorIdAsync((int)item.idApi!);
                item.id = item.idApi;
                if (item == null)
                {
                    return NotFound();
                }
                await dbCargo.EditarAsync(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/Cargo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ClsCargo itemExistente = await dbCargo.ObtenerPorIdAsync(id);
                if (itemExistente == null)
                {
                    return NotFound();
                }
                await dbCargo.EliminarAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
