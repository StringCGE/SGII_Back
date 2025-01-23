using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.mod_la_factura
{
    public class ClaveAcceso
    {
        private string _fechaEmision = string.Empty;
        private string _tipoComprobante = string.Empty;
        private string _numeroRUC = string.Empty;
        private string _tipoAmbiente = string.Empty;
        private string _serie = string.Empty;
        private string _numeroComprobante = string.Empty;
        private string _codigoNumerico = string.Empty;
        private string _tipoEmision = string.Empty;
        private string _digitoVerificador = string.Empty;

        public string FechaEmision
        {
            get => _fechaEmision;
            set
            {
                if (!IsValidNumeric(value, 8)) throw new ArgumentException("Fecha de emisión debe ser numérica y de 8 caracteres.");
                _fechaEmision = value;
            }
        }

        public string TipoComprobante
        {
            get => _tipoComprobante;
            set
            {
                if (!IsValidComprobante(value)) throw new ArgumentException("Tipo de comprobante inválido.");
                _tipoComprobante = value;
            }
        }

        public string NumeroRUC
        {
            get => _numeroRUC;
            set
            {
                if (!IsValidNumeric(value, 13)) throw new ArgumentException("Número de RUC debe ser numérico y de 13 caracteres.");
                _numeroRUC = value;
            }
        }

        public string TipoAmbiente
        {
            get => _tipoAmbiente;
            set
            {
                if (!IsValidAmbiente(value)) throw new ArgumentException("Tipo de ambiente inválido.");
                _tipoAmbiente = value;
            }
        }

        public string Serie
        {
            get => _serie;
            set
            {
                if (!IsValidNumeric(value, 6)) throw new ArgumentException("Serie debe ser numérica y de 6 caracteres.");
                _serie = value;
            }
        }

        public string NumeroComprobante
        {
            get => _numeroComprobante;
            set
            {
                if (!IsValidNumeric(value, 9)) throw new ArgumentException("Número de comprobante debe ser numérico y de 9 caracteres.");
                _numeroComprobante = value;
            }
        }

        public string CodigoNumerico
        {
            get => _codigoNumerico;
            set
            {
                if (!IsValidNumeric(value, 8)) throw new ArgumentException("Código numérico debe ser numérico y de 8 caracteres.");
                _codigoNumerico = value;
            }
        }

        public string TipoEmision
        {
            get => _tipoEmision;
            set
            {
                if (!IsValidTipoEmision(value)) throw new ArgumentException("Tipo de emisión inválido.");
                _tipoEmision = value;
            }
        }

        public string DigitoVerificador
        {
            get => _digitoVerificador;
            private set => _digitoVerificador = value;
        }

        public void GenerarDigitoVerificador()
        {
            string codigo = GetCodigoPross();
            int suma = 0;
            int peso = 2;

            for (int i = codigo.Length - 1; i >= 0; i--)
            {
                suma += (codigo[i] - '0') * peso;
                peso = peso == 7 ? 2 : peso + 1;
            }

            int modulo = 11 - (suma % 11);
            DigitoVerificador = modulo == 11 ? "0" : modulo == 10 ? "1" : modulo.ToString();
        }

        private string GetCodigoPross()
        {
            return FechaEmision + TipoComprobante + NumeroRUC + TipoAmbiente + Serie + NumeroComprobante + CodigoNumerico + TipoEmision;
        }

        public string GetCodigo()
        {
            return GetCodigoPross() + DigitoVerificador;
        }

        public void SetCodigo(string codigo)
        {
            if (codigo.Length != 49) throw new ArgumentException("El código debe tener 49 caracteres.");

            FechaEmision = codigo.Substring(0, 8);
            TipoComprobante = codigo.Substring(8, 2);
            NumeroRUC = codigo.Substring(10, 13);
            TipoAmbiente = codigo.Substring(23, 1);
            Serie = codigo.Substring(24, 6);
            NumeroComprobante = codigo.Substring(30, 9);
            CodigoNumerico = codigo.Substring(39, 8);
            TipoEmision = codigo.Substring(47, 1);
            DigitoVerificador = codigo.Substring(48, 1);
        }

        private bool IsValidNumeric(string value, int length)
        {
            return value.Length == length && long.TryParse(value, out _);
        }

        private bool IsValidComprobante(string value)
        {
            var validComprobantes = new[] { "01", "03", "04", "05", "06", "07" };
            return validComprobantes.Contains(value);
        }

        private bool IsValidAmbiente(string value)
        {
            var validAmbientes = new[] { "1" };
            return validAmbientes.Contains(value);
        }

        private bool IsValidTipoEmision(string value)
        {
            var validTipos = new[] { "1" };
            return validTipos.Contains(value);
        }
    }
}
