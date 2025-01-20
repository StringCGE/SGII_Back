using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar proveedor
    /// </summary>
    public class ClsProveedor : ClsDbObj
    {
        /// <summary>
        /// variable RazonSocial
        /// </summary>
        public string? razonSocial { get; set; }
        /// <summary>
        /// variable Ruc
        /// </summary>
        public string? ruc { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsPersona? responsable { get; set; }
        /// <summary>
        /// variable TelefonoResponsable
        /// </summary>
        public string? telefonoResponsable { get; set; }
        /// <summary>
        /// variable DireccionMatriz
        /// </summary>
        public string? direccionMatriz { get; set; }
        /// <summary>
        /// variable Telefono1
        /// </summary>
        public string? telefono1 { get; set; }
        /// <summary>
        /// variable Telefono2
        /// </summary>
        public string? telefono2 { get; set; }
        /// <summary>
        /// variable Email
        /// </summary>
        public string? email { get; set; }
    }
    
    public class FetchDataProveedor : FetchData
    {
        /// <summary>
        /// variable RazonSocial
        /// </summary>
        public string? razonSocial { get; set; }
        /// <summary>
        /// variable Ruc
        /// </summary>
        public string? ruc { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsPersona? responsable { get; set; }
        /// <summary>
        /// variable TelefonoResponsable
        /// </summary>
        public string? telefonoResponsable { get; set; }
        /// <summary>
        /// variable DireccionMatriz
        /// </summary>
        public string? direccionMatriz { get; set; }
        /// <summary>
        /// variable Telefono1
        /// </summary>
        public string? telefono1 { get; set; }
        /// <summary>
        /// variable Telefono2
        /// </summary>
        public string? telefono2 { get; set; }
        /// <summary>
        /// variable Email
        /// </summary>
        public string? email { get; set; }
    }
}
