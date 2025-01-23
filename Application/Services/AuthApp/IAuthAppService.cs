using SGII_Back.Application.DTO.AuthDTO;

namespace SGII_Back.Application.Services.AuthApp;

public interface IAuthAppService
{
    Task<AuthDTO> Login(AuthRequest login);
    Task<ResetPasswordDTO> SendCodeToResetPassword(SendCodeToResetPasswordRequest login);
    Task<ResetPasswordDTO> ResetPassword(ResetPasswordRequest login);
}
