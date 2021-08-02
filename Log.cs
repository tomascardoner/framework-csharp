using NLog;
using System.Windows.Forms;

namespace CardonerSistemas
{
    public class Log
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        internal enum Levels
        {
            Trace,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        internal void Write(string message, Levels level)
        {
            if (level == Levels.Trace)
            {
                WriteTrace(message);
            }
            else if (level == Levels.Debug)
            {
                WriteDebug(message);
            }
            else if (level == Levels.Info)
            {
                WriteInfo(message);
            }
            else if (level == Levels.Warn)
            {
                WriteWarn(message);
            }
            else if (level == Levels.Error)
            {
                WriteError(message);
            }
            else if (level == Levels.Fatal)
            {
                WriteFatal(message);
            }
        }

        internal void WriteTrace(string message)
        {
            logger.Trace(message);
        }

        internal void WriteDebug(string message)
        {
            logger.Debug(message);
        }

        internal void WriteInfo(string message)
        {
            logger.Info(message);
        }

        internal void WriteWarn(string message)
        {
            logger.Warn(message);
        }

        internal void WriteError(string message)
        {
            logger.Error(message);
        }

        internal void WriteFatal(string message)
        {
            logger.Fatal(message);
        }

        internal void WriteAndShow(string message, Levels level)
        {
            Write(message, level);

            MessageBoxIcon icon = MessageBoxIcon.None;
            if (level == Levels.Info)
            {
                icon = MessageBoxIcon.Information;
            }
            else if (level == Levels.Warn)
            {
                icon = MessageBoxIcon.Warning;
            }
            else if (level == Levels.Error)
            {
                icon = MessageBoxIcon.Error;
            }
            else if (level == Levels.Fatal)
            {
                icon = MessageBoxIcon.Stop;
            }
            MessageBox.Show(message, My.Application.Info.Title, MessageBoxButtons.OK, icon);
        }

        internal void WriteAndShowTrace(string message)
        {
            WriteAndShow(message, Levels.Trace);
        }

        internal void WriteAndShowDebug(string message)
        {
            WriteAndShow(message, Levels.Debug);
        }

        internal void WriteAndShowInfo(string message)
        {
            WriteAndShow(message, Levels.Info);
        }

        internal void WriteAndShowWarn(string message)
        {
            WriteAndShow(message, Levels.Warn);
        }

        internal void WriteAndShowError(string message)
        {
            WriteAndShow(message, Levels.Error);
        }

        internal void WriteAndShowFatal(string message)
        {
            WriteAndShow(message, Levels.Fatal);
        }
    }
}
