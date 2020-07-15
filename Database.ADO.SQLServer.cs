using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CardonerSistemas.Database.ADO
{
    internal class SQLServer
    {

        #region Properties

        internal string ApplicationName { get; set; }
        internal string AttachDBFilename { get; set; }
        internal string Datasource { get; set; }
        internal string InitialCatalog { get; set; }
        internal string UserId { get; set; }
        internal string PasswordEncrypted { get; set; }
        internal string Password { get; set; }
        internal bool MultipleActiveResultsets { get; set; }
        internal string WorkstationID { get; set; }
        internal string ConnectionString { get; set; }

        internal SqlConnection Connection { get; set; }

        internal bool PasswordUnencrypt()
        {
            CardonerSistemas.Encrypt.TripleDES passwordDecrypter = new CardonerSistemas.Encrypt.TripleDES(CardonerSistemas.Constants.PublicEncryptionPassword);
            string unencryptedPassword = "";
            if (!passwordDecrypter.Decrypt(PasswordEncrypted, ref unencryptedPassword))
            {
                unencryptedPassword = null;
                passwordDecrypter = null;
                return false;
            }
            Password = unencryptedPassword;
            unencryptedPassword = null;
            passwordDecrypter = null;

            return true;
        }

        #endregion

        #region Connection

        internal bool SetProperties(string datasource, string initialCatalog, string userId, string passwordEncrypted)
        {
            int selectedDatasourceIndex;

            if (datasource.Contains(CardonerSistemas.Constants.StringListSeparator))
            {
                // Muestro la ventana de selecciónde Datasource
                CardonerSistemas.Database.SelectDatasource selectDatasource = new SelectDatasource();
                selectDatasource.comboboxDataSource.Items.AddRange(datasource.Split(Convert.ToChar(CardonerSistemas.Constants.StringListSeparator)));
                if (selectDatasource.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                selectedDatasourceIndex = selectDatasource.comboboxDataSource.SelectedIndex;
                selectDatasource.Close();
                selectDatasource = null;

                // Asigno las propiedades
                Datasource = SelectProperty(datasource, selectedDatasourceIndex);
                InitialCatalog = SelectProperty(initialCatalog, selectedDatasourceIndex);
                UserId = SelectProperty(userId, selectedDatasourceIndex);
                PasswordEncrypted = SelectProperty(passwordEncrypted, selectedDatasourceIndex);
            }
            else
            {
                Datasource = datasource;
                InitialCatalog = initialCatalog;
                UserId = userId;
                PasswordEncrypted = passwordEncrypted;
            }

            return true;
        }

        private string SelectProperty(string value, int selectedIndex)
        {
            if (value.Contains(CardonerSistemas.Constants.StringListSeparator))
            {
                string[] values;
                values = value.Split(Convert.ToChar(CardonerSistemas.Constants.StringListSeparator));
                if (values.GetUpperBound(0) >= selectedIndex)
                {
                    return values[selectedIndex];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return value;
            }
        }

        internal void CreateConnectionString()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder
            {
                ApplicationName = ApplicationName,
                DataSource = Datasource
            };
            if (AttachDBFilename != null && AttachDBFilename.Trim().Length > 0)
            {
                scsb.AttachDBFilename = AttachDBFilename;
            }
            if (InitialCatalog != null && InitialCatalog.Trim().Length > 0)
            {
                scsb.InitialCatalog = InitialCatalog;
            }
            if (UserId != null && UserId.Trim().Length > 0)
            {
                scsb.UserID = UserId;
            }
            if (Password != null && Password.Trim().Length > 0)
            {
                scsb.Password = Password;
            }
            scsb.MultipleActiveResultSets = MultipleActiveResultsets;
            scsb.WorkstationID = WorkstationID;

            ConnectionString = scsb.ConnectionString;
        }

        internal bool Connect()
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

        internal bool IsConnected()
        {
            return !(Connection == null || Connection.State == ConnectionState.Closed || Connection.State == ConnectionState.Broken);
        }

        internal bool Close()
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

        internal bool OpenDataReader(ref SqlDataReader dataReader, string commandText, CommandType commandType, CommandBehavior commandBehavior, string errorMessage)
        {
            try
            {
                SqlCommand Command = new SqlCommand
                {
                    Connection = Connection,
                    CommandText = commandText,
                    CommandType = commandType
                };

                dataReader = Command.ExecuteReader(commandBehavior);

                Command.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        internal bool OpenDataSet(ref SqlDataAdapter dataAdapter, ref DataSet dataSet, string selectCommandText, string sourceTable, string errorMessage)
        {
            try
            {
                dataSet = new DataSet();
                dataAdapter = new SqlDataAdapter(selectCommandText, Connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdapter.Fill(dataSet, sourceTable);
                commandBuilder.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        internal bool OpenDataTable(ref DataTable dataTable, string selectCommandText, string sourceTable, string errorMessage)
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

        internal bool Execute(string commandText, CommandType commandType, string errorMessage)
        {
            try
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = Connection,
                    CommandText = commandText,
                    CommandType = commandType
                };
                command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        internal bool Execute(string commandText, CommandType commandType, SqlParameterCollection sqlParameterCollection, string errorMessage)
        {
            try
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = Connection,
                    CommandText = commandText,
                    CommandType = commandType
                };
                foreach (SqlParameter parameter in sqlParameterCollection)
                {
                    command.Parameters.Add(parameter);
                }
                command.ExecuteNonQuery();
                command.Dispose();
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

        internal static string DataReaderGetString(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetString(dataReader.GetOrdinal(columnName));
        }

        internal static string DataReaderGetStringSafeAsEmpty(SqlDataReader dataReader, string columnName)
        {
            string result = DataReaderGetStringSafeAsNull(dataReader, columnName);
            if (result == null)
            {
                return System.String.Empty;
            }
            else
            {
                return result;
            }
        }

        internal static string DataReaderGetStringSafeAsNull(SqlDataReader dataReader, string columnName)
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

        internal static byte DataReaderGetByte(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetByte(dataReader.GetOrdinal(columnName));
        }

        internal static byte DataReaderGetByteSafeAsMinValue(SqlDataReader dataReader, string columnName)
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

        internal static byte? DataReaderGetByteSafeAsNull(SqlDataReader dataReader, string columnName)
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

        internal static short DataReaderGetShort(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt16(dataReader.GetOrdinal(columnName));
        }

        internal static short DataReaderGetShortSafeAsMinValue(SqlDataReader dataReader, string columnName)
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

        internal static short? DataReaderGetShortSafeAsNull(SqlDataReader dataReader, string columnName)
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

        internal static int DataReaderGetInteger(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt32(dataReader.GetOrdinal(columnName));
        }

        internal static int DataReaderGetIntegerSafeAsMinValue(SqlDataReader dataReader, string columnName)
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

        internal static int? DataReaderGetIntegerSafeAsNull(SqlDataReader dataReader, string columnName)
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

        internal static long DataReaderGetLong(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetInt64(dataReader.GetOrdinal(columnName));
        }

        internal static long DataReaderGetLongSafeAsMinValue(SqlDataReader dataReader, string columnName)
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

        internal static long? DataReaderGetLongSafeAsNull(SqlDataReader dataReader, string columnName)
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

        internal static decimal DataReaderGetDecimal(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDecimal(dataReader.GetOrdinal(columnName));
        }

        internal static decimal DataReaderGetDecimalSafeAsMinValue(SqlDataReader dataReader, string columnName)
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

        internal static decimal? DataReaderGetDecimalSafeAsNull(SqlDataReader dataReader, string columnName)
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

        internal static byte DataReaderGetBooleanSafeAsByte(SqlDataReader dataReader, string columnName)
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

        internal static bool? DataReaderGetBooleanSafeAsNull(SqlDataReader dataReader, string columnName)
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

        internal static DateTime DataReaderGetDateTime(SqlDataReader dataReader, string columnName)
        {
            return dataReader.GetDateTime(dataReader.GetOrdinal(columnName));
        }

        internal static DateTime DataReaderGetDateTimeSafeAsMinValue(SqlDataReader dataReader, string columnName)
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

        internal static DateTime? DataReaderGetDateTimeSafeAsNull(SqlDataReader dataReader, string columnName)
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

        #region Data reader - Get varbinary

        internal static Stream DataReaderGetStream(SqlDataReader dataReader, string columnName, string errorMessage = "")
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

        internal static Image DataReaderGetStreamAsImage(SqlDataReader dataReader, string columnName)
        {
            Stream stream = DataReaderGetStream(dataReader, columnName);
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