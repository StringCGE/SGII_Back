using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar tipoIdentificacion
    /// </summary>
    public class ClsTipoIdentificacion : ClsDbObj
    {
        /// <summary>
        /// variable Nombre
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        /// variable Detalle
        /// </summary>
        public string? detalle { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsNacionalidad? pais { get; set; }
    }
    
    public class FetchDataTipoIdentificacion : FetchData
    {
        /// <summary>
        /// variable Nombre
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        /// variable Detalle
        /// </summary>
        public string? detalle { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsNacionalidad? pais { get; set; }
    }
}
