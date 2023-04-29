namespace CardonerSistemas
{
    static class Barcode
    {

        #region Documentation

        // Códigos de barras utilizados en la identificación de productos:

        // EAN-13: consta de 13 dígitos y es la versión más difundida a nivel mundial
        //  - 3 dígitos: organización de codificación (Argentina: 779)
        //  - 4 dígitos: código de la empresa
        //  - 5 dígitos: identificacion del producto
        //  - 1 dígito: verificador

        // EAN-8: consta de 8 dígitos y es la versión compacta del EAN-13
        //  - 3 dígitos: organización de codificación (Argentina: 779)
        //  - 4 dígitos: identificacion del producto
        //  - 1 dígito: verificador

        // DUN-14: consta de 14 dígitos y se utiliza en unidades de despacho o cajas. Codificación ITF-14 (Interleaved Two of Five)
        //  - 1 dígito: variable logística (indica la cantidad de unidades de consumo que contiene la unidad de despacho)
        //  - 3 dígitos: organización de codificación (Argentina: 779)
        //  - 4 dígitos: código de la empresa
        //  - 5 dígitos: identificacion del producto
        //  - 1 dígito: verificador

        #endregion

        #region Parse codes

        internal static void GetParts(long value, out byte? logisticValue, out short? organizationValue, out short? manufacturerValue, out int? productValue, out byte? controlValue)
        {
            if (value <= 0)
            {
                logisticValue = null;
                organizationValue = null;
                manufacturerValue = null;
                productValue = null;
                controlValue = null;
                return;
            }
            string stringedValue = value.ToString();
            switch (stringedValue.Length)
            {
                case 8:
                    logisticValue = null;
                    organizationValue = short.Parse(stringedValue.Substring(0, 3));
                    manufacturerValue = null;
                    productValue = int.Parse(stringedValue.Substring(3, 4));
                    controlValue = byte.Parse(stringedValue.Substring(7, 1));
                    break;
                case 13:
                    logisticValue = null;
                    organizationValue = short.Parse(stringedValue.Substring(0, 3));
                    manufacturerValue = short.Parse(stringedValue.Substring(3, 4));
                    productValue = int.Parse(stringedValue.Substring(7, 5));
                    controlValue = byte.Parse(stringedValue.Substring(12, 1));
                    break;
                case 14:
                    logisticValue = byte.Parse(stringedValue.Substring(1, 1));
                    organizationValue = short.Parse(stringedValue.Substring(1, 3));
                    manufacturerValue = short.Parse(stringedValue.Substring(4, 4));
                    productValue = int.Parse(stringedValue.Substring(8, 5));
                    controlValue = byte.Parse(stringedValue.Substring(13, 1));
                    break;
                default:
                    logisticValue = null;
                    organizationValue = null;
                    manufacturerValue = null;
                    productValue = null;
                    controlValue = null;
                    break;
            }
        }

        #endregion

    }
}
