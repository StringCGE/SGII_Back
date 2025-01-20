using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar persona
    /// </summary>
    public class ClsPersona : ClsDbObj
    {
        /// <summary>
        /// variable Nombre1
        /// </summary>
        public string? nombre1 { get; set; }
        /// <summary>
        /// variable Nombre2
        /// </summary>
        public string? nombre2 { get; set; }
        /// <summary>
        /// variable Apellido1
        /// </summary>
        public string? apellido1 { get; set; }
        /// <summary>
        /// variable Apellido2
        /// </summary>
        public string? apellido2 { get; set; }
        /// <summary>
        /// variable FechaNacimiento
        /// </summary>
        public DateTime? fechaNacimiento { get; set; }
        /// <summary>
        /// variable Cedula
        /// </summary>
        public string? cedula { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsSexo? sexo { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEstadoCivil? estadoCivil { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsNacionalidad? nacionalidad { get; set; }
        /// <summary>
        /// variable GrupoSanguineo
        /// </summary>
        public string? grupoSanguineo { get; set; }
        /// <summary>
        /// variable TipoSanguineo
        /// </summary>
        public string? tipoSanguineo { get; set; }
    }
    
    public class FetchDataPersona : FetchData
    {
        /// <summary>
        /// variable Nombre1
        /// </summary>
        public string? nombre1 { get; set; }
        /// <summary>
        /// variable Nombre2
        /// </summary>
        public string? nombre2 { get; set; }
        /// <summary>
        /// variable Apellido1
        /// </summary>
        public string? apellido1 { get; set; }
        /// <summary>
        /// variable Apellido2
        /// </summary>
        public string? apellido2 { get; set; }
        /// <summary>
        /// variable FechaNacimiento
        /// </summary>
        public DateTime? fechaNacimiento { get; set; }
        /// <summary>
        /// variable Cedula
        /// </summary>
        public string? cedula { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsSexo? sexo { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEstadoCivil? estadoCivil { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsNacionalidad? nacionalidad { get; set; }
        /// <summary>
        /// variable GrupoSanguineo
        /// </summary>
        public string? grupoSanguineo { get; set; }
        /// <summary>
        /// variable TipoSanguineo
        /// </summary>
        public string? tipoSanguineo { get; set; }
    }
}
