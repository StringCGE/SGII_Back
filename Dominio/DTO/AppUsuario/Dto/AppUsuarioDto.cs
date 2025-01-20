
using SGII_Back.Dominio.DTO.AppUsuario.Request;

namespace SGII_Back.Dominio.DTO.AppUsuario.Dto
{
    /// <summary>
    /// Dto de AppUsuario
    /// </summary>
    public class AppUsuarioDto : AppUsuarioRequest
    {
        /// <summary>
        /// Pnombre
        /// </summary>
        public string Pnombre { get; set; }
        /// <summary>
        /// Snombre
        /// </summary>
        public string Snombre { get; set; }
        /// <summary>
        /// Papellido
        /// </summary>
        public string Papellido { get; set; }
        /// <summary>
        /// Sapellido
        /// </summary>
        public string Sapellido { get; set; }
        /// <summary>
        /// Cedula
        /// </summary>
        public string Cedula { get; set; }
        /// <summary>
        /// Nacimiento
        /// </summary>
        public DateTime Nacimiento { get; set; }
    }
}
