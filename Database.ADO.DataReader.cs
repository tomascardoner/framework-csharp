using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace CardonerSistemas.Database.Ado
{
    internal class DataReader
    {

        #region Misc

        internal static bool IsDBNull(SqlDataReader dataReader, string columnName)
        { 
            return dataReader.IsDBNull(dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get Values - String

        internal static string GetString(SqlDataReader dataReader, int columnOrdinal)
        {
            return dataReader.GetString(columnOrdinal);
        }

        internal static string GetString(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetString(dataReader.GetOrdinal(columnName));
        }

        internal static string GetStringSafeAsEmpty(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return System.String.Empty;
            }
            else
            {
                return dataReader.GetString(columnOrdinal);
            }
        }

        internal static string GetStringSafeAsEmpty(SqlDataReader dataReader, string columnName)
        {
            return GetStringSafeAsEmpty(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static string GetStringSafeAsNull(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return null;
            }
            else
            {
                return dataReader.GetString(columnOrdinal);
            }
        }

        internal static string GetStringSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetStringSafeAsNull(dataReader, dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get values - Byte

        internal static byte GetByte(SqlDataReader dataReader, int columnOrdinal)
        {
            return dataReader.GetByte(columnOrdinal);
        }

        internal static byte GetByte(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetByte(dataReader.GetOrdinal(columnName));
        }

        internal static byte GetByteSafeAsMinValue(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return Byte.MinValue;
            }
            else
            {
                return dataReader.GetByte(columnOrdinal);
            }
        }

        internal static byte GetByteSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetByteSafeAsMinValue(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static byte? GetByteSafeAsNull(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return null;
            }
            else
            {
                return dataReader.GetByte(columnOrdinal);
            }
        }

        internal static byte? GetByteSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetByteSafeAsNull(dataReader, dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get values - Short

        internal static short GetShort(SqlDataReader dataReader, int columnOrdinal)
        {
            return dataReader.GetInt16(columnOrdinal);
        }

        internal static short GetShort(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt16(dataReader.GetOrdinal(columnName));
        }

        internal static short GetShortSafeAsMinValue(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return Byte.MinValue;
            }
            else
            {
                return dataReader.GetInt16(columnOrdinal);
            }
        }

        internal static short GetShortSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetShortSafeAsMinValue(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static short? GetShortSafeAsNull(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return null;
            }
            else
            {
                return dataReader.GetInt16(columnOrdinal);
            }
        }

        internal static short? GetShortSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetShortSafeAsNull(dataReader, dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get values - Integer

        internal static int GetInteger(SqlDataReader dataReader, int columnOrdinal)
        {
            return dataReader.GetInt32(columnOrdinal);
        }

        internal static int GetInteger(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt32(dataReader.GetOrdinal(columnName));
        }

        internal static int GetIntegerSafeAsMinValue(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return Byte.MinValue;
            }
            else
            {
                return dataReader.GetInt32(columnOrdinal);
            }
        }

        internal static int GetIntegerSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetIntegerSafeAsMinValue(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static int? GetIntegerSafeAsNull(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return null;
            }
            else
            {
                return dataReader.GetInt32(columnOrdinal);
            }
        }

        internal static int? GetIntegerSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetIntegerSafeAsNull(dataReader, dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get values - Long

        internal static long GetLong(SqlDataReader dataReader, int columnOrdinal)
        {
            return dataReader.GetInt64(columnOrdinal);
        }

        internal static long GetLong(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt64(dataReader.GetOrdinal(columnName));
        }

        internal static long GetLongSafeAsMinValue(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return Byte.MinValue;
            }
            else
            {
                return dataReader.GetInt64(columnOrdinal);
            }
        }

        internal static long GetLongSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetLongSafeAsMinValue(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static long? GetLongSafeAsNull(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return null;
            }
            else
            {
                return dataReader.GetInt64(columnOrdinal);
            }
        }

        internal static long? GetLongSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetLongSafeAsNull(dataReader, dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get values - Decimal

        internal static decimal GetDecimal(SqlDataReader dataReader, int columnOrdinal)
        {
            return dataReader.GetDecimal(columnOrdinal);
        }

        internal static decimal GetDecimal(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDecimal(dataReader.GetOrdinal(columnName));
        }

        internal static decimal GetDecimalSafeAsMinValue(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return Byte.MinValue;
            }
            else
            {
                return dataReader.GetDecimal(columnOrdinal);
            }
        }

        internal static decimal GetDecimalSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetDecimalSafeAsMinValue(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static decimal? GetDecimalSafeAsNull(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return null;
            }
            else
            {
                return dataReader.GetDecimal(columnOrdinal);
            }
        }

        internal static decimal? GetDecimalSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetDecimalSafeAsNull(dataReader, dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get values - Boolean

        public static bool GetBoolean(SqlDataReader dataReader, int columnOrdinal)
        {
            return dataReader.GetBoolean(columnOrdinal);
        }

        public static bool GetBoolean(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetBoolean(dataReader.GetOrdinal(columnName));
        }

        internal static byte GetBooleanSafeAsByte(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return 2;
            }
            else
            {
                if (dataReader.GetBoolean(columnOrdinal))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        internal static byte GetBooleanSafeAsByte(SqlDataReader dataReader, string columnName)
        {
            return GetBooleanSafeAsByte(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static bool? GetBooleanSafeAsNull(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return null;
            }
            else
            {
                return dataReader.GetBoolean(columnOrdinal);
            }
        }

        internal static bool? GetBooleanSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetBooleanSafeAsNull(dataReader, columnName);
        }

        #endregion

        #region Get values - DateTime

        internal static DateTime GetDateTime(SqlDataReader dataReader, int columnOrdinal)
        {
            return dataReader.GetDateTime(columnOrdinal);
        }

        internal static DateTime GetDateTime(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDateTime(dataReader.GetOrdinal(columnName));
        }

        internal static DateTime GetDateTimeSafeAsMinValue(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return DateTime.MinValue;
            }
            else
            {
                return dataReader.GetDateTime(columnOrdinal);
            }
        }

        internal static DateTime GetDateTimeSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetDateTimeSafeAsMinValue(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static DateTime? GetDateTimeSafeAsNull(SqlDataReader dataReader, int columnOrdinal)
        {
            if (dataReader.IsDBNull(columnOrdinal))
            {
                return null;
            }
            else
            {
                return dataReader.GetDateTime(columnOrdinal);
            }
        }

        internal static DateTime? GetDateTimeSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetDateTimeSafeAsNull(dataReader, dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get values - Binary

        internal static Stream GetStream(SqlDataReader dataReader, int columnOrdinal)
        {
            try
            {
                return dataReader.GetStream(columnOrdinal);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static Stream GetStream(SqlDataReader dataReader, string columnName)
        {
            return GetStream(dataReader, dataReader.GetOrdinal(columnName));
        }

        internal static Image GetStreamAsImage(SqlDataReader dataReader, int columnOrdinal)
        {
            Stream stream = GetStream(dataReader, columnOrdinal);
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

        internal static Image GetStreamAsImage(SqlDataReader dataReader, string columnName)
        {
            return GetStreamAsImage(dataReader, dataReader.GetOrdinal(columnName));
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