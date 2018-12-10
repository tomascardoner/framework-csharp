using System;
using System.Data.SqlClient;

namespace CardonerSistemas
{
    class Database_SQL
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
    }
}