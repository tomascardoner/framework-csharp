using System;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Error
    {
        public static void ProcessError(Exception ex, string FriendlyMessageText ="", bool ShowMessageBox = true, bool CustomMessageBox = true)
        {
            string exceptionMessageText;
            string messageTextToLog;
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

                exceptionMessageText += $"\r\n{new string('=', 25)}\r\nINNER EXCEPTION:\r\n{innerException.Message}";
            }

            messageTextToLog = "Where: " + ex.Source;

            if (!string.IsNullOrWhiteSpace(FriendlyMessageText))
            {
                messageTextToLog += " |#| User Message: " + FriendlyMessageText;
            }

            messageTextToLog += $" |#| Stack Trace: {ex.StackTrace} |#| Error: {exceptionMessageText}";

            //CardonerSistemas.My.Application.Log.WriteException(Exception, TraceEventType.Error, ExceptionMessageText)
            
            if (ShowMessageBox)
            {
                if (CustomMessageBox)
                {
                    CardonerSistemas.ErrorMessageBox emb = new CardonerSistemas.ErrorMessageBox();
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