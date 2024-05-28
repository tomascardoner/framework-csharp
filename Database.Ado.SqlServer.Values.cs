using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace CardonerSistemas.Database.Ado
{
    internal static class SqlServerValues
    {

        #region Get values - data reader

        #region String values

        internal static string GetString(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetString(dataReader.GetOrdinal(columnName));
        }

        internal static string GetStringSafeAsValue(SqlDataReader dataReader, string columnName, string value)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return value;
            }
            else
            {
                return dataReader.GetString(columnIndex);
            }
        }

        internal static string GetStringSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetStringSafeAsValue(dataReader, columnName, null);
        }

        internal static string GetStringSafeAsEmpty(SqlDataReader dataReader, string columnName)
        {
            return GetStringSafeAsValue(dataReader, columnName, string.Empty);
        }

        #endregion

        #region Byte values

        internal static byte GetByte(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetByte(dataReader.GetOrdinal(columnName));
        }

        internal static byte? GetByteSafeAsValue(SqlDataReader dataReader, string columnName, byte? value)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return value;
            }
            else
            {
                return dataReader.GetByte(columnIndex);
            }
        }
        internal static byte GetByteSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetByteSafeAsValue(dataReader, columnName, byte.MinValue).Value;
        }

        internal static byte? GetByteSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetByteSafeAsValue(dataReader, columnName, null);
        }

        #endregion

        #region Short values

        internal static short GetShort(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt16(dataReader.GetOrdinal(columnName));
        }

        internal static short? GetShortSafeAsValue(SqlDataReader dataReader, string columnName, short? value)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return value;
            }
            else
            {
                return dataReader.GetInt16(columnIndex);
            }
        }

        internal static short GetShortSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetShortSafeAsValue(dataReader, columnName, short.MinValue).Value;
        }

        internal static short? GetShortSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetShortSafeAsValue(dataReader, columnName, null);
        }

        #endregion

        #region Integer values

        internal static int GetInteger(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt32(dataReader.GetOrdinal(columnName));
        }

        internal static int? GetIntegerSafeAsValue(SqlDataReader dataReader, string columnName, int? value)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return value;
            }
            else
            {
                return dataReader.GetInt32(columnIndex);
            }
        }

        internal static int GetIntegerSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetIntegerSafeAsValue(dataReader, columnName, int.MinValue).Value;
        }

        internal static int? GetIntegerSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetIntegerSafeAsValue(dataReader, columnName, null);
        }

        #endregion

        #region Long values

        internal static long GetLong(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt64(dataReader.GetOrdinal(columnName));
        }

        internal static long? GetLongSafeAsValue(SqlDataReader dataReader, string columnName, long? value)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return value;
            }
            else
            {
                return dataReader.GetInt64(columnIndex);
            }
        }

        internal static long GetLongSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetLongSafeAsValue(dataReader, columnName, long.MinValue).Value;
        }

        internal static long? GetLongSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetLongSafeAsValue(dataReader, columnName, null);
        }

        #endregion

        #region Decimal values

        internal static decimal GetDecimal(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDecimal(dataReader.GetOrdinal(columnName));
        }

        internal static decimal? GetDecimalSafeAsValue(SqlDataReader dataReader, string columnName, decimal? value)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return value;
            }
            else
            {
                return dataReader.GetDecimal(columnIndex);
            }
        }

        internal static decimal GetDecimalSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetDecimalSafeAsValue(dataReader, columnName, decimal.MinValue).Value;
        }

        internal static decimal? GetDecimalSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetDecimalSafeAsValue(dataReader, columnName, null);
        }

        #endregion

        #region Bool values

        public static bool GetBoolean(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetBoolean(dataReader.GetOrdinal(columnName));
        }

        internal static bool? GetBooleanSafeAsValue(SqlDataReader dataReader, string columnName, bool? value)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return value;
            }
            else
            {
                return dataReader.GetBoolean(columnIndex);
            }
        }

        internal static bool? GetBooleanSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetBooleanSafeAsValue(dataReader, columnName, null);
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

        #endregion

        #region DateTime values

        internal static DateTime GetDateTime(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDateTime(dataReader.GetOrdinal(columnName));
        }

        internal static DateTime? GetDateTimeSafeAsValue(SqlDataReader dataReader, string columnName, DateTime? value)
        {
            int columnIndex = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(columnIndex))
            {
                return value;
            }
            else
            {
                return dataReader.GetDateTime(columnIndex);
            }
        }

        internal static DateTime GetDateTimeSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            return GetDateTimeSafeAsValue(dataReader, columnName, DateTime.MinValue).Value;
        }

        internal static DateTime? GetDateTimeSafeAsNull(SqlDataReader dataReader, string columnName)
        {
            return GetDateTimeSafeAsValue(dataReader, columnName, null);
        }

        #endregion

        #region Stream values

        internal static Stream GetStream(SqlDataReader dataReader, string columnName, string errorMessage = "")
        {
            try
            {
                return dataReader.GetStream(dataReader.GetOrdinal(columnName));
            }
            catch (Exception ex)
            {
                if (errorMessage != "")
                {
                    CardonerSistemas.Error.ProcessError(ex, errorMessage);
                }
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

        #region Set values

        internal static object SetValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        internal static object SetValue(string value)
        {
            if (value == null || value.Trim() == string.Empty)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        #endregion

    }
}