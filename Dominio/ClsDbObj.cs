using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase base para soportar un registro basico de actividades
    /// </summary>
    public class ClsDbObj : Object //Sera implicito pero a revicion de ojo de alcon mejor ver estacio lleno que espaxio por rellenar
    {
        /// <summary>
        /// Id de referencia desde cualquier front
        /// </summary>
        public int? id { get; set; }
        /// <summary>
        /// Id en api es decir id de la base de datos en sistema centralizado
        /// </summary>
        public int? idApi { get; set; }
        /// <summary>
        /// Fecha de registro en el front
        /// </summary>
        public DateTime? dtReg { get; set; }
        /// <summary>
        /// Id de la persona que registra, si es cero forma parte del primer registro 
        /// </summary>
        public int? idPersReg { get; set; }
        /// <summary>
        /// Id de referencia desde cualquier front
        /// </summary>
        public int? estado { get; set; }

    }


    /// <summary>
    /// Clase base para soportar un registro basico de actividades
    /// </summary>
    public class FetchData : ClsDbObj
    {
        /// <summary>
        /// cantidad de datos a obtener
        /// </summary>
        public int take { get; set; }
        /// <summary>
        /// offset por fecha y hora
        /// </summary>
        public DateTime offsetDT { get; set; }

    }
}
