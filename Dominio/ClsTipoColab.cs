using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Hace reerencia al tipo de colaborador que puede ser, Contrato, Voluntario, Temporal
    /// </summary>
    public class ClsTipoColab : ClsDbObj
    {
        /// <summary>
        /// Nombre del tipo de colaborador
        /// </summary>
        public string? nombre { get; set; }
        /// <summary>
        /// Detalle de esta relacion con la fundacion
        /// </summary>
        public string? detalle { get; set; }
    }

    public class FetchDataTipoColab : FetchData
    {
        public string? nombre { get; set; }
    }
}
