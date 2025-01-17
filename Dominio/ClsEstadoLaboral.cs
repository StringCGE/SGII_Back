using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ClsEstadoLaboral : ClsDbObj
    {
        /// <summary>
        /// Nombre del estado civil
        /// </summary>
        public string? nombre { get; set; }
    }

    public class FetchDataEstadoLaboral : FetchData
    {
        /// <summary>
        /// Nombre del estado civil
        /// </summary>
        public string? nombre { get; set; }
    }

    /*
    public class ClsEstadoLaboral
    {
        public int? id { get; set; }
        public int? idApi { get; set; }
        public DateTime? dtReg { get; set; }
        public int? idPersReg { get; set; }
        public int? estado { get; set; }
        public string? nombre { get; set; }
        public string? detalle { get; set; }
    }
    */
}

