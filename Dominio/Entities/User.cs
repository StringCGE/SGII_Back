using Dominio;
using SGII_Back.Dominio.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGII_Back.Dominio.Entities;

[Table("Users")]
public class User : BaseEntity<int>
{
    public required ClsPersona persona { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string urlFoto { get; set; }
    public required string role { get; set; }
    public string Salt { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? TempCode { get; set; } = string.Empty;
    public DateTime? TempCodeCreateAt { get; set; }
}