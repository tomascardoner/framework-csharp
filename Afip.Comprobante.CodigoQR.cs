using System.Drawing;
using System;
using System.Globalization;
using System.Text;
using CardonerSistemas.Framework.Afip;

namespace CardonerSistemas.Afip.Comprobantes.CodigoQR
{
    public static class Ver1
    {
#pragma warning disable S1075 // URIs should not be hardcoded
        private const string CodigoQRUrl = "https://www.afip.gob.ar/fe/qr/?p={DATA}";
#pragma warning restore S1075 // URIs should not be hardcoded
        private const string CodigoQRDatos = "{\"ver\":1,\"fecha\":\"{FECHA}\",\"cuit\":{CUIT},\"ptoVta\":{PUNTOVENTA},\"tipoCmp\":{TIPOCOMPROBANTE},\"nroCmp\":{NUMEROCOMPROBANTE},\"importe\":{IMPORTE},\"moneda\":\"{MONEDA}\",\"ctz\":{MONEDACOTIZACION},\"tipoDocRec\":{TIPODOCUMENTO},\"nroDocRec\":{NUMERODOCUMENTO},\"tipoCodAut\":\"{TIPOCODIGOAUTORIZACION}\",\"codAut\":{CODIGOAUTORIZACION}}";

        // Campos
        private const string CodigoQRCampoDatos = "{DATA}";

        private const string CodigoQRCampoFecha = "{FECHA}";
        private const string CodigoQRCampoCuit = "{CUIT}";
        private const string CodigoQRCampoPuntoVenta = "{PUNTOVENTA}";
        private const string CodigoQRCampoTipoComprobante = "{TIPOCOMPROBANTE}";
        private const string CodigoQRCampoNumeroComprobante = "{NUMEROCOMPROBANTE}";
        private const string CodigoQRCampoImporte = "{IMPORTE}";
        private const string CodigoQRCampoMoneda = "{MONEDA}";
        private const string CodigoQRCampoMonedaCotizacion = "{MONEDACOTIZACION}";
        private const string CodigoQRCampoTipoDocumento = "{TIPODOCUMENTO}";
        private const string CodigoQRCampoNumeroDocumento = "{NUMERODOCUMENTO}";
        private const string CodigoQRCampoTipoCodigoAutorizacion = "{TIPOCODIGOAUTORIZACION}";
        private const string CodigoQRCampoCodigoAutorizacion = "{CODIGOAUTORIZACION}";

        public const char TipoCodigoAutorizacionCaea = 'A';
        public const char TipoCodigoAutorizacionCae = 'E';

        public class Datos
        {
            private DateTime _fecha;
            private long _cuit;
            private int _puntoVenta;
            private short _tipoComprobante;
            private int _numeroComprobante;
            private decimal _importe;
            private string _moneda = string.Empty;
            private decimal _monedaCotizacion;
            private byte _tipoDocumento;
            private char _tipoCodigoAutorizacion = ' ';
            private long _codigoAutorizacion;

            public DateTime Fecha
            {
                get => _fecha;
                set
                {
                    if (value.Year >= 2000 && value.Year <= 2099)
                    {
                        _fecha = value;
                    }
                    else
                    {
                        throw new ArgumentException("Fecha inválida.");
                    }
                }
            }

            public long Cuit
            {
                get => _cuit;
                set
                {
                    if (Afip.Cuit.Verificar(value))
                    {
                        _cuit = value;
                    }
                    else
                    {
                        throw new ArgumentException("CUIT inválida.");
                    }
                }
            }

            public int PuntoVenta
            {
                get => _puntoVenta;
                set
                {
                    if (value >= 1 && value <= 99999)
                    {
                        _puntoVenta = value;
                    }
                    else
                    {
                        throw new ArgumentException("Punto de venta inválido.");
                    }
                }
            }

            public short TipoComprobante
            {
                get => _tipoComprobante;
                set
                {
                    if (value >= 1 && value <= 999 && Tablas.TiposComprobante.ContainsKey(value))
                    {
                        _tipoComprobante = value;
                    }
                    else
                    {
                        throw new ArgumentException("Tipo de comprobante inválido.");
                    }
                }
            }

            public int NumeroComprobante
            {
                get => _numeroComprobante;
                set
                {
                    if (value >= 1 && value <= 99999999)
                    {
                        _numeroComprobante = value;
                    }
                    else
                    {
                        throw new ArgumentException("Número de comprobante inválido.");
                    }
                }
            }

            public decimal Importe
            {
                get => _importe;
                set
                {
                    bool isValid = value >= 0 && value <= (decimal)9999999999999.99;
                    if (isValid)
                    {
                        _importe = value;
                    }
                    else
                    {
                        throw new ArgumentException("Importe inválido.");
                    }
                }
            }

            public string Moneda
            {
                get => _moneda;
                set
                {
                    if (Tablas.Monedas.ContainsKey(value))
                    {
                        _moneda = value;
                    }
                    else
                    {
                        throw new ArgumentException("Moneda inválida.");
                    }
                }
            }

            public decimal MonedaCotizacion
            {
                get => _monedaCotizacion;
                set
                {
                    if (value > 0 && value <= (decimal)9999999999999.999999)
                    {
                        _monedaCotizacion = value;
                    }
                    else
                    {
                        throw new ArgumentException("Corización de moneda inválida.");
                    }
                }
            }

            public byte TipoDocumento
            {
                get => _tipoDocumento;
                set
                {
                    if (value >= 0 && value <= 99 && Tablas.TiposDocumento.ContainsKey(value))
                    {
                        _tipoDocumento = value;
                    }
                    else
                    {
                        throw new ArgumentException("Tipo de documento inválido.");
                    }
                }
            }

            public ulong NumeroDocumento { get; set; }

            public char TipoCodigoAutorizacion
            {
                get => _tipoCodigoAutorizacion;
                set
                {
                    if (value == TipoCodigoAutorizacionCaea || value == TipoCodigoAutorizacionCae)
                    {
                        _tipoCodigoAutorizacion = value;
                    }
                    else
                    {
                        throw new ArgumentException("Tipo de código de autorización inválido.");
                    }
                }
            }

            public long CodigoAutorizacion
            {
                get => _codigoAutorizacion;
                set
                {
                    if (value >= 10000000000000 && value <= 99999999999999)
                    {
                        _codigoAutorizacion = value;
                    }
                    else
                    {
                        throw new ArgumentException("Código de autorización inválido.");
                    }
                }
            }
        }

        public static Bitmap GenerarImagen(Datos datos, int pixelsPorModulo = 4)
        {
            if (datos == null)
            {
                throw new ArgumentException("Datos inválidos (null).");
            }

            string codigoQRDatos = CodigoQRDatos;

            // Reemplazo los valores de los campos
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoFecha, datos.Fecha.ToString("yyyy-MM-dd"));
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoCuit, datos.Cuit.ToString());
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoPuntoVenta, datos.PuntoVenta.ToString());
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoTipoComprobante, datos.TipoComprobante.ToString());
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoNumeroComprobante, datos.NumeroComprobante.ToString());
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoImporte, datos.Importe.ToString("#0.00").Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, "."));
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoMoneda, datos.Moneda);
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoMonedaCotizacion, datos.MonedaCotizacion.ToString("#0.000000").Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, "."));
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoTipoDocumento, datos.TipoDocumento.ToString());
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoNumeroDocumento, datos.NumeroDocumento.ToString());
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoTipoCodigoAutorizacion, datos.TipoCodigoAutorizacion.ToString());
            codigoQRDatos = codigoQRDatos.Replace(CodigoQRCampoCodigoAutorizacion, datos.CodigoAutorizacion.ToString());

            // Convierto los datos a Base64
            codigoQRDatos = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(codigoQRDatos));

            // Preparo el link de Afip
            string afipUrl = CodigoQRUrl;
            afipUrl = afipUrl.Replace(CodigoQRCampoDatos, codigoQRDatos);

            // Genero el QR
            return GraphicCodes.QR.Generate(afipUrl, pixelsPorModulo);
        }

        public static byte[] GenerarBytes(Datos datos, int pixelsPorModulo = 4)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo(GenerarImagen(datos, pixelsPorModulo), typeof(byte[]));
        }
    }
}