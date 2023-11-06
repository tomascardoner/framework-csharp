using System;
using CSGestion.Properties;

namespace CardonerSistemas
{
    static class DateTimeExtension
    {
        
        #region Declarations
        
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
            DayToday,
            DayYesterday,
            DayBeforeYesterday,
            DayLast2,
            DayLast3,
            DayLast4,
            DayLast7,
            DayLast15
        }

        internal enum PeriodWeekValues : byte
        {
            WeekCurrent,
            WeekBeforeCurrent,
            WeekLast2,
            WeekLast3
        }

        internal enum PeriodMonthValues : byte
        {
            MonthCurrent,
            MonthBeforeCurrent,
            MonthLast2,
            MonthLast3,
            MonthLast6
        }

        internal enum PeriodYearValues : byte
        {
            YearCurrent,
            YearBeforeCurrent
        }

        internal enum PeriodRangeValues : byte
        {
            DateEqual,
            DateBefore,
            DateAfter,
            DateBetween
        }

        #endregion

        #region ComboBoxs de filtros de fechas

        internal static void FillPeriodTypesComboBox(System.Windows.Forms.ComboBox control, PeriodTypes selectedPeriodType)
        {
            control.Items.AddRange(new string[] { Resources.StringItemAllMaleEnclosed, "Día:", "Semana:", "Mes:", "Año:", "Fecha" });
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
                    control.Items.AddRange(new string[] { "Hoy", "Ayer", "Anteayer", "Últimos 2", "Últimos 3", "Últimos 4", "Últimos 7", "Últimos 15" });
                    break;
                case PeriodTypes.Week:
                    control.Items.AddRange(new string[] { "Actual", "Anterior", "Últimas 2", "Últimas 3" });
                    break;
                case PeriodTypes.Month:
                    control.Items.AddRange(new string[] { "Actual", "Anterior", "Últimos 2", "Últimos 3", "Últimos 6" });
                    break;
                case PeriodTypes.Year:
                    control.Items.AddRange(new string[] { "Actual", "Anterior" });
                    break;
                case PeriodTypes.Range:
                    control.Items.AddRange(new string[] { "es igual a:", "es anterior a:", "es posterior a:", "está entre:" });
                    break;
            }
            control.Visible = (periodType != PeriodTypes.All);
            control.SelectedIndex = 0;
        }

        internal static void GetDatesFromPeriodTypeAndValue(PeriodTypes periodType, byte periodValue, ref System.DateTime dateFrom, ref System.DateTime dateTo, System.DateTime dateValueFrom, System.DateTime dateValueTo)
        {
            switch (periodType)
            {
                // All
                case PeriodTypes.All:
                    dateFrom = System.DateTime.MinValue;
                    dateTo = System.DateTime.MaxValue;
                    break;
                // Days
                case PeriodTypes.Day:
                    PeriodDayValues periodDayValue = (PeriodDayValues)periodValue;

                    switch (periodDayValue)
                    {
                        case PeriodDayValues.DayToday:
                            dateFrom = System.DateTime.Today;
                            dateTo = dateFrom.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.DayYesterday:
                            dateFrom = System.DateTime.Today.AddDays(-1);
                            dateTo = dateFrom.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.DayBeforeYesterday:
                            dateFrom = System.DateTime.Today.AddDays(-2);
                            dateTo = dateFrom.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.DayLast2:
                            dateFrom = System.DateTime.Today.AddDays(-1);
                            dateTo = System.DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.DayLast3:
                            dateFrom = System.DateTime.Today.AddDays(-2);
                            dateTo = System.DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.DayLast4:
                            dateFrom = System.DateTime.Today.AddDays(-3);
                            dateTo = System.DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.DayLast7:
                            dateFrom = System.DateTime.Today.AddDays(-6);
                            dateTo = System.DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodDayValues.DayLast15:
                            dateFrom = System.DateTime.Today.AddDays(-14);
                            dateTo = System.DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                    }
                    break;

                // Weeks
                case PeriodTypes.Week:
                    PeriodWeekValues periodWeekValue = (PeriodWeekValues)periodValue;

                    switch (periodWeekValue)
                    {
                        case PeriodWeekValues.WeekCurrent:
                            dateFrom = System.DateTime.Today.AddDays(-(double)System.DateTime.Today.DayOfWeek);
                            dateTo = System.DateTime.Today.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodWeekValues.WeekBeforeCurrent:
                            dateFrom = System.DateTime.Today.AddDays(-(double)System.DateTime.Today.DayOfWeek - 7);
                            dateTo = System.DateTime.Today.AddDays(-(double)System.DateTime.Today.DayOfWeek).AddMilliseconds(-1);
                            break;
                        case PeriodWeekValues.WeekLast2:
                            dateFrom = System.DateTime.Today.AddDays(-(double)System.DateTime.Today.DayOfWeek - 7);
                            dateTo = System.DateTime.Today.AddDays(7 - (double)System.DateTime.Today.DayOfWeek).AddMilliseconds(-1);
                            break;
                        case PeriodWeekValues.WeekLast3:
                            dateFrom = System.DateTime.Today.AddDays(-(double)System.DateTime.Today.DayOfWeek - 14);
                            dateTo = System.DateTime.Today.AddDays(7 - (double)System.DateTime.Today.DayOfWeek).AddMilliseconds(-1);
                            break;
                    }
                    break;

                // Months
                case PeriodTypes.Month:
                    PeriodMonthValues periodMonthValue = (PeriodMonthValues)periodValue;

                    switch (periodMonthValue)
                    {
                        case PeriodMonthValues.MonthCurrent:
                            dateFrom = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1);
                            dateTo = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(1).AddMilliseconds(-1);
                            break;
                        case PeriodMonthValues.MonthBeforeCurrent:
                            dateFrom = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(-1);
                            dateTo = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMilliseconds(-1);
                            break;
                        case PeriodMonthValues.MonthLast2:
                            dateFrom = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(-1);
                            dateTo = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(1).AddMilliseconds(-1);
                            break;
                        case PeriodMonthValues.MonthLast3:
                            dateFrom = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(-2);
                            dateTo = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(1).AddMilliseconds(-1);
                            break;
                        case PeriodMonthValues.MonthLast6:
                            dateFrom = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(-5);
                            dateTo = new System.DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(1).AddMilliseconds(-1);
                            break;
                    }
                    break;

                // Years
                case PeriodTypes.Year:
                    PeriodYearValues periodYearValue = (PeriodYearValues)periodValue;

                    switch (periodYearValue)
                    {
                        case PeriodYearValues.YearCurrent:
                            dateFrom = new System.DateTime(System.DateTime.Today.Year, 1, 1);
                            dateTo = new System.DateTime(System.DateTime.Today.Year, 1, 1).AddYears(1).AddMilliseconds(-1);
                            break;
                        case PeriodYearValues.YearBeforeCurrent:
                            dateFrom = new System.DateTime(System.DateTime.Today.Year, 1, 1).AddYears(-1);
                            dateTo = new System.DateTime(System.DateTime.Today.Year, 1, 1).AddMilliseconds(-1);
                            break;
                    }
                    break;

                // Range
                case PeriodTypes.Range:
                    PeriodRangeValues periodRangeValue = (PeriodRangeValues)periodValue;

                    switch (periodRangeValue)
                    {
                        case PeriodRangeValues.DateEqual:
                            dateFrom = dateValueFrom;
                            dateTo = dateValueFrom.AddDays(1).AddMilliseconds(-1);
                            break;
                        case PeriodRangeValues.DateBefore:
                            dateFrom = System.DateTime.MinValue;
                            dateTo = dateValueFrom.AddMilliseconds(-1);
                            break;
                        case PeriodRangeValues.DateAfter:
                            dateFrom = dateValueFrom.AddDays(1);
                            dateTo = System.DateTime.MaxValue;
                            break;
                        case PeriodRangeValues.DateBetween:
                            dateFrom = dateValueFrom;
                            dateTo = dateValueTo.AddDays(1).AddMilliseconds(-1);
                            break;
                    }
                    break;
            }
        }

        #endregion

        internal static long GetElapsedCompleteYearsFromDates(System.DateTime startDate, System.DateTime endDate)
        {
            long elapsedYears;

            elapsedYears = endDate.Year - startDate.Year;
            if ((startDate.Month > endDate.Month) | (startDate.Month == endDate.Month && startDate.Day > endDate.Day))
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