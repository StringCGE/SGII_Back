using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar itemFactura
    /// </summary>
    public class ClsItemFactura : ClsDbObj
    {
        /// <summary>
        /// variable Cantidad
        /// </summary>
        public int? cantidad { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsProducto? producto { get; set; }
        /// <summary>
        /// variable PrecioUnitario
        /// </summary>
        public double? precioUnitario { get; set; }
        /// <summary>
        /// variable Total
        /// </summary>
        public double? total { get; set; }
    }
    
    public class FetchDataItemFactura : FetchData
    {
        /// <summary>
        /// variable Cantidad
        /// </summary>
        public int? cantidad { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsProducto? producto { get; set; }
        /// <summary>
        /// variable PrecioUnitario
        /// </summary>
        public double? precioUnitario { get; set; }
        /// <summary>
        /// variable Total
        /// </summary>
        public double? total { get; set; }
    }
}
