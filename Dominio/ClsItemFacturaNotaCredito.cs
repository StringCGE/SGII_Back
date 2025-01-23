using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar itemFacturaNotaCredito
    /// </summary>
    public class ClsItemFacturaNotaCredito : ClsDbObj
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsFacturaNotaCredito? facturaNotaCredito { get; set; }
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
        /// <summary>
        /// variable TipoTransac
        /// </summary>
        public int? tipoTransac { get; set; }
    }
    
    public class FetchDataItemFacturaNotaCredito : FetchData
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsFacturaNotaCredito? facturaNotaCredito { get; set; }
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
        /// <summary>
        /// variable TipoTransac
        /// </summary>
        public int? tipoTransac { get; set; }
    }
}
