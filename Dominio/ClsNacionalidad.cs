using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ClsNacionalidad : ClsDbObj
    {
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? nombre { get; set; }
    }

    public class FetchDataNacionalidad : FetchData
    {
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? nombre { get; set; }
    }
}
