using System;

namespace CardonerSistemas
{
    static class DateTimeExtension
    {

        #region Declarations

        private static readonly string[] itemsPeriodTypes = { "«Todos»", "Día:", "Semana:", "Mes:", "Año:", "Fecha" };
        private static readonly string[] itemsDays = { "Hoy", "Ayer", "Anteayer", "Últimos 2", "Últimos 3", "Últimos 4", "Últimos 7", "Últimos 15" };
        private static readonly string[] itemsWeeks = { "Actual", "Anterior", "Últimas 2", "Últimas 3" };
        private static readonly string[] itemsMonths = { "Actual", "Anterior", "Últimos 2", "Últimos 3", "Últimos 6" };
        private static readonly string[] itemsYears = { "Actual", "Anterior", "Últimos 2" };
        private static readonly string[] itemsRange = { "es igual a:", "es anterior a:", "es posterior a:", "está entre:" };

        internal enum PeriodTypes : byte
        {
            All,
            Day,
            Week,
            Month,
            Year,
            Range            
        }
        
        internal enum PeriodDayValues : byte
        {
            Today,
            Yesterday,
            BeforeYesterday,
            Last2,
            Last3,
            Last4,
            Last7,
            Last15
        }

        internal enum PeriodWeekValues : byte
        {
            Current,
            BeforeCurrent,
            Last2,
            Last3
        }

        internal enum PeriodMonthValues : byte
        {
            Current,
            BeforeCurrent,
            Last2,
            Last3,
            Last6
        }

        internal enum PeriodYearValues : byte
        {
            Current,
            BeforeCurrent,
            Last2
        }

        internal enum PeriodRangeValues : byte
        {
            Equal,
            Before,
            After,
            Between
        }

        #endregion

        #region ComboBoxs de filtros de fechas

        internal static void FillPeriodTypesComboBox(System.Windows.Forms.ComboBox control, PeriodTypes selectedPeriodType)
        {
            control.Items.AddRange(itemsPeriodTypes);
            control.SelectedIndex = (int)selectedPeriodType;
        }

        internal static void FillPeriodValuesComboBox(System.Windows.Forms.ComboBox control, PeriodTypes periodType)
        {
            control.Items.Clear();
            switch (periodType)
            {
                case PeriodTypes.All:
                    control.Items.Add(string.Empty);
                    break;
                case PeriodTypes.Day:
                    control.Items.AddRange(itemsDays);
                    break;
                case PeriodTypes.Week:
                    control.Items.AddRange(itemsWeeks);
                    break;
                case PeriodTypes.Month:
                    control.Items.AddRange(itemsMonths);
                    break;
                case PeriodTypes.Year:
                    control.Items.AddRange(itemsYears);
                    break;
                case PeriodTypes.Range:
                    control.Items.AddRange(itemsRange);
                    break;
            }
            control.Visible = (periodType != PeriodTypes.All);
            control.SelectedIndex = 0;
        }

        internal static void GetDatesFromPeriodTypeAndValue(PeriodTypes periodType, byte periodValue, out DateTime dateFrom, out DateTime dateTo, DateTime dateValueFrom, DateTime dateValueTo)
        {
            dateFrom = DateTime.MinValue;
            dateTo = DateTime.MaxValue;

            dateValueFrom = dateValueFrom.Date;
            dateValueTo = dateValueTo.Date;

            switch (periodType)
            {
                // All
                case PeriodTypes.All:
                    dateFrom = DateTime.MinValue;
                    dateTo = DateTime.MaxValue;
                    break;
                // Days
                case PeriodTypes.Day:
                    PeriodDayValues periodDayValue = (PeriodDayValues)periodValue;

                    switch (periodDayValue)
                    {
                        case PeriodDayValues.Today:
                            dateFrom = DateTime.Today;
                            dateTo = dateFrom.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.Yesterday:
                            dateFrom = DateTime.Today.AddDays(-1);
                            dateTo = dateFrom.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.BeforeYesterday:
                            dateFrom = DateTime.Today.AddDays(-2);
                            dateTo = dateFrom.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.Last2:
                            dateFrom = DateTime.Today.AddDays(-1);
                            dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.Last3:
                            dateFrom = DateTime.Today.AddDays(-2);
                            dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.Last4:
                            dateFrom = DateTime.Today.AddDays(-3);
                            dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.Last7:
                            dateFrom = DateTime.Today.AddDays(-6);
                            dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.Last15:
                            dateFrom = DateTime.Today.AddDays(-14);
                            dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                    }
                    break;

                // Weeks
                case PeriodTypes.Week:
                    PeriodWeekValues periodWeekValue = (PeriodWeekValues)periodValue;

                    switch (periodWeekValue)
                    {
                        case PeriodWeekValues.Current:
                            dateFrom = DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek);
                            dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodWeekValues.BeforeCurrent:
                            dateFrom = DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek - 7);
                            dateTo = DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek).AddMilliseconds(-1);
                            break;
                        case PeriodWeekValues.Last2:
                            dateFrom = DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek - 7);
                            dateTo = DateTime.Today.AddDays(7 - (double)DateTime.Today.DayOfWeek).AddMilliseconds(-1);
                            break;
                        case PeriodWeekValues.Last3:
                            dateFrom = DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek - 14);
                            dateTo = DateTime.Today.AddDays(7 - (double)DateTime.Today.DayOfWeek).AddMilliseconds(-1);
                            break;
                    }
                    break;

                // Months
                case PeriodTypes.Month:
                    PeriodMonthValues periodMonthValue = (PeriodMonthValues)periodValue;

                    switch (periodMonthValue)
                    {
                        case PeriodMonthValues.Current:
                            dateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                            dateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddMilliseconds(-1);
                            break;
                        case PeriodMonthValues.BeforeCurrent:
                            dateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                            dateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMilliseconds(-1);
                            break;
                        case PeriodMonthValues.Last2:
                            dateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                            dateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddMilliseconds(-1);
                            break;
                        case PeriodMonthValues.Last3:
                            dateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-2);
                            dateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddMilliseconds(-1);
                            break;
                        case PeriodMonthValues.Last6:
                            dateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-5);
                            dateTo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddMilliseconds(-1);
                            break;
                    }
                    break;

                // Years
                case PeriodTypes.Year:
                    PeriodYearValues periodYearValue = (PeriodYearValues)periodValue;

                    switch (periodYearValue)
                    {
                        case PeriodYearValues.Current:
                            dateFrom = new DateTime(DateTime.Today.Year, 1, 1);
                            dateTo = new DateTime(DateTime.Today.Year, 1, 1).AddYears(1).AddMilliseconds(-1);
                            break;
                        case PeriodYearValues.BeforeCurrent:
                            dateFrom = new DateTime(DateTime.Today.Year, 1, 1).AddYears(-1);
                            dateTo = new DateTime(DateTime.Today.Year, 1, 1).AddMilliseconds(-1);
                            break;
                        case PeriodYearValues.Last2:
                            dateFrom = new DateTime(DateTime.Today.Year, 1, 1).AddYears(-1);
                            dateTo = new DateTime(DateTime.Today.Year, 1, 1).AddYears(1).AddMilliseconds(-1);
                            break;
                    }
                    break;

                // Range
                case PeriodTypes.Range:
                    PeriodRangeValues periodRangeValue = (PeriodRangeValues)periodValue;

                    switch (periodRangeValue)
                    {
                        case PeriodRangeValues.Equal:
                            dateFrom = dateValueFrom;
                            dateTo = dateValueFrom.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodRangeValues.Before:
                            dateFrom = DateTime.MinValue;
                            dateTo = dateValueFrom.AddMilliseconds(-1);
                            break;
                        case PeriodRangeValues.After:
                            dateFrom = dateValueFrom.AddDays(1);
                            dateTo = DateTime.MaxValue;
                            break;
                        case PeriodRangeValues.Between:
                            dateFrom = dateValueFrom;
                            dateTo = dateValueTo.AddDays(1).AddMilliseconds(-1);
                            break;
                    }
                    break;
            }
        }

        #endregion

        internal static long GetElapsedCompleteYearsFromDates(DateTime startDate, DateTime endDate)
        {
            long elapsedYears;

            elapsedYears = endDate.Year - startDate.Year;
            if ((startDate.Month > endDate.Month) || (startDate.Month == endDate.Month && startDate.Day > endDate.Day))
            {
                elapsedYears--;
            }
            return elapsedYears;
        }

        internal static string GetElapsedYearsMonthsAndDaysFromDays(int elapsedDays)
        {
            Decimal daysInAYear;
            short elapsedYears;
            byte elapsedMonths;
            string result;

            if (elapsedDays > 1460)
            {
                // Elapsed more than 4 years, so take aproximate account of leap years
                daysInAYear = (Decimal)365.25;
            }
            else
            {
                daysInAYear = 365;
            }

            // Get elapsed years and the remaining days
            elapsedYears = (short)(elapsedDays / daysInAYear);
            elapsedDays = (short)(elapsedDays % daysInAYear);

            // Get elapsed months and the remainig days
            elapsedMonths = (byte)(elapsedDays / 30);
            elapsedDays %= 30;

            // Years
            switch (elapsedYears)
            {
                case 0:
                    result = string.Empty;
                    break;
                case 1:
                    result = "1 año";
                    break;
                default:
                    result = $"{elapsedYears} años";
                    break;
            }

            // Months
            if (elapsedMonths > 0)
            {
                if (result.Length > 0)
                {
                    if (elapsedDays == 0)
                    {
                        result += " y ";
                    }
                    else
                    {
                        result += ", ";
                    }
                }

                if (elapsedMonths == 1)
                {
                    result += "1 mes";
                }
                else
                {
                    result += $"{elapsedMonths} meses";
                }
            }

            // Days
            if (elapsedDays > 0)
            {
                if (result.Length > 0)
                {
                    result += " y ";
                }

                if (elapsedDays == 1)
                {
                    result += "1 día";
                }
                else
                {
                    result += $"{elapsedDays} días";
                }
            }

            // Final dot
            if (result.Length > 0)
            {
                result += ".";
            }

            return result;
        }

    }
}