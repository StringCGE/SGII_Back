using SGII_Back.Dominio.Shared;
using SGII_Back.Dominio.Shared.Entities;
using SGII_Back.Dominio.Shared.Interfaces;

using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace SGII_Back.Dominio;
public class BaseEntity<T> : BaseEntity
{
    [Key]

    public required T Id { get; set; }
}
public class BaseEntity : AuditEntity, IEntity
{
    public bool Active { get; set; }

}