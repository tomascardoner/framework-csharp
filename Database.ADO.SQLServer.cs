using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CardonerSistemas.Database.ADO
{
    class SQLServer
    {
        #region Properties

        public string ApplicationName { get; set; }
        public string AttachDBFilename { get; set; }
        public string Datasource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public bool MultipleActiveResultsets { get; set; }
        public string WorkstationID { get; set; }
        public string ConnectionString { get; set; }

        public SqlConnection Connection { get; set; }

        #endregion

        #region Connection

        public void CreateConnectionString()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

            scsb.ApplicationName = ApplicationName;
            scsb.DataSource = Datasource;
            if (AttachDBFilename != null && AttachDBFilename.Trim().Length > 0)
            {
                scsb.AttachDBFilename = AttachDBFilename;
            }
            if (InitialCatalog != null && InitialCatalog.Trim().Length > 0)
            {
                scsb.InitialCatalog = InitialCatalog;
            }
            if (UserID != null && UserID.Trim().Length > 0)
            {
                scsb.UserID = UserID;
            }
            if (Password != null && Password.Trim().Length > 0)
            {
                scsb.Password = Password;
            }
            scsb.MultipleActiveResultSets = MultipleActiveResultsets;
            scsb.WorkstationID = WorkstationID;

            ConnectionString = scsb.ConnectionString;
        }

        public bool Connect()
        {
            try
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, "Error al conectarse a la Base de Datos.");
                return false;
            }
        }

        public bool IsConnected()
        {
            return !(Connection == null || Connection.State == ConnectionState.Closed || Connection.State == ConnectionState.Broken);
        }

        public bool Close()
        {
            if (IsConnected())
            {
                try
                {
                    Connection.Close();
                    Connection = null;
                    return true;
                }
                catch (Exception ex)
                {
                    CardonerSistemas.Error.ProcessError(ex, "Error al cerrar la conexión a la Base de Datos.");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Retrieve data

        public bool OpenDataReader(ref SqlDataReader dataReader, string commandText, CommandType commandType, CommandBehavior commandBehavior, string errorMessage)
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Connection;
                Command.CommandText = commandText;
                Command.CommandType = commandType;

                dataReader = Command.ExecuteReader(commandBehavior);

                Command = null;

                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataSet(ref SqlDataAdapter dataAdapter, ref DataSet dataSet, string selectCommandText, string sourceTable, string errorMessage)
        {
            try
            {
                dataSet = new DataSet();
                dataAdapter = new SqlDataAdapter(selectCommandText, Connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdapter.Fill(dataSet, sourceTable);

                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataTable(ref DataTable dataTable, string selectCommandText, string sourceTable, string errorMessage)
        {
            try
            {
                SqlDataAdapter dataAdapter = null;
                DataSet dataSet = null;

                if (OpenDataSet(ref dataAdapter, ref dataSet, selectCommandText, sourceTable, errorMessage))
                {
                    dataSet.Tables[0].TableName = sourceTable;
                    dataTable = dataSet.Tables[sourceTable];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool Execute(string commandText, CommandType commandType, string errorMessage)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = Connection;
                command.CommandText = commandText;
                command.CommandType = commandType;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool Execute(string commandText, CommandType commandType, SqlParameterCollection sqlParameterCollection, string errorMessage)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = Connection;
                command.CommandText = commandText;
                command.CommandType = commandType;
                foreach (SqlParameter parameter in sqlParameterCollection)
                {
                    command.Parameters.Add(parameter);
                }
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        #endregion

        #region Get values - data reader

        public static string DataReaderGetString(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetString(dataReader.GetOrdinal(columnName));
        }

        public static string DataReaderGetStringSafeAsEmpty(SqlDataReader dataReader, string columnName)
        {
            string result = DataReaderGetStringSafeAsNull(dataReader, columnName);
            if (result == null)
            {
                return String.Empty;
            }
            else
            {
                return result;
            }
        }

        public static string DataReaderGetStringSafeAsNull(SqlDataReader dataReader, string columnName)
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

        public static byte DataReaderGetByte(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetByte(dataReader.GetOrdinal(columnName));
        }

        public static byte DataReaderGetByteSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            byte? result = DataReaderGetByteSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return byte.MinValue;
            }
        }

        public static byte? DataReaderGetByteSafeAsNull(SqlDataReader dataReader, string columnName)
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

        public static short DataReaderGetShort(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt16(dataReader.GetOrdinal(columnName));
        }

        public static short DataReaderGetShortSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            short? result = DataReaderGetShortSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return short.MinValue;
            }
        }

        public static short? DataReaderGetShortSafeAsNull(SqlDataReader dataReader, string columnName)
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

        public static int DataReaderGetInteger(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt32(dataReader.GetOrdinal(columnName));
        }

        public static int DataReaderGetIntegerSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            int? result = DataReaderGetIntegerSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return int.MinValue;
            }
        }

        public static int? DataReaderGetIntegerSafeAsNull(SqlDataReader dataReader, string columnName)
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

        public static long DataReaderGetLong(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt64(dataReader.GetOrdinal(columnName));
        }

        public static long DataReaderGetLongSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            long? result = DataReaderGetLongSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return long.MinValue;
            }
        }

        public static long? DataReaderGetLongSafeAsNull(SqlDataReader dataReader, string columnName)
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

        public static decimal DataReaderGetDecimal(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDecimal(dataReader.GetOrdinal(columnName));
        }

        public static decimal DataReaderGetDecimalSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            decimal? result = DataReaderGetDecimalSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return decimal.MinValue;
            }
        }

        public static decimal? DataReaderGetDecimalSafeAsNull(SqlDataReader dataReader, string columnName)
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

        public static bool DataReaderGetBoolean(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetBoolean(dataReader.GetOrdinal(columnName));
        }

        public static byte DataReaderGetBooleanSafeAsByte(SqlDataReader dataReader, string columnName)
        {
            bool? result = DataReaderGetBooleanSafeAsNull(dataReader, columnName);
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

        public static bool? DataReaderGetBooleanSafeAsNull(SqlDataReader dataReader, string columnName)
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

        public static DateTime DataReaderGetDateTime(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDateTime(dataReader.GetOrdinal(columnName));
        }

        public static DateTime DataReaderGetDateTimeSafeAsMinValue(SqlDataReader dataReader, string columnName)
        {
            DateTime? result = DataReaderGetDateTimeSafeAsNull(dataReader, columnName);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime? DataReaderGetDateTimeSafeAsNull(SqlDataReader dataReader, string columnName)
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

        public static Stream DataReaderGetStream(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetStream(dataReader.GetOrdinal(columnName));
        }

        #endregion

        #region Get values - object

        public static string ObjectGetString(object value)
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

        public static byte? ObjectGetByte(object value)
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

        public static short? ObjectGetShort(object value)
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

        public static int? ObjectGetInteger(object value)
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

        public static long? ObjectGetLong(object value)
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

        public static decimal? ObjectGetDecimal(object value)
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

        public static bool? ObjectGetBoolean(object value)
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

        public static DateTime? ObjectGetDateTime(object value)
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