using Microsoft.PointOfService;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CardonerSistemas
{
    class POS_Printer
    {
        #region Formatting text

        private const char ESC_Char = '\x1B';
        static private string ESC = ESC_Char + "|";
        public const string CrLf = "\r\n";

        public string TextStyleBold = ESC + "bC";
        public string TextStyleUnderline = ESC + "uC";
        public string TextStyleItalic = ESC + "iC";
        public string TextStyleSubScript = ESC + "tbC";
        public string TextStyleSuperScript = ESC + "tpC";
        public string TextStyleStrikeThrough = ESC + "stC";
        public string TextStyleRestoreNormal = ESC + "N";

        public string TextSizeSingleHighAndWide = ESC + "1C";
        public string TextSizeDoubleWide = ESC + "2C";
        public string TextSizeDoubleHigh = ESC + "3C";
        public string TextSizeDoubleHighAndWide = ESC + "4C";

        private string _ScaleHorizontally = ESC + "#hC";
        private string _ScaleVertically = ESC + "#vC";

        public string AlignCentre = ESC + "cA";
        public string AlignRight = ESC + "rA";
        public string AlignLeft = ESC + "lA";

        public string TextSizeScaleHorizontally(byte times)
        {
            return _ScaleHorizontally.Replace("#", times.ToString());
        }
        public string TextSizeScaleVertically(byte times)
        {
            return _ScaleVertically.Replace("#", times.ToString());
        }

        // Sample = "This is a test" + CrLf + SetBold + SetSize(3) + SetCentre + "it works" + SetSize(1) + " pretty well" + CrLf + "OK";

        #endregion

        PosExplorer explorer;
        DeviceInfo deviceInfo;
        PosPrinter printer;

        public POS_Printer()
        {
            try
            {
                explorer = new PosExplorer();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        ~POS_Printer()
        {
            explorer = null;
            deviceInfo = null;
            printer = null;
        }

        public bool GetDevice(string deviceName)
        {
            try
            {
                deviceInfo = explorer.GetDevice(DeviceType.PosPrinter, deviceName);
                printer = (PosPrinter)explorer.CreateInstance(deviceInfo);
                return (printer != null);
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, "Error al obtener información de la impresora.");
                return false;
            }
        }

        public bool Open()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.Open();
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al abrir la conexión a la impresora.");
                return false;
            }
        }

        public bool Claim(int timeout)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.Claim(timeout);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al obtener acceso exclusivo a la impresora.");
                return false;
            }
        }

        public bool Enable()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.DeviceEnabled = true;
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al habilitar la impresora.");
                return false;
            }
        }

        public bool PrintLine(string textToPrint)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.PrintNormal(PrinterStation.Receipt, textToPrint);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al abrir la conexión a la impresora.");
                return false;
            }
        }

        public bool SetLogo(PrinterLogoLocation location, string data)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.SetLogo(location, data);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al cerrar la conexión a la impresora.");
                return false;
            }
        }

        public bool SetBitmap(int bitmapNumber, PrinterStation station, string fileName, int width, int alignment)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.SetBitmap(bitmapNumber, station, fileName, width, alignment);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al cerrar la conexión a la impresora.");
                return false;
            }
        }

        public bool PrintBitmap(string fileName, int width, int alignment)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.PrintBitmap(PrinterStation.Receipt, fileName, width, alignment);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al imprimir la imagen en la impresora.");
                return false;
            }
        }

        public bool PrintMemoryBitmap(Bitmap bitmap, int width, int alignment)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.PrintMemoryBitmap(PrinterStation.Receipt, bitmap, width, alignment);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al imprimir la imagen en la impresora.");
                return false;
            }
        }

        public bool PrintSavedBitmap(int bitmapNumber)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // printer.PrintSavedBitmap(bitmapNumber);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al imprimir la imagen en la impresora.");
                return false;
            }
        }

        public bool DrawRuledLine()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.DrawRuledLine(PrinterStation.Receipt, "0,500", LineDirection.Horizontal, 1, LineStyle.BrokenLine, 1);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al liberar el acceso exclusivo a la impresora.");
                return false;
            }
        }

        public bool CutPaper(int percentage)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.CutPaper(percentage);
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al cortar el papel de la impresora.");
                return false;
            }
        }

        public bool Release()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.Release();
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al liberar el acceso exclusivo a la impresora.");
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                printer.Close();
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al cerrar la conexión a la impresora.");
                return false;
            }
        }

    }
}
