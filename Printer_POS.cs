using Microsoft.PointOfService;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CardonerSistemas
{
    class Printer_POS
    {
        PosExplorer explorer;
        DeviceInfo deviceInfo;
        PosDevice device;
        PosPrinter printer;

        public Printer_POS()
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

        ~Printer_POS()
        {
            explorer = null;
            deviceInfo = null;
            device = null;
            printer = null;
        }

        public bool GetDevice(string deviceName)
        {
            try
            {
                deviceInfo = explorer.GetDevice(DeviceType.PosPrinter, deviceName);
                device = explorer.CreateInstance(deviceInfo);
                //printer =

                //return (printer != null && printer.Capabilities.Receipt.IsPrinterPresent)
                return true;
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

        public bool PrintLine(string textToPrint)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                textToPrint = textToPrint.Replace("ESC", ((char)27).ToString()) + "\x1B|1lF";
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
