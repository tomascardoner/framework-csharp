using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CardonerSistemas
{
    class Bank
    {
        // Formato de CBU
        // La CBU debe ser ingresada en 2 bloques:

        // El 1º bloque contiene:
        //      • Banco (3 dígitos)
        //      • Dígito Verificador 1 (1 dígito)
        //      • Sucursal (3 dígitos)
        //      • Dígito Verificador 2 (1 digito)

        // El 2º bloque contiene:
        //      • Cuenta (13 dígitos)
        //      • Dígito Verificador (1 dígito)

        static internal byte? ObtenerDigitoVerificadorCbuBloque1(string cbuBloque1)
        {
            string cbuBloque1Limpio;
            int total;

            // Limpio los espacios anterior y posterior que pudiera tener el string
            cbuBloque1 = cbuBloque1.Trim();
            cbuBloque1Limpio = Regex.Replace(cbuBloque1, "[^\\d]", "");

            // Verifico que el número tenga la longitud correcta
            if (cbuBloque1.Length == 7)
            {
                if (cbuBloque1Limpio.Length < 7)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            cbuBloque1 = cbuBloque1Limpio;

            // Individualiza y multiplica los dígitos
            total = Convert.ToInt16(cbuBloque1[0].ToString()) * 7;
            total += Convert.ToInt16(cbuBloque1[1].ToString()) * 1;
            total += Convert.ToInt16(cbuBloque1[2].ToString()) * 3;
            total += Convert.ToInt16(cbuBloque1[3].ToString()) * 9;
            total += Convert.ToInt16(cbuBloque1[4].ToString()) * 7;
            total += Convert.ToInt16(cbuBloque1[5].ToString()) * 1;
            total += Convert.ToInt16(cbuBloque1[6].ToString()) * 3;

            // Calcula el dígito verificador
            byte digitoVerificador;
            digitoVerificador = (byte)(10 - Convert.ToByte(total.ToString().Last().ToString()));

            return digitoVerificador;
        }

        static internal byte? ObtenerDigitoVerificadorCbuBloque2(string cbuBloque2)
        {
            string cbuBloque2Limpio;
            int total;

            // Limpio los espacios anterior y posterior que pudiera tener el string
            cbuBloque2 = cbuBloque2.Trim();
            cbuBloque2Limpio = Regex.Replace(cbuBloque2, "[^\\d]", "");

            // Verifico que el número tenga la longitud correcta
            if (cbuBloque2.Length == 13)
            {
                if (cbuBloque2Limpio.Length < 13)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            cbuBloque2 = cbuBloque2Limpio;

            // Individualiza y multiplica los dígitos
            total = Convert.ToInt16(cbuBloque2[0].ToString()) * 3;
            total += Convert.ToInt16(cbuBloque2[1].ToString()) * 9;
            total += Convert.ToInt16(cbuBloque2[2].ToString()) * 7;
            total += Convert.ToInt16(cbuBloque2[3].ToString()) * 1;
            total += Convert.ToInt16(cbuBloque2[4].ToString()) * 3;
            total += Convert.ToInt16(cbuBloque2[5].ToString()) * 9;
            total += Convert.ToInt16(cbuBloque2[6].ToString()) * 7;
            total += Convert.ToInt16(cbuBloque2[7].ToString()) * 1;
            total += Convert.ToInt16(cbuBloque2[8].ToString()) * 3;
            total += Convert.ToInt16(cbuBloque2[9].ToString()) * 9;
            total += Convert.ToInt16(cbuBloque2[10].ToString()) * 7;
            total += Convert.ToInt16(cbuBloque2[11].ToString()) * 1;
            total += Convert.ToInt16(cbuBloque2[12].ToString()) * 3;

            // Calcula el dígito verificador
            byte digitoVerificador;
            digitoVerificador = (byte)(10 - Convert.ToByte(total.ToString().Last().ToString()));

            return digitoVerificador;
        }

        // Esta función verifica una CBU y devuelve:
        //   -1 si es correcta
        //    0 si es incorrecta por el formato
        //    1 si tiene un error en el primer bloque
        //    2 si tiene un error en el segundo bloque
        static internal short VerificarCBU(string cbu)
        {
            string cbuLimpio;
            byte? digitoVerificador;

            // Limpio los espacios anterior y posterior que pudiera tener el string
            cbu = cbu.Trim();
            cbuLimpio = Regex.Replace(cbu, "[^\\d]", "");

            // Verifico que el número tenga el formato correcto de:
            // 22 dígitos consecutivos o 25 caracteres con guiones según el formato (0000000-0 0000000000000-0)
            switch (cbu.Length)
            {
                case 22:
                    if (cbuLimpio.Length < 22)
                    {
                        return 0;
                    }
                    break;
                case 25:
                    if (cbu[7] == '-' && cbu[9] == ' ' && cbu[23] == '-')
                    {
                        if (cbuLimpio.Length < 22)
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                    break;
                default:
                    return 0;
            }
            cbu = cbuLimpio;

            // Verifico el bloque 1
            digitoVerificador = ObtenerDigitoVerificadorCbuBloque1(cbu.Substring(0, 7));
            if (digitoVerificador == null)
            {
                return 0;
            }
            else
            {
                if (Convert.ToByte(cbu[7].ToString()) != digitoVerificador)
                {
                    return 1;
                }
            }

            // Verifico el bloque 2
            digitoVerificador = ObtenerDigitoVerificadorCbuBloque2(cbu.Substring(8, 13));
            if (digitoVerificador == null)
            {
                return 0;
            }
            else
            {
                if (Convert.ToByte(cbu[21].ToString()) != digitoVerificador)
                {
                    return 2;
                }
            }

            // CBU correcto
            return -1;
        }


        static internal bool CBUCorrecta(string cbu)
        {
            return (VerificarCBU(cbu) == -1);
        }

    }
}
