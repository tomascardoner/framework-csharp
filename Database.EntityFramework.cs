using System.Data.Entity.Core.EntityClient;

namespace CardonerSistemas.Database
{
    static partial class EntityFramework
    {
        //private const int ErrorRelatedEntityNumber = 547;
        //private const string ErrorRelatedEntityMessage = "The DELETE statement conflicted with the REFERENCE constraint '{CONSTRAINT_NAME}'. The conflict occurred in database '{DATABASE_NAME}', table '{TABLE_NAME}', column '{COLUMN_NAME}'.";
        //private const int ErrorDuplicatedEntityNumber = 2601;
        //private const string ErrorDuplicatedEntityMessage = "Cannot insert duplicate key row in object '{TABLE_NAME}' with unique index '{UNIQUE_INDEX}'. The duplicate key value is ({VALUE}).";

        internal enum Errors : short
        {
            NoDBError,
            Unknown,
            InvalidColumn = 207,
            RelatedEntity = 547,
            DuplicatedEntity = 2601,
            PrimaryKeyViolation = 2627,
            UserDefinedError = 5000
        }

        static public string CreateConnectionString(string provider, string providerConnectionString, string modelName)
        {
            EntityConnectionStringBuilder ecb = new EntityConnectionStringBuilder()
            {
                Metadata = string.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", modelName),
                Provider = provider,
                ProviderConnectionString = providerConnectionString
            };

            return ecb.ConnectionString;
        }

        static public Errors TryDecodeDbUpdateException(System.Data.Entity.Infrastructure.DbUpdateException ex)
        {
            if (ex.InnerException is System.Data.Entity.Core.UpdateException)
            {
                if (ex.InnerException.InnerException != null && ex.InnerException.InnerException is System.Data.SqlClient.SqlException exception)
                {
                    foreach (System.Data.SqlClient.SqlError sqlError in exception.Errors)
                    {
                        return (Errors)sqlError.Number;
                    }
                }
                return Errors.Unknown;
            }
            else
            {
                return Errors.Unknown;
            }
        }
    }
}