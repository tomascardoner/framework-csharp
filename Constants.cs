using System;

namespace CardonerSistemas
{
    static class Constants
    {
        internal const string PublicEncryptionPassword = "CmcaTlMdmA,aTmP,am2CyalhdEb";

        // ComboBoxes
        internal const byte ComboBoxAllYesNoListIndexForAll = 0;
        internal const byte ComboBoxAllYesNoListIndexForYes = 1;
        internal const byte ComboBoxAllYesNoListIndexForNo = 2;

        // To String formats
        internal const string FormatStringToNumber = "N";
        internal const string FormatStringToNumberInteger = "G";
        internal const string FormatStringToCurrency = "C";
        internal const string FormatStringToPercent = "P";
        internal const string FormatStringToHexadecimal = "X";

        //////////////////////
        //    FIELD VALUES
        //////////////////////
        public const byte ByteFieldValueNotSpecified = 0;
        public const short ShortFieldValueNotSpecified = 0;
        public const int IntegerFieldValueNotSpecified = 0;

        public const byte ByteFieldValueAll = byte.MaxValue;
        public const short ShortFieldValueAll = short.MaxValue;
        public const int IntegerFieldValueAll = int.MaxValue;

        public const byte ByteFieldValueOther = 254;
        public const short ShortFieldValueOther = 32766;
        public const int IntegerFieldValueOther = 2147483646;

        ////////////////////
        //    STRINGS
        ////////////////////
        public const string KeyStringer = "@";
        public const string KeyDelimiter = "|";

        public const string StringListSeparator = ";";
        public const string StringListDelimiter = "¬";

        ////////////////////
        //    E-MAIL
        ////////////////////
        public const string EmailClientNetDll = "NETCLIENT";
        public const string EmailClientMSOutlook = "MSOUTLOOK";
        public const string EmailClientCrystalReportsMapi = "CRYSTALMAPI";

    }
}