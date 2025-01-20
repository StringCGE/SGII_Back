using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SGII_Back.Application.Services.AuthApp;
using SGII_Back.Application.DTO.AuthDTO;
using Application.Services.AuthApp;

namespace SGII_Back.Controllers.auth;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{

    private readonly IAuthAppService _authService;

    public AuthController(IAuthAppService authService)
    {
        _authService = authService;
    }

        [HttpPost()]
    public IActionResult Login([FromBody] AuthRequest login)
    {

        var _auth = _authService.Login(login);
        if (_auth.Autenticate)
            return Ok(_auth);

        return Ok();// n Unauthorized(_auth);
    }

    [HttpPost("ResetPassword")]
    public IActionResult ResetPassword([FromBody] ResetPasswordRequest login)
    {
        /*var response = _authService.ResetPassword(login);

        if (response.success)
            return Ok(new { message = response.message });

        return BadRequest(new { message = response.message });*/
        return Ok();

    }

    [HttpPost("SendCodeToResetPassword")]
    public async Task<IActionResult> SendCodeToResetPassword([FromBody] SendCodeToResetPasswordRequest login)
    {

        /*var response = await _authService.SendCodeToResetPassword(login);
        if (response.success)
            return Ok(new { message = response.message });

        return BadRequest(new { message = response.message });*/
        return Ok();
    }

}