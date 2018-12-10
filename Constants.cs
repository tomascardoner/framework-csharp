using System;

namespace CardonerSistemas
{
    static class Constants
    {
        public const string PublicEncryptionPassword = "CmcaTlMdmA,aTmP,am2CyalhdEb";

        public const byte COMBOBOX_ALLYESNO_ALL_LISTINDEX = 0;
        public const byte COMBOBOX_ALLYESNO_YES_LISTINDEX = 1;
        public const byte COMBOBOX_ALLYESNO_NO_LISTINDEX = 2;

        //////////////////////
        //    FIELD VALUES
        //////////////////////
        public const byte FIELD_VALUE_NOTSPECIFIED_BYTE = 0;
        public const short FIELD_VALUE_NOTSPECIFIED_SHORT = 0;
        public const int FIELD_VALUE_NOTSPECIFIED_INTEGER = 0;
        //public const DateTime FIELD_VALUE_NOTSPECIFIED_DATE = new DateTime();

        public const byte FIELD_VALUE_ALL_BYTE = byte.MaxValue;
        public const short FIELD_VALUE_ALL_SHORT = short.MaxValue;
        public const int FIELD_VALUE_ALL_INTEGER = int.MaxValue;

        public const byte FIELD_VALUE_OTHER_BYTE = 254;
        public const short FIELD_VALUE_OTHER_SHORT = 32766;
        public const int FIELD_VALUE_OTHER_INTEGER = 2147483646;

        ////////////////////
        //    STRINGS
        ////////////////////
        public const string KeyStringer = "@";
        public const string KeyDelimiter = "|";

        public const string StringListSeparator = "|";
        public const string StringListDelimiter = "¬";

        ////////////////////
        //    E-MAIL
        ////////////////////
        public const string EMAIL_CLIENT_NETDLL = "NETCLIENT";
        public const string EMAIL_CLIENT_MSOUTLOOK = "MSOUTLOOK";
        public const string EMAIL_CLIENT_CRYSTALREPORTSMAPI = "CRYSTALMAPI";
    }
}