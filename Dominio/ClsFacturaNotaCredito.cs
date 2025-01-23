using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Clase para registrar facturaNotaCredito
    /// </summary>
    public class ClsFacturaNotaCredito : ClsDbObj
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisorItem? emisor { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsRegistroDoc? registroFactura { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsCliente? cliente { get; set; }
        /// <summary>
        /// variable ClaveAcceso
        /// </summary>
        public string? claveAcceso { get; set; }
        /// <summary>
        /// variable EsFactura
        /// </summary>
        public bool? esFactura { get; set; }
        /// <summary>
        /// variable Autorizacion
        /// </summary>
        public string? autorizacion { get; set; }
        /// <summary>
        /// variable SubtotalPrevio
        /// </summary>
        public double? subtotalPrevio { get; set; }
        /// <summary>
        /// variable Subtotal0
        /// </summary>
        public double? subtotal0 { get; set; }
        /// <summary>
        /// variable Descuento
        /// </summary>
        public double? descuento { get; set; }
        /// <summary>
        /// variable Subtotal
        /// </summary>
        public double? subtotal { get; set; }
        /// <summary>
        /// variable Iva
        /// </summary>
        public double? iva { get; set; }
        /// <summary>
        /// variable Total
        /// </summary>
        public double? total { get; set; }
        /// <summary>
        /// variable PagoEfectivo
        /// </summary>
        public double? pagoEfectivo { get; set; }
        /// <summary>
        /// variable PagoTarjetaDebCred
        /// </summary>
        public double? pagoTarjetaDebCred { get; set; }
        /// <summary>
        /// variable PagoOtraForma
        /// </summary>
        public double? pagoOtraForma { get; set; }
        /// <summary>
        /// variable PagoOtraFormaDetalle
        /// </summary>
        public string? pagoOtraFormaDetalle { get; set; }

        /// <summary>
        /// Los items de la factura
        /// </summary>
        public List<ClsItemFacturaNotaCredito> lItem {  get; set; }
    }
    
    public class FetchDataFacturaNotaCredito : FetchData
    {
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsEmisorItem? emisor { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsRegistroDoc? registroFactura { get; set; }
        /// <summary>
        /// Sexo de nacimiento de la persona
        /// </summary>
        public ClsCliente? cliente { get; set; }
        /// <summary>
        /// variable ClaveAcceso
        /// </summary>
        public string? claveAcceso { get; set; }
        /// <summary>
        /// variable EsFactura
        /// </summary>
        public bool? esFactura { get; set; }
        /// <summary>
        /// variable Autorizacion
        /// </summary>
        public string? autorizacion { get; set; }
        /// <summary>
        /// variable SubtotalPrevio
        /// </summary>
        public double? subtotalPrevio { get; set; }
        /// <summary>
        /// variable Subtotal0
        /// </summary>
        public double? subtotal0 { get; set; }
        /// <summary>
        /// variable Descuento
        /// </summary>
        public double? descuento { get; set; }
        /// <summary>
        /// variable Subtotal
        /// </summary>
        public double? subtotal { get; set; }
        /// <summary>
        /// variable Iva
        /// </summary>
        public double? iva { get; set; }
        /// <summary>
        /// variable Total
        /// </summary>
        public double? total { get; set; }
        /// <summary>
        /// variable PagoEfectivo
        /// </summary>
        public double? pagoEfectivo { get; set; }
        /// <summary>
        /// variable PagoTarjetaDebCred
        /// </summary>
        public double? pagoTarjetaDebCred { get; set; }
        /// <summary>
        /// variable PagoOtraForma
        /// </summary>
        public double? pagoOtraForma { get; set; }
        /// <summary>
        /// variable PagoOtraFormaDetalle
        /// </summary>
        public string? pagoOtraFormaDetalle { get; set; }
    }
}
