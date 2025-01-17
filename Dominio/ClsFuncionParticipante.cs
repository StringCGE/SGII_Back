using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ClsFuncionParticipante : ClsDbObj
    {
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? nombre { get; set; }

        /// <summary>
        /// Detalle de las actividades o funcion que debe cumplir o alguna reseña del cargo
        /// </summary>
        public string? detalle { get; set; }
    }

    public class FetchDataFuncionParticipante : FetchData
    {
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? nombre { get; set; }
    }


}
