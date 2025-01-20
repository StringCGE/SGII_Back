using SGII_Back.Dominio.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SGII_Back.Dominio.Shared.Entities;

public class AuditEntity : IAudit
{
    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public int? UpdatedBy { get; set; }
}

