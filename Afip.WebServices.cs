using System;
using System.Collections.Generic;

namespace CardonerSistemas.Afip
{
    static class WebServices
    {

        #region Declarations

        internal const string ServicioFacturacionElectronica = "wsfe";

        internal const string SolicitudCaeResultadoAceptado = "A";
        internal const string SolicitudCaeResultadoRechazado = "R";
        internal const string SolicitudCaeResultadoParcial = "P";

        internal class ComprobanteAsociado
        {
            internal short TipoComprobante { get; set; }
            internal short PuntoVenta { get; set; }
            internal int ComprobanteNumero { get; set; }
        }

        internal class Tributo
        {
            internal short ID { get; set; }
            internal string Descripcion { get; set; }
            internal decimal BaseImponible { get; set; }
            internal decimal Alicuota { get; set; }
            internal decimal Importe { get; set; }
        }

        internal class Iva
        {
            internal short Id { get; set; }
            internal decimal BaseImponible { get; set; }
            internal decimal Importe { get; set; }
        }

        internal class Opcional
        {
            internal string Id { get; set; }
            internal string Valor { get; set; }
        }

        internal class FacturaElectronicaCabecera
        {
            internal short Concepto { get; set; }
            internal short TipoDocumento { get; set; }
            internal long DocumentoNumero { get; set; }
            internal short TipoComprobante { get; set; }
            internal short PuntoVenta { get; set; }
            internal int ComprobanteDesde { get; set; }
            internal int ComprobanteHasta { get; set; }
            internal DateTime ComprobanteFecha { get; set; }
            internal decimal ImporteTotal { get; set; }
            internal decimal ImporteTotalConc { get; set; }            // Importe neto no gravado - Para comprobantes "C", debe ser cero.
            internal decimal ImporteNeto { get; set; }                 // Importe neto gravado - Para comprobantes "C", debe ser igual al Subtotal.
            internal decimal ImporteOperacionesExentas { get; set; }   // Para comprobantes "C", debe ser cero.
            internal decimal ImporteTributos { get; set; }
            internal decimal ImporteIVA { get; set; }                  // Para comprobantes "C", debe ser cero.
            internal DateTime FechaServicioDesde { get; set; }
            internal DateTime FechaServicioHasta { get; set; }
            internal DateTime FechaVencimientoPago { get; set; }
            internal string MonedaID { get; set; }
            internal decimal MonedaCotizacion { get; set; }            // Para pesos argentinos, debe ser 1.

            internal List<ComprobanteAsociado> ComprobantesAsociados { get; set; }
            internal List<Tributo> Tributos { get; set; }
            internal List<Iva> IVAs { get; set; }
            internal List<Opcional> Opcionales { get; set; }

            public void AfipWebServices()
            {
                ComprobantesAsociados = new List<ComprobanteAsociado>();
                Tributos = new List<Tributo>();
                IVAs = new List<Iva>();
                Opcionales = new List<Opcional>();
            }
        }

        internal class ResultadoCae
        {
            internal char Resultado { get; set; }
            internal string Numero { get; set; }
            internal DateTime FechaVencimiento { get; set; }
            internal string Observaciones { get; set; }
            internal string ErrorMessage { get; set; }
        }

        internal class ResultadoConsultaComprobante
        {
            internal short Concepto { get; set; }
            internal short TipoDocumento { get; set; }
            internal short DocumentoNumero { get; set; }
            internal short TipoComprobante { get; set; }
            internal short PuntoVenta { get; set; }
            internal int ComprobanteDesde { get; set; }
            internal int ComprobanteHasta { get; set; }
            internal DateTime ComprobanteFecha { get; set; }
            internal decimal ImporteTotal { get; set; }
            internal decimal ImporteTotalConc { get; set; }            // Importe neto no gravado - Para comprobantes "C", debe ser cero.
            internal decimal ImporteNeto { get; set; }                 // Importe neto gravado - Para comprobantes "C", debe ser igual al Subtotal.
            internal decimal ImporteTributos { get; set; }
            internal decimal ImporteIVA { get; set; }                  // Para comprobantes "C", debe ser cero.
            internal DateTime FechaServicioDesde { get; set; }
            internal DateTime FechaServicioHasta { get; set; }
            internal DateTime FechaVencimientoPago { get; set; }
            internal string MonedaID { get; set; }
            internal decimal MonedaCotizacion { get; set; }            // Para pesos argentinos, debe ser 1.
            internal char Resultado { get; set; }
            internal string CodigoAutorizacion { get; set; }
            internal string EmisionTipo { get; set; }
            internal DateTime FechaVencimiento { get; set; }
            internal DateTime FechaHoraProceso { get; set; }
            internal string Observaciones { get; set; }
            internal string ErrorMessage { get; set; }
        }

    #endregion

    }
}
