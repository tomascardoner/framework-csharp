using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CardonerSistemas
{
    class Serializer
    {
        public T Deserialize<T>(string input)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }

    }

    static class Configuration
    {
        private const int ErrorFileBadFormat = -2146233079;
        private const int ErrorFileBadFormatInnerElement = -2146232000;
        private const int ErrorFileBadFormatInnerElementValue = -2146233033;
        private const string ErrorFileBadFormatPositionPattern = @"\(\d+, \d+\)";
        private const string ErrorFileBadFormatPositionLinePattern = @"\d+";

        private static bool CheckFileExist(string configFolder, string fileName, EventLog log)
        {
            if (File.Exists(configFolder + fileName))
            {
                return true;
            }
            else
            {
                log.WriteEntry($"No se encontró el archivo de configuración '{fileName}', el cual debe estar ubicado dentro de la carpeta '{configFolder}'.");
                return false;
            }
        }

        internal static bool LoadFile<T>(string configFolder, string fileName, ref T configObject, EventLog log)
        {
            XmlSerializer serializer;
            FileStream fileStream;

            if (!CheckFileExist(configFolder, fileName, log))
            {
                return false;
            }

            try
            {
                serializer = new XmlSerializer(typeof(T));
                fileStream = new FileStream(configFolder + fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                configObject = (T)serializer.Deserialize(fileStream);
                return true;
            }
            catch (System.Exception ex)
            {
                switch (ex.HResult)
                {
                    case ErrorFileBadFormat:
                        // El formato del archivo es incorrecto.
                        if (ex.InnerException != null)
                        {
                            // Intento obtener mayor información del error
                            switch (ex.InnerException.HResult)
                            {
                                case ErrorFileBadFormatInnerElement:
                                    // Error en el elemento
                                    log.WriteEntry($"Error en el formato del archivo de configuración {fileName}.\n\n{ex.InnerException.Message}");
                                    break;
                                case ErrorFileBadFormatInnerElementValue:
                                    // El valor especificado en el elemento no es válido

                                    // Trato de obtener la línea en la que se encuentra el error
                                    string textoPosicion;
                                    string textoPosicionLinea;
                                    int posicionLinea = 0;

                                    Regex regexPosicion = new Regex(ErrorFileBadFormatPositionPattern);
                                    Regex regexLinea = new Regex(ErrorFileBadFormatPositionLinePattern);

                                    if (regexPosicion.IsMatch(ex.Message))
                                    {
                                        textoPosicion = regexPosicion.Match(ex.Message).Value;
                                        textoPosicionLinea = regexLinea.Match(textoPosicion).Value;
                                        int.TryParse(textoPosicionLinea, out posicionLinea);
                                    }

                                    if (posicionLinea > 0)
                                    {
                                        log.WriteEntry($"Error en el formato del archivo de configuración '{fileName}', en la línea número {posicionLinea - 1}.\n\n{ex.InnerException.Message}");
                                    }
                                    else
                                    {
                                        log.WriteEntry($"Error en el formato del archivo de configuración '{fileName}'.\n\n{ex.InnerException.Message}");
                                    }
                                    break;
                                default:
                                    log.WriteEntry($"Error al cargar el archivo de configuración '{fileName}'. {ex.Message}.");
                                    break;
                            }
                        }
                        break;
                    default:
                        log.WriteEntry($"Error al cargar el archivo de configuración '{fileName}'. {ex.Message}.");
                        break;
                }
                serializer = null;
                fileStream = null;
                return false;
            }
        }
    }
}