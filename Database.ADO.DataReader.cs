using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace CardonerSistemas.Database.ADO
{
    internal class DataReader
    {

        #region Get values

        internal static bool IsDBNull(SqlDataReader dataReader, string columnName)
        { 
            return dataReader.IsDBNull(dataReader.GetOrdinal(columnName));
        }

        internal static string GetString(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetString(dataReader.GetOrdinal(columnName));
        }

        internal static string GetStringSafeAsEmpty(SqlDataReader dataReader, string columnName)
        {
            string result = GetStringSafeAsNull(dataReader, columnName);
            if (result == null)
            {
                return System.String.Empty;
            }
            else
            {
                return result;
            }
        }

        internal static string GetStringSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return null;
            }
            else
            {
                return dataReader.GetString(columnIndex);
            }
        }

        internal static byte GetByte(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetByte(dataReader.GetOrdinal(columnName));
        }

        internal static byte GetByteSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            byte? result = GetByteSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return byte.MinValue;
            }
        }

        internal static byte? GetByteSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return null;
            }
            else
            {
                return dataReader.GetByte(columnIndex);
            }
        }

        internal static short GetShort(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt16(dataReader.GetOrdinal(columnName));
        }

        internal static short GetShortSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            short? result = GetShortSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return short.MinValue;
            }
        }

        internal static short? GetShortSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return null;
            }
            else
            {
                return dataReader.GetInt16(columnIndex);
            }
        }

        internal static int GetInteger(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt32(dataReader.GetOrdinal(columnName));
        }

        internal static int GetIntegerSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            int? result = GetIntegerSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return int.MinValue;
            }
        }

        internal static int? GetIntegerSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return null;
            }
            else
            {
                return dataReader.GetInt32(columnIndex);
            }
        }

        internal static long GetLong(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt64(dataReader.GetOrdinal(columnName));
        }

        internal static long GetLongSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            long? result = GetLongSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return long.MinValue;
            }
        }

        internal static long? GetLongSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return null;
            }
            else
            {
                return dataReader.GetInt64(columnIndex);
            }
        }

        internal static decimal GetDecimal(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDecimal(dataReader.GetOrdinal(columnName));
        }

        internal static decimal GetDecimalSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            decimal? result = GetDecimalSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return decimal.MinValue;
            }
        }

        internal static decimal? GetDecimalSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return null;
            }
            else
            {
                return dataReader.GetDecimal(columnIndex);
            }
        }

        public static bool GetBoolean(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetBoolean(dataReader.GetOrdinal(columnName));
        }

        internal static byte GetBooleanSafeAsByte(SqlDataReader dataReader, string columnName)
        {
            bool? result = GetBooleanSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 2;
            }
        }

        internal static bool? GetBooleanSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return null;
            }
            else
            {
                return dataReader.GetBoolean(columnIndex);
            }
        }

        internal static DateTime GetDateTime(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDateTime(dataReader.GetOrdinal(columnName));
        }

        internal static DateTime GetDateTimeSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            DateTime? result = GetDateTimeSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        internal static DateTime? GetDateTimeSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return null;
            }
            else
            {
                return dataReader.GetDateTime(columnIndex);
            }
        }

        #endregion

        #region Get varbinary

        internal static Stream GetStream(SqlDataReader dataReader, string columnName, string errorMessage = "")
        {
            try
            {
                return dataReader.GetStream(dataReader.GetOrdinal(columnName));
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static Image GetStreamAsImage(SqlDataReader dataReader, string columnName)
        {
            Stream stream = GetStream(dataReader, columnName);
            if (stream == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return Bitmap.FromStream(stream);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #endregion

        #region Get values - object

        internal static string ObjectGetString(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }
            else
            {
                return Convert.ToString(value);
            }
        }

        internal static byte? ObjectGetByte(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }
            else
            {
                return Convert.ToByte(value);
            }
        }

        internal static short? ObjectGetShort(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(value);
            }
        }

        internal static int? ObjectGetInteger(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        internal static long? ObjectGetLong(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }
            else
            {
                return Convert.ToInt64(value);
            }
        }

        internal static decimal? ObjectGetDecimal(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(value);
            }
        }

        internal static bool? ObjectGetBoolean(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(value);
            }
        }

        internal static DateTime? ObjectGetDateTime(object value)
        {
            if (DBNull.Value.Equals(value))
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(value);
            }
        }

        #endregion

    }
}