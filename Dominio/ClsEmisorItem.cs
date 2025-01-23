using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar emisorItem
    /// </summary>
    public class ClsEmisorItem : ClsDbObj
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisor? emisor { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisorEstablecimiento? emisorEstablecimiento { get; set; }
        /// <summary>
        /// variable PuntoEmision
        /// </summary>
        public string? puntoEmision { get; set; }
    }
    
    public class FetchDataEmisorItem : FetchData
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisor? emisor { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisorEstablecimiento? emisorEstablecimiento { get; set; }
        /// <summary>
        /// variable PuntoEmision
        /// </summary>
        public string? puntoEmision { get; set; }
    }
}
