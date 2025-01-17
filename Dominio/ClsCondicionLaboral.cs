using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ClsCondicionLaboral : ClsDbObj
    {
        /// <summary>
        /// Nombre del estado civil
        /// </summary>
        public string? nombre { get; set; }
    }

    public class FetchDataCondicionLaboral : FetchData
    {
        /// <summary>
        /// Nombre del estado civil
        /// </summary>
        public string? nombre { get; set; }
    }
}
