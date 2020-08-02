namespace CardonerSistemas
{
    static class Parameters
    {
        internal enum ParameterTypes : byte
        { 
            TextShort = 1,
            TextLong = 2,
            NumberInteger = 3,
            NumberDecimal = 4,
            Money = 5,
            DateTime = 6,
            Date = 7,
            Time = 8,
            YesNo = 9,
            Image = 10,
            ListOfTextShort = 51,
            ReportsCompany = 101,
            ReportsTitle = 102,
            ReportsFilterText = 103,
            ReportsFilterTextShow = 104
        };

    }
}