using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar producto
    /// </summary>
    public class ClsProducto : ClsDbObj
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisor? proveedor { get; set; }
        /// <summary>
        /// variable Nombre
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        /// variable Detalle
        /// </summary>
        public string? detalle { get; set; }
        /// <summary>
        /// variable Precio
        /// </summary>
        public double? precio { get; set; }
        /// <summary>
        /// variable Cantidad
        /// </summary>
        public int? cantidad { get; set; }
    }
    
    public class FetchDataProducto : FetchData
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisor? proveedor { get; set; }
        /// <summary>
        /// variable Nombre
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        /// variable Detalle
        /// </summary>
        public string? detalle { get; set; }
        /// <summary>
        /// variable Precio
        /// </summary>
        public double? precio { get; set; }
        /// <summary>
        /// variable Cantidad
        /// </summary>
        public int? cantidad { get; set; }
    }
}
