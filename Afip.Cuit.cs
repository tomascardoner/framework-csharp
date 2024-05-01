using System;
using System.Text.RegularExpressions;

namespace CardonerSistemas.Afip
{
    public static class Cuit
    {
        #region Declaraciones

        private const char Separador = '-';

        // Prefijos
        public const string PrefijoMasculino = "20";

        public const string PrefijoMasculinoAlternativo = "23";
        public const string PrefijoFemenino = "27";
        public const string PrefijoFemeninoAlternativo = "24";
        public const string PrefijoPersonaJuridica = "30";
        public const string PrefijoPersonaJuridicaAlternativo1 = "33";
        public const string PrefijoPersonaJuridicaAlternativo2 = "34";

        #endregion Declaraciones

        private static bool VerificarFormato(string value, string cleanedValue)
        {
            int mustLenght = 0;

            switch (value.Length)
            {
                case 10:
                case 12:
                    mustLenght = 10;
                    break;

                case 11:
                case 13:
                    mustLenght = 11;
                    break;

                default:
                    break;
            }

            switch (value.Length)
            {
                case 10:
                case 11:
                    if (cleanedValue.Length < mustLenght)
                    {
                        return false;
                    }
                    break;

                case 12:
                case 13:
                    if (value[2] == Separador && value[9] == Separador)
                    {
                        if (cleanedValue.Length < mustLenght)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    break;

                default:
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Método que verifica que la CUIT tenga el formato correcto.
        /// Sin dígito verificador: 10 dígitos consecutivos o 12 caracteres con guiones según el formato (99-99999999-)
        /// Con dígito verificador: 11 dígitos consecutivos o 13 caracteres con guiones según el formato (99-99999999-9)
        /// </summary>
        /// <param name="value">La CUIT a verificar.</param>
        /// <returns>True si el formato es correcto, False si no.</returns>
        public static bool VerificarFormato(string value)
        {
            value = value.Trim();
            string valueCleaned = Regex.Replace(value, "[^\\d]", string.Empty);
            return VerificarFormato(value, valueCleaned);
        }

        public static byte? ObtenerDigitoVerificador(string value)
        {
            int total;

            value = value.Trim();
            string valueCleaned = Regex.Replace(value, "[^\\d]", string.Empty);
            if (!VerificarFormato(value, valueCleaned))
            {
                return null;
            }

            // Multiplico los dígitos
            total = Convert.ToInt16(valueCleaned[0].ToString()) * 5;
            total += Convert.ToInt16(valueCleaned[1].ToString()) * 4;
            total += Convert.ToInt16(valueCleaned[2].ToString()) * 3;
            total += Convert.ToInt16(valueCleaned[3].ToString()) * 2;
            total += Convert.ToInt16(valueCleaned[4].ToString()) * 7;
            total += Convert.ToInt16(valueCleaned[5].ToString()) * 6;
            total += Convert.ToInt16(valueCleaned[6].ToString()) * 5;
            total += Convert.ToInt16(valueCleaned[7].ToString()) * 4;
            total += Convert.ToInt16(valueCleaned[8].ToString()) * 3;
            total += Convert.ToInt16(valueCleaned[9].ToString()) * 2;

            return (byte)((11 - (total % 11)) % 11);
        }

        public static bool Verificar(long value)
        {
            if (value.ToString().Length != 11)
            {
                return false;
            }
            return Verificar(value.ToString());
        }

        public static bool Verificar(string value)
        {
            value = value.Trim();
            string valueCleaned = Regex.Replace(value, "[^\\d]", string.Empty);
            if (!VerificarFormato(value, valueCleaned))
            {
                return false;
            }

            // Verifico que el prefijo sea uno de los correctos
            string prefijo = valueCleaned.Substring(0, 2);
            if (prefijo != PrefijoMasculino && prefijo != PrefijoMasculinoAlternativo && prefijo != PrefijoFemenino && prefijo != PrefijoFemeninoAlternativo && prefijo != PrefijoPersonaJuridica && prefijo != PrefijoPersonaJuridicaAlternativo1 && prefijo != PrefijoPersonaJuridicaAlternativo2)
            {
                return false;
            }

            // Verifico el dígito verificador
            byte? digitoVerificador = ObtenerDigitoVerificador(valueCleaned.Substring(0, 10));
            if (digitoVerificador == null)
            {
                return false;
            }
            else
            {
                return valueCleaned[10] == digitoVerificador.ToString()[0];
            }
        }
    }
}