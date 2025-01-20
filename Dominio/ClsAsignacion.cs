using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase base para registrar los tipos de personas que se gestionan en la plataforma
    /// </summary>
    public class ClsAsignacion : ClsDbObj
    {
        /// <summary>
        /// id de base de datos (idApi) de persona que tiene esta asignacion, la verdad no debe existir esto pero en fin
        /// </summary>
        public bool? idPersona { get; set; }
        /// <summary>
        /// Instancia de la persona, aqui se puede registrar directo la existencia del ID sin los datos y lueg  los datos , El apuro
        /// </summary>
        public ClsPersona? objPersona { get; set; }
        /// <summary>
        /// Aqui se registra cuando inicia oficialmente la asignacion, pudo ser registro tardio o registro adelantado asi que no siempre coincide con fecha de registro
        /// </summary>
        public DateTime? inicioAsignacion { get; set; }
        /// <summary>
        /// Fecha en la que finaliza actividad, puede no existir fecha de termino de asignacion peor si debe eistir algun registro futuro cuando esta termine, en ese caso la fecha de inicio podra ser null indicando que existe una fecha de inicio en otro registro
        /// </summary>
        public DateTime? finAsignacion { get; set; }
    }
}
