using System;
using System.Data;
using System.Data.OleDb;

namespace CardonerSistemas.Database.ADO
{
    class OleDb
    {
        public const string ProviderJet351 = "Microsoft.Jet.OLEDB.3.51";
        public const string ProviderJet40 = "Microsoft.Jet.OLEDB.4.0";
        public const string ProviderOffice12Access = "Microsoft.ACE.OLEDB.12.0";
        public const string ProviderAnalysisServices11 = "MSOLAP.5";
        public const string ProviderOracle = "MSDAORA.1";
        public const string ProviderSearch = "Search.CollatorDSO.1";
        public const string ProviderSQLServer = "SQLOLEDB.1";
        public const string ProviderSimple = "MSDAOSP.1";
        public const string ProviderDataShape = "MSDataShape.1";
        public const string ProviderMicrosoftDirectoryServices = "ADsDSOObject";
        public const string ProviderSQLServerNativeClient11 = "SQLNCLI11.1";

        public string Provider { get; set; }
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }

        public string ConnectionString { get; set; }

        public OleDbConnection Connection { get; set; }

        public void CreateConnectionString()
        {
            OleDbConnectionStringBuilder ocsb = new OleDbConnectionStringBuilder();

            if (!string.IsNullOrEmpty(InitialCatalog))
            {
                ocsb.Add("Initial Catalog=", InitialCatalog);
            }
            if (!string.IsNullOrEmpty(UserID))
            {
                ocsb.Add("User ID", UserID);
            }
            if (!string.IsNullOrEmpty(Password))
            {
                ocsb.Add("Password=", Password);
            }

            ocsb.Provider = Provider;
            ocsb.DataSource = DataSource;
            ConnectionString = ocsb.ConnectionString;
        }

        public bool Connect()
        {
            try
            {
                Connection = new OleDbConnection(ConnectionString);
                Connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ref ex, "Error al conectarse a la Base de Datos.");
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                if (Connection != null)
                {
                    if (Connection.State == System.Data.ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    Connection = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ref ex, "Error al cerrar la conexión a la Base de Datos.");
                return false;
            }
        }

        public bool OpenDataReader(ref OleDbDataReader dataReader, string commandText, CommandType commandType, CommandBehavior commandBehavior, string errorMessage)
        {
            try
            {
                OleDbCommand Command = new OleDbCommand();
                Command.Connection = Connection;
                Command.CommandText = commandText;
                Command.CommandType = commandType;

                dataReader = Command.ExecuteReader(commandBehavior);

                Command = null;

                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ref ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataSet(ref OleDbDataAdapter dataAdapter, ref DataSet dataSet, string selectCommandText, string sourceTable, string errorMessage)
        {
            try
            {
                dataSet = new DataSet();
                dataAdapter = new OleDbDataAdapter(selectCommandText, Connection);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter);
                dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                dataAdapter.Fill(dataSet, sourceTable);

                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ref ex, errorMessage);
                return false;
            }
        }

        public bool OpenDataTable(ref DataTable dataTable, string selectCommandText, string sourceTable, string errorMessage)
        {
            try
            {
                OleDbDataAdapter dataAdapter = null;
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
                CardonerSistemas.Error.ProcessError(ref ex, errorMessage);
                return false;
            }
        }

        public bool Execute(string commandText, CommandType commandType, string errorMessage)
        {
            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = Connection;
                command.CommandText = commandText;
                command.CommandType = commandType;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ref ex, errorMessage);
                return false;
            }
        }
    }
}
