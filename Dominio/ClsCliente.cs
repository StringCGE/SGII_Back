using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar cliente
    /// </summary>
    public class ClsCliente : ClsDbObj
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsPersona? persona { get; set; }
        /// <summary>
        /// variable Identificacion
        /// </summary>
        public string? identificacion { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsTipoIdentificacion? tipoIdentificacion { get; set; }
        /// <summary>
        /// variable Telefono
        /// </summary>
        public string? telefono { get; set; }
    }
    
    public class FetchDataCliente : FetchData
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsPersona? persona { get; set; }
        /// <summary>
        /// variable Identificacion
        /// </summary>
        public string? identificacion { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsTipoIdentificacion? tipoIdentificacion { get; set; }
        /// <summary>
        /// variable Telefono
        /// </summary>
        public string? telefono { get; set; }
    }
}
