using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace CardonerSistemas.Communication
{
    static class Serial
    {

        #region Declarations

        internal static int[] BaudRates = new int[] { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 57600, 115200, 128000, 256000 };
        internal static int BaudRateDefault = 9600;

        internal static Parity ParityDefault = Parity.None;

        internal static int[] DataBits = new int[] { 5, 6, 7, 8 };
        internal static int DataBitsDefault = 8;

        internal static StopBits StopBitsDefault = StopBits.One;

        internal static int ReadBufferSizeDefault = 4096;

        internal static Encoding EncodingDefault = new ASCIIEncoding();

        internal static Handshake HandshakeDefault = Handshake.None;

        internal static int ReadTimeoutDefault = 500;

        internal static int ReadPauseIntervalDefault = 0;

        #endregion

        #region Methods


        #endregion

    }
}
