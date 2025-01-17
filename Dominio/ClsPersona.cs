using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ClsPersona : ClsDbObj
    {
        /// <summary>
        /// Primer nombre de la prsona
        /// </summary>
        public string? nombre1 { get; set; }
        /// <summary>
        /// Segundo nombre de la persona
        /// </summary>
        public string? nombre2 { get; set; }
        /// <summary>
        /// Primer apellido de la persona
        /// </summary>
        public string? apellido1 { get; set; }
        /// <summary>
        /// Segundo apellido de la persona
        /// </summary>
        public string? apellido2 { get; set; }
        /// <summary>
        /// Fecha de nacimiento de la persona
        /// </summary>
        public DateTime? fechaNacimiento { get; set; }
        /// <summary>
        /// Cedula de la persona
        /// </summary>
        public string? cedula { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsSexo? sexo { get; set; }
        /// <summary>
        /// estado civil de la persona
        /// </summary>
        public ClsEstadoCivil? estadoCivil { get; set; }
        /// <summary>
        /// Nacionalidad de la persona
        /// </summary>
        public ClsNacionalidad? nacionalidad { get; set; }
        /// <summary>
        /// Grupo sanguineo de la persona
        /// </summary>
        public string? grupoSanguineo { get; set; }
        /// <summary>
        /// Tipo sanguineo de la persona
        /// </summary>
        public string? tipoSanguineo { get; set; }
    }

    public class FetchDataPersona : FetchData
    {
        /// <summary>
        /// Primer nombre de la prsona
        /// </summary>
        public string? nombre1 { get; set; }
        /// <summary>
        /// Segundo nombre de la persona
        /// </summary>
        public string? nombre2 { get; set; }
        /// <summary>
        /// Primer apellido de la persona
        /// </summary>
        public string? apellido1 { get; set; }
        /// <summary>
        /// Segundo apellido de la persona
        /// </summary>
        public string? apellido2 { get; set; }
        /// <summary>
        /// Fecha de nacimiento de la persona
        /// </summary>
        public DateTime? fechaNacimiento { get; set; }
        /// <summary>
        /// Cedula de la persona
        /// </summary>
        public string? cedula { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public List<ClsSexo>? sexo { get; set; }
        /// <summary>
        /// estado civil de la persona
        /// </summary>
        public List<ClsEstadoCivil>? estadoCivil { get; set; }
        /// <summary>
        /// Nacionalidad de la persona
        /// </summary>
        public List<ClsNacionalidad>? nacionalidad { get; set; }
        /// <summary>
        /// Grupo sanguineo de la persona
        /// </summary>
        public string? grupoSanguineo { get; set; }
        /// <summary>
        /// Tipo sanguineo de la persona
        /// </summary>
        public string? tipoSanguineo { get; set; }
    }




    /*
    public class ClsPersona
    {
        public int? id { get; set; }//no filtro
        public int? idApi { get; set; }//no filtro
        public DateTime? dtReg { get; set; }//no filtro
        public int? idPersReg { get; set; }//no filtro
        public int? estado { get; set; }//no filtro pero que sea diferente de 0
        public string? nombre1 { get; set; }
        public string? nombre2 { get; set; }
        public string? apellido1 { get; set; }
        public string? apellido2 { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string? cedula { get; set; }
        public ClsSexo? sexo { get; set; }
        public ClsEstadoCivil? estadoCivil { get; set; }
        public ClsNacionalidad? nacionalidad { get; set; }
        public string? grupoSanguineo { get; set; }
        public string? tipoSanguineo { get; set; }
    }

    public class ClsSexo
    {
        public int? id { get; set; }//no filtro
        public int? idApi { get; set; }//no filtro
        public DateTime? dtReg { get; set; }//no filtro
        public int? idPersReg { get; set; }//no filtro pero que sea diferente de 0
        public int? estado { get; set; }
        public string? nombre { get; set; }
    }
    public class ClsNacionalidad
    {
        public int? id { get; set; }//no filtro
        public int? idApi { get; set; }//no filtro
        public DateTime? dtReg { get; set; }//no filtro
        public int? idPersReg { get; set; }//no filtro
        public int? estado { get; set; }//no filtro pero que sea diferente de 0
        public string? nombre { get; set; }
        public string? detalle { get; set; }
    }

    
    public class ClsEstadoCivil
    {
        public int? id { get; set; }//no filtro
        public int? idApi { get; set; }//no filtro
        public DateTime? dtReg { get; set; }//no filtro
        public int? idPersReg { get; set; }//no filtro
        public int? estado { get; set; }//no filtro pero que sea diferente de 0
        public string? nombre { get; set; }
        public string? detalle { get; set; }
    }
    
    */
}
