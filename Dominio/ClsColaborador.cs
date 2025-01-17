using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Representa a una persona que es colaborador, puede ser parte de la fundacion o ser un voluntario es decir fijo o temporal
    /// </summary>
    public class ClsColaborador : ClsAsignacion
    {
        /*/// <summary>
        /// id de base de datos (idApi) de persona de este colaborador
        /// </summary>
        public int? persona_id { get; set; }*/
        /// <summary>
        /// Datos de la persona
        /// </summary>
        public ClsPersona? persona { get; set; }
        /*/// <summary>
        /// 
        /// </summary>
        public int? tipoColab_id { get; set; }*/
        /// <summary>
        /// 
        /// </summary>
        public ClsTipoColab? tipoColab { get; set; }
        /*/// <summary>
        /// id de base de datos (idApi) del cargo de este colaborador
        /// </summary>
        public int? cargo_id { get; set; }*/
        /// <summary>
        /// Datos del cargo que ocupa este colaborador
        /// </summary>
        public ClsCargo? cargo { get; set; }
        /*/// <summary>
        /// El estado en el que se encuentra el colaborador, No existe clase para esto, Deberia? creo que si
        /// </summary>
        public int? estadoLaboral_id { get; set; }*/
        /// <summary>
        /// Puede ser cero o ganar sueldo
        /// </summary>
        public ClsEstadoLaboral? estadoLaboral { get; set; }
        /*/// <summary>
        /// El estado en el que se encuentra el colaborador, No existe clase para esto, Deberia? creo que si
        /// </summary>
        public int? condicionLaboral_id { get; set; }*/
        /// <summary>
        /// Tipo de relacion del colaborador con la fundacion
        /// </summary>
        public ClsCondicionLaboral? condicionLaboral { get; set; }
        /// <summary>
        /// Tipo de relacion del colaborador con la fundacion
        /// </summary>
        public double? sueldo { get; set; }
        /// <summary>
        /// Horas regulares al dua de trabajo, aproximado para casos de voluntariado
        /// </summary>
        public int? horasTrabajo { get; set; }
        /// <summary>
        /// Si existe contrato, existe ruta del archivo
        /// </summary>
        public string? contrato { get; set; }
    }



    public class FetchDataColaborador : FetchData
    {
        /*/// <summary>
        /// id de base de datos (idApi) de persona de este colaborador
        /// </summary>
        public int? persona_id { get; set; }*/
        /// <summary>
        /// Datos de la persona
        /// </summary>
        public ClsPersona? Persona { get; set; }
        /*/// <summary>
        /// 
        /// </summary>
        public int? tipoColab_id { get; set; }*/
        /// <summary>
        /// 
        /// </summary>
        public ClsTipoColab? tipoColab { get; set; }
        /*/// <summary>
        /// id de base de datos (idApi) del cargo de este colaborador
        /// </summary>
        public int? cargo_id { get; set; }*/
        /// <summary>
        /// Datos del cargo que ocupa este colaborador
        /// </summary>
        public ClsCargo? cargo { get; set; }
        /*/// <summary>
        /// El estado en el que se encuentra el colaborador, No existe clase para esto, Deberia? creo que si
        /// </summary>
        public int? estadoLaboral_id { get; set; }*/
        /// <summary>
        /// Puede ser cero o ganar sueldo
        /// </summary>
        public ClsEstadoLaboral? estadoLaboral { get; set; }
        /*/// <summary>
        /// El estado en el que se encuentra el colaborador, No existe clase para esto, Deberia? creo que si
        /// </summary>
        public int? condicionLaboral_id { get; set; }*/
        /// <summary>
        /// Tipo de relacion del colaborador con la fundacion
        /// </summary>
        public ClsCondicionLaboral? condicionLaboral { get; set; }
        /// <summary>
        /// Tipo de relacion del colaborador con la fundacion
        /// </summary>
        public double? sueldo { get; set; }
        /// <summary>
        /// Horas regulares al dua de trabajo, aproximado para casos de voluntariado
        /// </summary>
        public int? horasTrabajo { get; set; }
        /// <summary>
        /// Si existe contrato, existe ruta del archivo
        /// </summary>
        public string? contrato { get; set; }
    }
}
