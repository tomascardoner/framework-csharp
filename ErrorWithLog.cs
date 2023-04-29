using System;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Error
    {
        public static void ProcessError(Exception ex, string FriendlyMessageText = "", bool ShowMessageBox = true, bool CustomMessageBox = true, Log log = null, bool logStackTrace = false)
        {
            string exceptionMessageText;
            string exceptionMessageToLog;
            string messageTextToLog;
            Exception innerException;

            const string TextSeparator = " // ";

            // Prepare Exception Message Text counting for Inner Exceptions
            exceptionMessageText = ex.Message;
            exceptionMessageToLog = ex.Message;
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
                exceptionMessageToLog += $"{TextSeparator}Inner Exception: {innerException.Message.Replace("\r\n", " ")}";
            }

            messageTextToLog = "Source: " + ex.Source;

            if (!string.IsNullOrWhiteSpace(FriendlyMessageText))
            {
                messageTextToLog += $"{TextSeparator}User Message: {FriendlyMessageText}";
            }

            if (logStackTrace)
            {
                messageTextToLog += $"{TextSeparator}Stack Trace: {ex.StackTrace}";
            }
            messageTextToLog += $"{TextSeparator}Error: {exceptionMessageToLog}";

            log?.WriteError(messageTextToLog);

            if (ShowMessageBox)
            {
                if (CustomMessageBox)
                {
                    ErrorMessageBox emb = new ErrorMessageBox();
                    emb.labelFriendlyMessage.Text = FriendlyMessageText;
                    emb.labelSourceData.Text = ex.Source;
                    emb.textboxStackTraceData.Text = ex.StackTrace;
                    emb.textboxMessageData.Text = exceptionMessageText;
                    emb.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Se ha producido un error.\n\n{FriendlyMessageText}\n\n{exceptionMessageText}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}