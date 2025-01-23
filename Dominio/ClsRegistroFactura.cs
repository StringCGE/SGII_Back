using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar registroFactura
    /// </summary>
    public class ClsRegistroFactura : ClsDbObj
    {
        /// <summary>
        /// variable Secuencial
        /// </summary>
        public string? secuencial { get; set; }
        /// <summary>
        /// variable RazonSocial
        /// </summary>
        public string? razonSocial { get; set; }
        /// <summary>
        /// variable Identificacion
        /// </summary>
        public string? identificacion { get; set; }
        /// <summary>
        /// variable FechaEmision
        /// </summary>
        public string? fechaEmision { get; set; }
        /// <summary>
        /// variable NumeroGuiaRemision
        /// </summary>
        public string? numeroGuiaRemision { get; set; }
        /// <summary>
        /// variable CodigoNumerico
        /// </summary>
        public string? codigoNumerico { get; set; }
        /// <summary>
        /// variable Verificador
        /// </summary>
        public string? verificador { get; set; }
    }
    
    public class FetchDataRegistroFactura : FetchData
    {
        /// <summary>
        /// variable Secuencial
        /// </summary>
        public string? secuencial { get; set; }
        /// <summary>
        /// variable RazonSocial
        /// </summary>
        public string? razonSocial { get; set; }
        /// <summary>
        /// variable Identificacion
        /// </summary>
        public string? identificacion { get; set; }
        /// <summary>
        /// variable FechaEmision
        /// </summary>
        public string? fechaEmision { get; set; }
        /// <summary>
        /// variable NumeroGuiaRemision
        /// </summary>
        public string? numeroGuiaRemision { get; set; }
        /// <summary>
        /// variable CodigoNumerico
        /// </summary>
        public string? codigoNumerico { get; set; }
        /// <summary>
        /// variable Verificador
        /// </summary>
        public string? verificador { get; set; }
    }
}
