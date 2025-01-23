using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar emisorEstablecimiento
    /// </summary>
    public class ClsEmisorEstablecimiento : ClsDbObj
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisor? emisor { get; set; }
        /// <summary>
        /// variable Numero
        /// </summary>
        public int? numero { get; set; }
        /// <summary>
        /// variable Nombre
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        /// variable Direccion
        /// </summary>
        public string? direccion { get; set; }
        /// <summary>
        /// variable PuntosDeEmision
        /// </summary>
        public string? puntosDeEmision { get; set; }
    }
    
    public class FetchDataEmisorEstablecimiento : FetchData
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisor? emisor { get; set; }
        /// <summary>
        /// variable Numero
        /// </summary>
        public int? numero { get; set; }
        /// <summary>
        /// variable Nombre
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        /// variable Direccion
        /// </summary>
        public string? direccion { get; set; }
        /// <summary>
        /// variable PuntosDeEmision
        /// </summary>
        public string? puntosDeEmision { get; set; }
    }
}
