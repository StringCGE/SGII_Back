namespace SGII_Back.Dominio.Shared.Interfaces;

public interface IAudit
{
    DateTime CreatedAt { get; set; }
    int CreatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }
    int? UpdatedBy { get; set; }
}
