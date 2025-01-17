using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Un colaboador va a tener un cargo o funcion
    /// </summary>
    public class ClsCargo : ClsDbObj
    {
        /// <summary>
        /// Nombre del cargo o funcion
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        /// Detalle de las actividades o funcion que debe cumplir o alguna reseña del cargo
        /// </summary>
        public string? detalle { get; set; }
    }

    public class FetchDataCargo : FetchData
    {
        public string? nombre { get; set; }
    }

    /*
    public class ClsCargo
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
