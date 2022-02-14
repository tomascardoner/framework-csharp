using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace CardonerSistemas.Configuration
{
    static class Json
    {
        private static bool CheckFileExist(string configFolder, string fileName, bool showMessageIfFileNotExist, Log log = null)
        {
            if (File.Exists(Path.Combine(configFolder, fileName)))
            {
                return true;
            }
            else
            {
                string message = $"No se encontró el archivo de configuración '{fileName}', el cual debe estar ubicado dentro de la carpeta '{configFolder}'.";
                if (log == null)
                {
                    if (showMessageIfFileNotExist)
                    {
                        MessageBox.Show(message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (showMessageIfFileNotExist)
                    {
                        log.WriteAndShowError(message);
                    }
                    else
                    {
                        log.WriteError(message);
                    }
                }
                return false;
            }
        }

        internal static bool LoadFile<T>(string configFolder, string fileName, ref T configObject, bool returnFalseIfFileNotExist = true, Log log = null)
        {
            if (!CheckFileExist(configFolder, fileName, returnFalseIfFileNotExist, log))
            {
                if (returnFalseIfFileNotExist)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            string jsonConfigFileString;

            try
            {
                jsonConfigFileString = File.ReadAllText(Path.Combine(configFolder, fileName));
            }
            catch (System.Exception ex)
            {
                string message = $"Error al leer el archivo de configuración {fileName}.\n\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    message += $"\n\nInner message:\n{ex.InnerException.Message}";
                }
                if (log == null)
                {
                    MessageBox.Show(message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    log.WriteAndShowError(message);
                }
                return false;
            }

            try
            {
                configObject = JsonSerializer.Deserialize<T>(jsonConfigFileString);
            }
            catch (System.Exception ex)
            {
                string message;
                if (ex.InnerException == null)
                {
                    message = $"Error al interpretar el archivo de configuración {fileName}.\n\n{ex.Message}";
                }
                else
                {
                    message = $"Error al interpretar el archivo de configuración {fileName}.\n\n{ex.InnerException.Message}";
                }
                if (log == null)
                {
                    MessageBox.Show(message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    log.WriteAndShowError(message);
                }
                return false;
            }

            return true;
        }

        internal static bool SaveFile<T>(string configFolder, string fileName, ref T configObject, bool writeIndented = true, Log log = null)
        {
            string jsonConfigFileString;

            try
            {
                log.WriteDebug($"Serializer of {configObject.ToString()}");
                jsonConfigFileString = JsonSerializer.Serialize<T>(configObject, new JsonSerializerOptions() { WriteIndented = writeIndented });
            }
            catch (System.Exception ex)
            {
                string message = $"Error al serializar el objeto en archivo de configuración.\n\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    message += $"\n\nInner message:\n{ex.InnerException.Message}";
                }
                if (log == null)
                {
                    MessageBox.Show(message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    log.WriteAndShowError(message);
                }
                return false;
            }

            try
            {
                File.WriteAllText(Path.Combine(configFolder, fileName), jsonConfigFileString);
            }
            catch (System.Exception ex)
            {
                string message = $"Error al guardar el archivo de configuración {fileName}.\n\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    message += $"\n\nInner message:\n{ex.InnerException.Message}";
                }
                if (log == null)
                {
                    MessageBox.Show(message, My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    log.WriteAndShowError(message);
                }
                return false;
            }

            return true;
        }

        static internal Font ConvertStringToFont(string value)
        {
            TypeConverter converter;
            Font convertedFont;

            try
            {
                converter = TypeDescriptor.GetConverter(typeof(Font));
                convertedFont = (Font)converter.ConvertFromString(value);
            }
            catch (System.Exception)
            {
                convertedFont = null;
            }

            return convertedFont;
        }

        static internal string ConvertFontToString(Font value)
        {
            TypeConverter converter;
            string convertedString;

            try
            {
                converter = TypeDescriptor.GetConverter(typeof(Font));
                convertedString = converter.ConvertToString(value);
            }
            catch (System.Exception)
            {
                convertedString = "";
            }

            return convertedString;
        }

    }
}