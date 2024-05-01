using QRCoder;
using System;
using System.Drawing;

namespace CardonerSistemas.GraphicCodes
{
    public static class QR
    {
        public enum ErrorCorrectionLevel : byte
        {
            Low,
            Medium,
            Quartile,
            High
        }

        private static QRCodeGenerator.ECCLevel GetErrorCorrectionLevel(ErrorCorrectionLevel errorCorrectionLevel)
        {
            switch (errorCorrectionLevel)
            {
                case ErrorCorrectionLevel.Low:
                    return QRCodeGenerator.ECCLevel.L;
                case ErrorCorrectionLevel.Medium:
                    return QRCodeGenerator.ECCLevel.M;
                case ErrorCorrectionLevel.Quartile:
                    return QRCodeGenerator.ECCLevel.Q;
                case ErrorCorrectionLevel.High:
                    return QRCodeGenerator.ECCLevel.H;
                default:
                    throw new ArgumentOutOfRangeException("errorCorrectionLevel", "Fuera de rango.");
            }
        }

        public static Bitmap Generate(string data, ErrorCorrectionLevel errorCorrectionLevel, int pixelsPerModule, bool forceUtf8 = false)
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(data, GetErrorCorrectionLevel(errorCorrectionLevel), forceUtf8);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(pixelsPerModule);
        }

        public static Bitmap Generate(string data, int pixelsPerModule)
        {
            return Generate(data, ErrorCorrectionLevel.Low, pixelsPerModule);
        }
    }
}