using System;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Error
    {
        public static void ProcessError(Exception ex, string FriendlyMessageText ="")
        {
            string exceptionMessageText;
            Exception innerException;

            // Prepare Exception Message Text counting for Inner Exceptions
            exceptionMessageText = ex.Message;
            if (ex.InnerException != null)
            {
                if (ex.InnerException.InnerException != null)
                { 
                    innerException = ex.InnerException.InnerException;
                }
                else
                {
                    innerException = ex.InnerException;
                }

                exceptionMessageText += $"\r\n{new string('=', 16)}\r\nInner Exception:\r\n{innerException.Message}";
            }

            MessageBox.Show($"Se ha producido un error.\n\n{FriendlyMessageText}\n\n{exceptionMessageText}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}