using System.ComponentModel.DataAnnotations;
using Dominio;

namespace SGII_Back.Application.DTO.UserDTO;

public class UserUpdate
{
    public required ClsPersona persona { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string urlFoto { get; set; }
    public required string role { get; set; }
}




