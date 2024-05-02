using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CardonerSistemas.Database.Ado
{
    public class SqlServer
    {

        private const string ErrorLoginFailed = "Login failed for user ";

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
        internal byte ConnectTimeout { get; set; }
        internal byte ConnectRetryCount { get; set; }
        internal byte ConnectRetryInterval { get; set; }
        internal string ConnectionString { get; set; }

        internal SqlConnection Connection { get; set; }

        internal bool PasswordEncrypt()
        {
            string encryptedPassword = string.Empty;
            if (Encrypt.StringCipher.Encrypt(Password, Constants.PublicEncryptionPassword, ref encryptedPassword))
            {
                PasswordEncrypted = encryptedPassword;
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool PasswordUnencrypt()
        {
            string unencryptedPassword = string.Empty;
            if (Encrypt.StringCipher.Decrypt(PasswordEncrypted, Constants.PublicEncryptionPassword, ref unencryptedPassword))
            {
                Password = unencryptedPassword;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Connection

        internal bool SetProperties(string datasource, string initialCatalog, string userId, string passwordEncrypted, byte connectTimeout, byte connectRetryCount, byte connectRetryInterval)
        {
            int selectedDatasourceIndex;

            if (datasource.Contains(Constants.StringListSeparator))
            {
                // Muestro la ventana de selección del Datasource
                SelectDatasource selectDatasource = new SelectDatasource();
                selectDatasource.comboboxDataSource.Items.AddRange(datasource.Split(Convert.ToChar(Constants.StringListSeparator)));
                if (selectDatasource.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                selectedDatasourceIndex = selectDatasource.comboboxDataSource.SelectedIndex;
                selectDatasource.Close();

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
            ConnectTimeout = connectTimeout;
            ConnectRetryCount = connectRetryCount;
            ConnectRetryInterval = connectRetryInterval;
            ApplicationName = My.Application.Info.Title;
            MultipleActiveResultsets = true;
            WorkstationID = Environment.MachineName;

            return true;
        }

        private string SelectProperty(string value, int selectedIndex)
        {
            if (value.Contains(Constants.StringListSeparator))
            {
                string[] values;
                values = value.Split(Convert.ToChar(Constants.StringListSeparator));
                if (values.GetUpperBound(0) >= selectedIndex)
                {
                    return values[selectedIndex];
                }
                else
                {
                    return string.Empty;
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
            scsb.ConnectTimeout = ConnectTimeout;
            scsb.ConnectRetryCount = ConnectRetryCount;
            scsb.ConnectRetryInterval = ConnectRetryInterval;
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
                Error.ProcessError(ex, "Error al conectarse a la Base de Datos.");
                return false;
            }
        }

        internal bool Connect(DatabaseConfig databaseConfig, ref bool newLoginData)
        {
            newLoginData = false;

            while (true)
            {
                try
                {
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2146232060 && ex.Message.Contains(ErrorLoginFailed))
                    {
                        // Los datos de inicio de sesión en la base de datos son incorrectos.
                        MessageBox.Show("Los datos de inicio de sesión a la base de datos son incorrectos.", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        // Pido datos nuevos.
                        LoginInfo loginInfo = new LoginInfo();
                        loginInfo.textboxUsuario.Text = UserId;
                        loginInfo.textboxPassword.Text = Password;
                        if (loginInfo.ShowDialog() != DialogResult.OK)
                        {
                            return false;
                        }
                        UserId = loginInfo.textboxUsuario.Text.TrimAndReduce();
                        databaseConfig.UserId = loginInfo.textboxUsuario.Text.TrimAndReduce();
                        Password = loginInfo.textboxPassword.Text.Trim();
                        if (Password.Length > 0)
                        {
                            if (PasswordEncrypt())
                            {
                                databaseConfig.Password = PasswordEncrypted;
                            }
                        }
                        else
                        {
                            databaseConfig.Password = string.Empty;
                        }
                        CreateConnectionString();
                        loginInfo.Close();
                        loginInfo.Dispose();
                        newLoginData = true;
                    }
                    else
                    {
                        Error.ProcessError(ex, "Error al conectarse a la Base de Datos.");
                        return false;
                    }
                }
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
                    Error.ProcessError(ex, "Error al cerrar la conexión a la Base de Datos.");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Parameters

        internal static object GetParameterValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        internal static object GetParameterValue(int selectedIndex, int minimumListIndex, string textValue)
        {
            if (selectedIndex < minimumListIndex)
            {
                return DBNull.Value;
            }
            else
            {
                return textValue;
            }
        }

        internal static object GetParameterValue(int selectedIndex, int trueListIndex, int falseListIndex)
        {
            if (selectedIndex == trueListIndex)
            {
                return 1;
            }
            else if (selectedIndex == falseListIndex)
            {
                return 0;
            }
            else
            {
                return DBNull.Value;
            }
        }

        #endregion Parameters

        #region Retrieve data

        public bool CreateCommand(out SqlCommand sqlCommand, string commandText, CommandType commandType, List<SqlParameter> parameters, string errorMessage)
        {
            try
            {
                sqlCommand = new SqlCommand()
                {
                    Connection = Connection,
                    CommandText = commandText,
                    CommandType = commandType
                };
                if (parameters != null)
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());
                }
                return true;
            }
            catch (Exception ex)
            {
                sqlCommand = null;
                Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataReader(out SqlDataReader dataReader, string commandText, CommandType commandType, CommandBehavior commandBehavior, List<SqlParameter> parameters, string errorMessage)
        {
            if (!CreateCommand(out SqlCommand sqlCommand, commandText, commandType, parameters, errorMessage))
            {
                dataReader = null;
                return false;
            }
            try
            {
                dataReader = sqlCommand.ExecuteReader(commandBehavior);
                return true;
            }
            catch (Exception ex)
            {
                dataReader = null;
                Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataReader(out SqlDataReader dataReader, string commandText, CommandType commandType, CommandBehavior commandBehavior, string errorMessage)
        {
            return OpenDataReader(out dataReader, commandText, commandType, commandBehavior, null, errorMessage);
        }

        public bool OpenDataSet(out SqlDataAdapter dataAdapter, out DataSet dataSet, string commandText, CommandType commandType, List<SqlParameter> parameters, string sourceTable, string errorMessage)
        {
            if (!CreateCommand(out SqlCommand sqlCommand, commandText, commandType, parameters, errorMessage))
            {
                dataAdapter = null;
                dataSet = null;
                return false;
            }
            try
            {
                dataSet = new DataSet();
                dataAdapter = new SqlDataAdapter(sqlCommand);
                using (SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter))
                {
                    dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    dataAdapter.Fill(dataSet, sourceTable);
                }
                return true;
            }
            catch (Exception ex)
            {
                dataAdapter = null;
                dataSet = null;
                Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataSet(out SqlDataAdapter dataAdapter, out DataSet dataSet, string commandText, CommandType commandType, string sourceTable, string errorMessage)
        {
            return OpenDataSet(out dataAdapter, out dataSet, commandText, commandType, null, sourceTable, errorMessage);
        }

        public bool OpenDataTable(out DataTable dataTable, string commandText, CommandType commandType, List<SqlParameter> parameters, string sourceTable, string errorMessage)
        {
            if (!OpenDataSet(out _, out DataSet dataSet, commandText, commandType, parameters, sourceTable, errorMessage))
            {
                dataTable = null;
                return false;
            }
            try
            {
                dataSet.Tables[0].TableName = sourceTable;
                dataTable = dataSet.Tables[sourceTable];
                return true;
            }
            catch (Exception ex)
            {
                dataTable = null;
                Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataTableFromDataReader(out DataTable dataTable, string commandText, CommandType commandType, CommandBehavior commandBehavior, List<SqlParameter> parameters, string errorMessage)
        {
            if (!OpenDataReader(out SqlDataReader sqlDataReader, commandText, commandType, commandBehavior, parameters, errorMessage))
            {
                dataTable = null;
                return false;
            }
            try
            {
                dataTable = new DataTable();
                if (sqlDataReader.HasRows)
                {
                    dataTable.Load(sqlDataReader);
                }
                sqlDataReader.Close();
                return true;
            }
            catch (Exception ex)
            {
                dataTable = null;
                Cursor.Current = Cursors.Default;
                Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataTableFromDataReader(out DataTable dataTable, string commandText, CommandType commandType, CommandBehavior commandBehavior, string errorMessage)
        {
            return OpenDataTableFromDataReader(out dataTable, commandText, commandType, commandBehavior, null, errorMessage);
        }

        public bool Execute(string commandText, CommandType commandType, List<SqlParameter> parameters, string errorMessage)
        {
            if (!CreateCommand(out SqlCommand sqlCommand, commandText, commandType, parameters, errorMessage))
            {
                return false;
            }
            try
            {
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex, errorMessage);
                return false;
            }
        }

        public bool Execute(string commandText, CommandType commandType, string errorMessage)
        {
            return Execute(commandText, commandType, null, errorMessage);
        }

        #endregion

    }
}