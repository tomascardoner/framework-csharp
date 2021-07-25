using System;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class Error
    {
        public static void ProcessError(Exception ex, string FriendlyMessageText ="", bool ShowMessageBox = true, bool CustomMessageBox = true)
        {
            string ExceptionMessageText;
            string MessageTextToLog;
            Exception InnerException;

            // Prepare Exception Message Text counting for Inner Exceptions
            ExceptionMessageText = ex.Message;
            if (ex.InnerException != null)
            {
                if (ex.InnerException.InnerException != null)
                { InnerException = ex.InnerException.InnerException; }
                else
                { InnerException = ex.InnerException; }

                ExceptionMessageText += $"\r\n{new string('=', 25)}\r\nINNER EXCEPTION:\r\n{InnerException.Message}";
            }

            MessageTextToLog = "Where: " + ex.Source;

            if (FriendlyMessageText != "")
            { MessageTextToLog += " |#| User Message: " + FriendlyMessageText; }

            MessageTextToLog += string.Format(" |#| Stack Trace: {0} |#| Error: {1}", ex.StackTrace, ExceptionMessageText);

            // CSFramework.My.Application.Log.WriteException(Exception, TraceEventType.Error, ExceptionMessageText)
            
            if (ShowMessageBox)
            {
                if (CustomMessageBox)
                {
                    CardonerSistemas.ErrorMessageBox emb = new CardonerSistemas.ErrorMessageBox();
                    emb.labelFriendlyMessage.Text = FriendlyMessageText;
                    emb.labelSourceData.Text = ex.Source;
                    emb.textboxStackTraceData.Text = ex.StackTrace;
                    emb.textboxMessageData.Text = ExceptionMessageText;
                    emb.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Se ha producido un error.\n\n{FriendlyMessageText}\n\n{ExceptionMessageText}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}