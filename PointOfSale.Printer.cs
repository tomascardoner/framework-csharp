using Microsoft.PointOfService;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CardonerSistemas.PointOfSale
{
    class Printer
    {
        #region Printer commands

        private const char ESC_Char = '\u001b';

        static private string ESC = ESC_Char + "|";
        public const string NewLine = "\n";
        public const string CarriageReturn = "\r";
        public string CrLf = NewLine + CarriageReturn;

        public string ResetFormat = ESC + "@";

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
        private string _SetLineSpacing = ESC + "3 #";

        public string AlignCentre = ESC + "cA";
        public string AlignRight = ESC + "rA";
        public string AlignLeft = ESC + "lA";

        public string LineFeedAndPaperCut = ESC + "fP";

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

        #region Declaration

        PosPrinter printer = null;

        public Printer()
        {
        }

        ~Printer()
        {
            if (printer != null)
            {
                try
                {
                    // Cancel the device
                    printer.DeviceEnabled = false;

                    // Release the device exclusive control right.
                    printer.Release();

                    // Finish using the device.
                    printer.Close();

                }
                catch (PosControlException)
                {
                }
            }
        }

        #endregion

        #region Open

        public bool GetDevice(string deviceName)
        {
            try
            {
                PosExplorer explorer;
                DeviceInfo deviceInfo;

                explorer = new PosExplorer();

                deviceInfo = explorer.GetDevice(DeviceType.PosPrinter, deviceName);
                printer = (PosPrinter)explorer.CreateInstance(deviceInfo);

                explorer = null;
                deviceInfo = null;

                return (printer != null);
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, "Error al obtener una instancia de la impresora.");
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

        public bool GetOpenClaimAndEnable(string deviceName, int claimTimeout)
        {
            if (GetDevice(deviceName))
            {
                if (Open())
                {
                    if (Claim(claimTimeout))
                    {
                        if (Enable())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Print commands

        public bool TransactionPrintBegin()
        {
            try
            {
                printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction);
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, "Error al iniciar la transacción de impresión.");
                return false;
            }
        }

        public bool TransactionPrintEnd()
        {
            try
            {
                printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, "Error al finalizar la transacción de impresión.");
                return false;
            }
        }

        public bool PrintLine(string textToPrint, params object[] args)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                textToPrint = ReplaceFormatTextStyle(textToPrint);
                printer.PrintNormal(PrinterStation.Receipt, string.Format(textToPrint, args));
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

        public bool PrintLineCrLf(string textToPrint, params object[] args)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                textToPrint = ReplaceFormatTextStyle(textToPrint);
                printer.PrintNormal(PrinterStation.Receipt, string.Format(textToPrint, args));
                printer.PrintNormal(PrinterStation.Receipt, CrLf);
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

        public bool SetBitmap(int bitmapNumber, PrinterStation station, string fileName, int width = PosPrinter.PrinterBitmapAsIs, int alignment = PosPrinter.PrinterBitmapCenter)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (printer.CapRecBitmap == true)
            {
                bool bSetBitmapSuccess = false;
                for (int iRetryCount = 0; iRetryCount < 5; iRetryCount++)
                {
                    try
                    {
                        //Register a bitmap
                        printer.SetBitmap(bitmapNumber, PrinterStation.Receipt, fileName, width, alignment);
                        bSetBitmapSuccess = true;
                        break;
                    }
                    catch (PosControlException pce)
                    {
                        if (pce.ErrorCode == ErrorCode.Failure && pce.ErrorCodeExtended == 0 && pce.Message == "It is not initialized.")
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
                if (!bSetBitmapSuccess)
                {
                    MessageBox.Show("Error al setear la imagen en la impresora.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Default;
                    return false;
                }
            }

            Cursor.Current = Cursors.Default;
            return true;
        }

        public bool PrintBarcode(string data, BarCodeSymbology symbology, int height, int width, int alignment, BarCodeTextPosition textPosition)
        {
            try
            {
                if (printer.CapRecBarCode == true)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    printer.PrintBarCode(PrinterStation.Receipt, data, symbology, height, width, alignment, textPosition);
                    Cursor.Current = Cursors.Default;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CardonerSistemas.Error.ProcessError(ex, "Error al imprimir el código de barras en la impresora.");
            }
            return false;
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

        #endregion

        #region Misc commands

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

        private string ReplaceFormatTextStyle(string textToFormat)
        {
            // ALIGNMENT
            textToFormat = textToFormat.Replace("<LEFT>", AlignLeft);
            textToFormat = textToFormat.Replace("<RIGHT>", AlignRight);
            textToFormat = textToFormat.Replace("<CENTER>", AlignCentre);

            // STYLE
            textToFormat = textToFormat.Replace("<NORMAL>", TextStyleRestoreNormal);
            textToFormat = textToFormat.Replace("<ITALIC>", TextStyleItalic);
            textToFormat = textToFormat.Replace("<UNDERLINE>", TextStyleUnderline);
            textToFormat = textToFormat.Replace("<BOLD>", TextStyleBold);
            textToFormat = textToFormat.Replace("<SUBSCRIPT>", TextStyleSubScript);
            textToFormat = textToFormat.Replace("<SUPERSCRIPT>", TextStyleSuperScript);
            textToFormat = textToFormat.Replace("<STRIKETHROUGH>", TextStyleStrikeThrough);

            // SIZE
            textToFormat = textToFormat.Replace("<SINGLE>", TextSizeSingleHighAndWide);
            textToFormat = textToFormat.Replace("<DOUBLEHIGH>", TextSizeDoubleHigh);
            textToFormat = textToFormat.Replace("<DOUBLEWIDE>", TextSizeDoubleWide);
            textToFormat = textToFormat.Replace("<DOUBLE>", TextSizeDoubleHighAndWide);

            // COMMANDS
            textToFormat = textToFormat.Replace("<CRLF>", CrLf);
            textToFormat = textToFormat.Replace("<CUT>", LineFeedAndPaperCut);

            return textToFormat;
        }

        #endregion

        #region Close

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

        public bool ReleaseAndClose(int timeout)
        {
            if (printer != null)
            {
                if (printer.Claimed)
                {
                    if (!Release())
                    {
                        return false;
                    }
                }
                switch (printer.State)
                {
                    case ControlState.Closed:
                        break;
                    case ControlState.Idle:
                        printer.Close();
                        break;
                    case ControlState.Busy:
                        for (int i = 0; i < timeout; i++)
                        {
                            System.Threading.Thread.Sleep(1000);
                            if (printer.State == ControlState.Idle)
                            {
                                break;
                            }
                        }
                        break;
                    case ControlState.Error:
                        return false;
                    default:
                        break;
                }
            }
            return true;
        }

        #endregion

        #region Error handling

        public string GetErrorCode(PosControlException ex)
        {
            string strErrorCodeEx = "";

            switch (ex.ErrorCodeExtended)
            {
                case PosPrinter.ExtendedErrorCoverOpen:
                case PosPrinter.ExtendedErrorJournalEmpty:
                case PosPrinter.ExtendedErrorReceiptEmpty:
                case PosPrinter.ExtendedErrorSlipEmpty:
                    strErrorCodeEx = ex.Message;
                    break;
                default:
                    string strEC = ex.ErrorCode.ToString();
                    string strECE = ex.ErrorCodeExtended.ToString();
                    strErrorCodeEx = "ErrorCode =" + strEC + "\nErrorCodeExtended =" + strECE + "\n"
                        + ex.Message;
                    break;
            }
            return strErrorCodeEx;
        }

        #endregion
    }
}
