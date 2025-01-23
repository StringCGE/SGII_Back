using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.mod_la_factura
{
    public class Emisor
    {
        private string _numeroRuc;
        private string _razonSocial;
        private string _nombreComercial;
        private string _direccionEstablecimientoMatriz;
        private string _direccionEstablecimientoEmisor;
        private string _codigoEstablecimientoEmisor;
        private string _codigoPuntoEmision;
        private string _contribuyenteEspecial;
        private string _obligadoContabilidad;
        private string _logoEmisor;
        private string _tipoAmbiente;
        private string _tipoEmision;

        public string NumeroRuc
        {
            get { return _numeroRuc; }
            set
            {
                if (value.Length == 13 && long.TryParse(value, out _))
                    _numeroRuc = value;
                else
                    throw new ArgumentException("Número de RUC debe ser numérico y tener 13 caracteres.");
            }
        }

        public string RazonSocial
        {
            get { return _razonSocial; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 300)
                    _razonSocial = value;
                else
                    throw new ArgumentException("Razón Social debe tener un máximo de 300 caracteres.");
            }
        }

        public string NombreComercial
        {
            get { return _nombreComercial; }
            set
            {
                if (value.Length <= 300)
                    _nombreComercial = value;
            }
        }

        public string DireccionEstablecimientoMatriz
        {
            get { return _direccionEstablecimientoMatriz; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 300)
                    _direccionEstablecimientoMatriz = value;
                else
                    throw new ArgumentException("Dirección del Establecimiento Matriz es obligatoria y debe tener un máximo de 300 caracteres.");
            }
        }

        public string DireccionEstablecimientoEmisor
        {
            get { return _direccionEstablecimientoEmisor; }
            set
            {
                if (value.Length <= 300)
                    _direccionEstablecimientoEmisor = value;
            }
        }

        public string CodigoEstablecimientoEmisor
        {
            get { return _codigoEstablecimientoEmisor; }
            set
            {
                if (value.Length == 3 && int.TryParse(value, out _))
                    _codigoEstablecimientoEmisor = value;
                else
                    throw new ArgumentException("Código del Establecimiento Emisor debe ser numérico y tener 3 caracteres.");
            }
        }

        public string CodigoPuntoEmision
        {
            get { return _codigoPuntoEmision; }
            set
            {
                if (value.Length == 3 && int.TryParse(value, out _))
                    _codigoPuntoEmision = value;
                else
                    throw new ArgumentException("Código del Punto de Emisión debe ser numérico y tener 3 caracteres.");
            }
        }

        public string ContribuyenteEspecial
        {
            get { return _contribuyenteEspecial; }
            set
            {
                if (value.Length >= 3 && value.Length <= 5 && int.TryParse(value, out _))
                    _contribuyenteEspecial = value;
            }
        }

        public string ObligadoContabilidad
        {
            get { return _obligadoContabilidad; }
            set
            {
                if (value == "SI" || value == "NO")
                    _obligadoContabilidad = value;
            }
        }

        public string LogoEmisor
        {
            get { return _logoEmisor; }
            set
            {
                _logoEmisor = value; // No validation for logo (image type, etc.)
            }
        }

        public string TipoAmbiente
        {
            get { return _tipoAmbiente; }
            set
            {
                if (value.Length == 1 && int.TryParse(value, out _))
                    _tipoAmbiente = value;
                else
                    throw new ArgumentException("Tipo de Ambiente debe ser numérico de 1 carácter.");
            }
        }

        public string TipoEmision
        {
            get { return _tipoEmision; }
            set
            {
                if (value.Length == 1 && int.TryParse(value, out _))
                    _tipoEmision = value;
                else
                    throw new ArgumentException("Tipo de Emisión debe ser numérico de 1 carácter.");
            }
        }

        public bool ComprobarCompleto()
        {
            return !string.IsNullOrEmpty(_numeroRuc) && !string.IsNullOrEmpty(_razonSocial)
                && !string.IsNullOrEmpty(_direccionEstablecimientoMatriz) && !string.IsNullOrEmpty(_codigoEstablecimientoEmisor)
                && !string.IsNullOrEmpty(_codigoPuntoEmision) && !string.IsNullOrEmpty(_tipoAmbiente)
                && !string.IsNullOrEmpty(_tipoEmision);
        }
    }
}
