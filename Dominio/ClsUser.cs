using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar user
    /// </summary>
    public class ClsUser : ClsDbObj
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsPersona? persona { get; set; }
        /// <summary>
        /// variable Email
        /// </summary>
        public string? email { get; set; }
        /// <summary>
        /// variable UrlFoto
        /// </summary>
        public string? urlFoto { get; set; }
        /// <summary>
        /// variable Role
        /// </summary>
        public string? role { get; set; }
        /// <summary>
        /// variable Password
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// variable Salt
        /// </summary>
        public string? salt { get; set; }
        /// <summary>
        /// variable TempCode
        /// </summary>
        public string? tempCode { get; set; }
        /// <summary>
        /// variable TempCodeCreateAt
        /// </summary>
        public DateTime? tempCodeCreateAt { get; set; }
    }
    
    public class FetchDataUser : FetchData
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsPersona? persona { get; set; }
        /// <summary>
        /// variable Email
        /// </summary>
        public string? email { get; set; }
        /// <summary>
        /// variable UrlFoto
        /// </summary>
        public string? urlFoto { get; set; }
        /// <summary>
        /// variable Role
        /// </summary>
        public string? role { get; set; }
        /// <summary>
        /// variable Password
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// variable Salt
        /// </summary>
        public string? salt { get; set; }
        /// <summary>
        /// variable TempCode
        /// </summary>
        public string? tempCode { get; set; }
        /// <summary>
        /// variable TempCodeCreateAt
        /// </summary>
        public DateTime? tempCodeCreateAt { get; set; }
    }
}
