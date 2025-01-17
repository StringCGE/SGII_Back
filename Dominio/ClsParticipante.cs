using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ClsParticipante : ClsDbObj
    {
        /// <summary>
        /// Datos de la persona
        /// </summary>
        public ClsPersona? persona { get; set; }
        /// <summary>
        /// Si existe contrato, existe ruta del archivo
        /// </summary>
        public string? detalle { get; set; }
        /// <summary>
        /// Horas regulares al dua de trabajo, aproximado para casos de voluntariado
        /// </summary>
        public int? horasTrabajadas { get; set; }
        /// <summary>
        /// Sonb las funciones que ese particioante va a cumplir en donde este registrada su participacion
        /// </summary>
        public List<ClsFuncionParticipante> funciones { get; set; }

    }

    public class FetchDataParticipante : FetchData
    {
        /// <summary>
        /// Datos de la persona
        /// </summary>
        public FetchDataPersona? persona { get; set; }
        /// <summary>
        /// Si existe contrato, existe ruta del archivo
        /// </summary>
        public string? detalle { get; set; }
        /// <summary>
        /// Horas regulares al dua de trabajo, aproximado para casos de voluntariado
        /// </summary>
        public int? horasTrabajadas { get; set; }
        /// <summary>
        /// Sonb las funciones que ese particioante va a cumplir en donde este registrada su participacion
        /// </summary>
        public List<ClsFuncionParticipante>? funciones { get; set; }

    }

    /*
    public class ClsParticipante
    {
        public int? id { get; set; }
        public int? idApi { get; set; }
        public DateTime? dtReg { get; set; }
        public int? idPersReg { get; set; }
        public int? estado { get; set; }
        public ClsPersona? persona { get; set; }
        public string? detalle { get; set; }
        public int? horasTrabajadas { get; set; }
        public List<ClsFuncionParticipante> funciones { get; set; }
    }
    */
}
