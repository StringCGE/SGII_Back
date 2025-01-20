
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SGII_Back.Dominio.DTO.AppUsuario.Request
{
    /// <summary>
    /// Request de AppUsuario
    /// </summary>
    public class AppUsuarioRequest
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


    /// <summary>
    /// RequestAllData de AppUsuario
    /// </summary>
    public class AppUsuarioRequestAllData
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