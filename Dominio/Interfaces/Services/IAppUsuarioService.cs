using SGII_Back.Dominio.DTO.AppUsuario.Dto;
using SGII_Back.Dominio.DTO.AppUsuario.FilterDto;
using SGII_Back.Dominio.DTO.AppUsuario.Request;
using SGII_Back.Dominio.Shared;

namespace SGII_Back.Dominio.Interfaces.Services
{
    /// <summary>
    /// servicio de AppUsuario
    /// </summary>
    public interface IAppUsuarioService : ICrudService<IOperationRequest<AppUsuarioRequest>, AppUsuarioDto, AppUsuarioFilter, int>
    {
    }
}