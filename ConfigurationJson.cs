using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace CardonerSistemas
{
    static class ConfigurationJson
    {
        private const int ErrorFileBadFormat = -2146233079;
        private const int ErrorFileBadFormatInnerElement = -2146232000;
        private const int ErrorFileBadFormatInnerElementValue = -2146233033;
        private const string ErrorFileBadFormatPositionPattern = @"\(\d+, \d+\)";
        private const string ErrorFileBadFormatPositionLinePattern = @"\d+";

        private static bool CheckFileExist(string configFolder, string fileName)
        {
            if (File.Exists(Path.Combine( configFolder , fileName)))
            {
                return true;
            }
            else
            {
                MessageBox.Show($"No se encontró el archivo de configuración '{fileName}', el cual debe estar ubicado dentro de la carpeta '{configFolder}'.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        internal static bool LoadFile<T>(string configFolder, string fileName, ref T configObject)
        {
            if (!CheckFileExist(configFolder, fileName))
            {
                return false;
            }

            string jsonConfigFileString;

            try
            {
                jsonConfigFileString = File.ReadAllText(Path.Combine(configFolder, fileName));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo de configuración {fileName}.\n\n{ex.InnerException.Message}", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                configObject = JsonSerializer.Deserialize<T>(jsonConfigFileString);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al interpretar el archivo de configuración {fileName}.\n\n{ex.InnerException.Message}", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
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