using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Dominio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGII_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        public StatusController()
        {
            
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
