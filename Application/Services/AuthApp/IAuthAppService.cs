using SGII_Back.Application.DTO.AuthDTO;

namespace SGII_Back.Application.Services.AuthApp;

public interface IAuthAppService
{
    AuthDTO Login(AuthRequest login);
    Task<ResetPasswordDTO> SendCodeToResetPassword(SendCodeToResetPasswordRequest login);
    ResetPasswordDTO ResetPassword(ResetPasswordRequest login);
}
