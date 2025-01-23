using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar registroDoc
    /// </summary>
    public class ClsRegistroDoc : ClsDbObj
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
        /// <summary>
        /// variable DenomComproModif
        /// </summary>
        public int? denomComproModif { get; set; }
        /// <summary>
        /// variable NumComproModif
        /// </summary>
        public string? numComproModif { get; set; }
        /// <summary>
        /// variable ComproModif
        /// </summary>
        public int? comproModif { get; set; }
    }
    
    public class FetchDataRegistroDoc : FetchData
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
        /// <summary>
        /// variable DenomComproModif
        /// </summary>
        public int? denomComproModif { get; set; }
        /// <summary>
        /// variable NumComproModif
        /// </summary>
        public string? numComproModif { get; set; }
        /// <summary>
        /// variable ComproModif
        /// </summary>
        public int? comproModif { get; set; }
    }
}
