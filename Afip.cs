using System;
using System.Text.RegularExpressions;

namespace CardonerSistemas
{
    static class Afip
    {
        internal enum ConceptosComprobantes : byte
        { 
            Productos = 1,
            Servicios = 2,
            ProductosYServicios = 3,
            Otro = 4
        }

        static internal byte? ObtenerDigitoVerificadorCuit(string cuit)
        {
            string cuitLimpio;
            string cuitAVerificar;
            int total;

            // Limpio los espacios
            cuit = cuit.Trim();
            cuitLimpio = Regex.Replace(cuit, "[^\\d]", "");

            // Verifico que el número tenga el formato correcto de:
            // 10 dígitos consecutivos o 12 caracteres con guiones según el formato (99-99999999-)
            switch (cuit.Length)
            {
                case 10:
                    if (cuitLimpio.Length < 10)
                    {
                        return null;
                    }
                    break;
                case 12:
                    if (cuit[2] == '-' && cuit[9] == '-')
                    {
                        if (cuitLimpio.Length < 10)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                    break;
                default:
                    return null;
            }
            cuitAVerificar = cuitLimpio;

            // Individualiza y multiplica los dígitos
            total = Convert.ToInt16(cuitAVerificar[0].ToString()) * 5;
            total += Convert.ToInt16(cuitAVerificar[1].ToString()) * 4;
            total += Convert.ToInt16(cuitAVerificar[2].ToString()) * 3;
            total += Convert.ToInt16(cuitAVerificar[3].ToString()) * 2;
            total += Convert.ToInt16(cuitAVerificar[4].ToString()) * 7;
            total += Convert.ToInt16(cuitAVerificar[5].ToString()) * 6;
            total += Convert.ToInt16(cuitAVerificar[6].ToString()) * 5;
            total += Convert.ToInt16(cuitAVerificar[7].ToString()) * 4;
            total += Convert.ToInt16(cuitAVerificar[8].ToString()) * 3;
            total += Convert.ToInt16(cuitAVerificar[9].ToString()) * 2;

            return (byte)((11 - (total % 11)) % 11);
        }

        static internal bool VerificarCuit(string cuit)
        {
            string cuitLimpio;
            string cuitAVerificar;
            string prefijo;
            byte? digitoVerificador;

            // Limpio los espacios
            cuit = cuit.Trim();
            cuitLimpio = Regex.Replace(cuit, "[^\\d]", "");

            // Verifico que el número tenga el formato correcto de:
            // 11 dígitos consecutivos o 13 caracteres con guiones según el formato (99-99999999-9)
            switch (cuit.Length)
            {
                case 11:
                    if (cuitLimpio.Length < 11)
                    {
                        return false;
                    }
                    break;
                case 13:
                    if (cuit[2] == '-' && cuit[9] == '-')
                    {
                        if (cuitLimpio.Length < 11)
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
            cuitAVerificar = cuitLimpio;

            prefijo = cuitAVerificar.Substring(0, 2);
            if (prefijo == "20" | prefijo == "23" | prefijo == "24" | prefijo == "27" | prefijo == "30" | prefijo == "33" | prefijo == "34")
            {
                digitoVerificador = ObtenerDigitoVerificadorCuit(cuitAVerificar.Substring(0, 10));
                if (digitoVerificador == null)
                {
                    return false;
                }
                else
                {
                    return (Convert.ToInt16(cuitAVerificar[10].ToString()) == digitoVerificador);
                }
            }
            else
            {
                return false;
            }
        }
    }
}
