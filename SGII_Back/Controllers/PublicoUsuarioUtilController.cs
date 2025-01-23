
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Avistador.API.Controllers
{
    public class PublicoUsuarioUtilController : Controller
    {/*
        private readonly ISmtpConfigurationAppService _smtpService;
        private readonly SmtpConfigurationAppService smtpService;
        private readonly IPublicacionAppService _publicacionAppService;
        private readonly PublicacionAppService publicacionAppService;
        private readonly ITransaccionService _transaccionService;
        private readonly TransaccionService transaccionService;
        public PublicoUsuarioUtilController(
            ISmtpConfigurationAppService smtpService,
            IPublicacionAppService publicacionAppService,
            ITransaccionService transaccionService
            )
        {
            _publicacionAppService = publicacionAppService;
            this.publicacionAppService = (PublicacionAppService?)publicacionAppService;
            _smtpService = smtpService;
            this.smtpService = (SmtpConfigurationAppService?)_smtpService;
            _transaccionService = transaccionService;
            this.transaccionService = (TransaccionService?)_transaccionService;
        }
        


        // Nuevo endpoint para recuperar la contraseña
        [HttpPost("recover-password")]
        public async Task<ActionResult> RecoverPassword([FromBody] RecoverPasswordRequest request)
        {
            var result = await smtpService.RecoverPasswordAsync(request);
            if (result)
                return Ok("Se ha enviado un correo con instrucciones para recuperar la contraseña.");
            return BadRequest("No se pudo procesar la solicitud. Verifique la información proporcionada.");
        }

        // Nuevo endpoint para enviar el correo de confirmación
        [HttpPost("confirm-email")]
        public async Task<ActionResult> SendConfirmationEmail([FromBody] EmailConfirmationRequest request)
        {
            var result = await smtpService.SendConfirmationEmailAsync(request);
            if (result)
                return Ok("Se ha enviado un correo de confirmación.");
            return BadRequest("No se pudo enviar el correo de confirmación.");
        }

        // Nuevo endpoint para confirmar el código de confirmación
        [HttpPost("confirm-email/{code}")]
        public async Task<ActionResult> ConfirmEmail(string code, [FromBody] EmailConfirmationVerificationRequest request)
        {
            var result = await smtpService.ConfirmEmailAsync(code, request);
            if (result)
                return Ok("El correo ha sido confirmado exitosamente.");
            return BadRequest("Código de confirmación inválido o expirado.");
        }














        

        /// <summary>
        /// Consulta si existe aquel usuario
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("/api/PublicoUsuarioUtil/UrlImage/{value}")]
        public async Task<OperationResult<String>> UrlImage(String value)
        {
            PublicacionFilter filter = new PublicacionFilter();
            filter.Titulo = value;
            filter.Grupo = "urlImage";
            filter.Offset = 0;
            filter.Take = 1;
            IOperationResultList<PublicacionDto> res = await publicacionAppService.GetAll(filter);

            if (res != null && res.Result.Count() > 0)
            {
                PublicacionDto publ = res.Result.First();
                return new OperationResult<string>(
                    HttpStatusCode.OK,
                    "Url " + value,
                    publ.UrlFoto,
                    "");
            }
            else
            {
                return new OperationResult<string>(
                    HttpStatusCode.NotFound,
                    "Url " + value,
                    "",
                    "No se encontró ninguna publicación.");
            }
            
        }
        [HttpPost("/api/PublicoUsuarioUtil/Validador/CambiarPsw")]
        public async Task<IOperationResult> CambiarPsw([FromBody] CambiarPswEmailRequest request)
        {
            return await transaccionService.CambiarPsw(
                request.emailCode,
                request.email,
                request.transaccionId,
                request.usuarioId,
                request.psw
            );
        }
        
        //UserQuery filter
        [HttpPost("/api/PublicoUsuarioUtil/Validador/RecuperarPswCodigoEmail")]
        public async Task<IOperationResult> RecuperarPswCodigoEmail([FromBody] EnviarCodigoEmailRequest request)
        {
            return await transaccionService.RecuperarPswCodigoEmail(request.Email);
        }
        
        [HttpPost("/api/PublicoUsuarioUtil/Validador/EnviarCodigoEmail")]
        public async Task<IOperationResult> ValidadorEnviarCodigoEmail([FromBody] EnviarCodigoEmailRequest request)
        {
            return await transaccionService.ValidadorEnviarCodigoEmail(request.Email);
        }
        
        [HttpPost("/api/PublicoUsuarioUtil/Validador/VerificarCodigoEmail")]
        public async Task<IOperationResult> verificarCodigo([FromBody] VerificarCodigoEmailRequest request)
        {
            return await transaccionService.verificarCodigoEmail(request.Email, request.emailCode);
        }*/
    }
    public class VerificarCodigoEmailRequest
    {
        public string Email { get; set; }
        public string emailCode { get; set; }
    }
    public class EnviarCodigoEmailRequest
    {
        public string Email { get; set; }
    }
    public class CambiarPswEmailRequest
    {
        public string emailCode { get; set; }
        public string email { get; set; }
        public int transaccionId { get; set; }
        public int usuarioId { get; set; }
        public string psw { get; set; }
    }

}

