using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ClsSexo : ClsDbObj
    {
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? nombre { get; set; }
    }

    public class FetchDataSexo : FetchData
    {
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? nombre { get; set; }
    }
    /*
    public class ClsSexo
    {
        public int? id { get; set; }
        public int? idApi { get; set; }
        public DateTime? dtReg { get; set; }
        public int? idPersReg { get; set; }
        public int? estado { get; set; }
        public string? nombre { get; set; }
    }
    */
}
