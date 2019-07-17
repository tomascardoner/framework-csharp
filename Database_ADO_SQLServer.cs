using System;
using System.Data;
using System.Data.SqlClient;

namespace CardonerSistemas
{
    class Database_ADO_SQLServer
    {
        public string applicationName { get; set; }
        public string attachDBFilename { get; set; }
        public string datasource { get; set; }
        public string initialCatalog { get; set; }
        public string userID { get; set; }
        public string password { get; set; }
        public bool MultipleActiveResultsets { get; set; }
        public string workstationID { get; set; }
        public string connectionString { get; set; }

        public SqlConnection connection { get; set; }

        public void CreateConnectionString()
        { 
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

            scsb.ApplicationName = applicationName;
            scsb.DataSource = datasource;
            if (attachDBFilename != null && attachDBFilename.Trim().Length > 0) 
            {
                scsb.AttachDBFilename = attachDBFilename;
            }
            if (initialCatalog != null && initialCatalog.Trim().Length > 0) 
            {
                scsb.InitialCatalog = initialCatalog;
            }
            if (userID != null && userID.Trim().Length > 0) 
            {
                scsb.UserID = userID;
            }
            if (password != null && password.Trim().Length > 0) 
            {
                scsb.Password = password;
            }
            scsb.MultipleActiveResultSets = MultipleActiveResultsets;
            scsb.WorkstationID = workstationID;

            connectionString = scsb.ConnectionString;
        }

        public bool Connect()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
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
                if (connection != null)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ref ex, "Error al cerrar la conexión a la Base de Datos.");
                return false;
            }
        }

        public bool OpenDataReader(ref SqlDataReader dataReader, string commandText, CommandType commandType, CommandBehavior commandBehavior, string errorMessage)
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = connection;
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

        public bool OpenDataSet(ref SqlDataAdapter dataAdapter, ref DataSet dataSet, string selectCommandText, string sourceTable, string errorMessage)
        {
            try
            {
                dataSet = new DataSet();
                dataAdapter = new SqlDataAdapter(selectCommandText, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
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
                CardonerSistemas.Error.ProcessError(ref ex, errorMessage);
                return false;
            }
        }

        public bool Execute(string commandText, CommandType commandType, string errorMessage)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
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