
using SGII_Back.Dominio.Shared;
using SGII_Back.Dominio.Shared.Entities;

namespace SGII_Back.Dominio.DTO.AppUsuario.FilterDto
{
    /// <summary>
    /// Filtro de AppUsuario
    /// </summary>
    public class AppUsuarioFilter : RequestPaginated
    {
        /// <summary>
        /// Pnombre
        /// </summary>
        public string? Pnombre { get; set; }
        /// <summary>
        /// Snombre
        /// </summary>
        public string? Snombre { get; set; }
        /// <summary>
        /// Papellido
        /// </summary>
        public string? Papellido { get; set; }
        /// <summary>
        /// Sapellido
        /// </summary>
        public string? Sapellido { get; set; }
        /// <summary>
        /// Cedula
        /// </summary>
        public string? Cedula { get; set; }
        /// <summary>
        /// Nacimiento
        /// </summary>
        public DateTime? Nacimiento { get; set; }
    }
}