using System.Data.Entity.Core.EntityClient;

namespace CardonerSistemas.Database
{
    static partial class EntityFramework
    {
        private const int ErrorRelatedEntityNumber = 547;
        private const string ErrorRelatedEntityMessage = "The DELETE statement conflicted with the REFERENCE constraint '{CONSTRAINT_NAME}'. The conflict occurred in database '{DATABASE_NAME}', table '{TABLE_NAME}', column '{COLUMN_NAME}'.";
        private const int ErrorDuplicatedEntityNumber = 2601;
        private const string ErrorDuplicatedEntityMessage = "Cannot insert duplicate key row in object '{TABLE_NAME}' with unique index '{UNIQUE_INDEX}'. The duplicate key value is ({VALUE}).";

        public enum Errors
        {
            NoDBError,
            Unknown,
            InvalidColumn,
            RelatedEntity,
            DuplicatedEntity,
            PrimaryKeyViolation,
            UserDefinedError
        }

        static public string CreateConnectionString(string provider, string providerConnectionString, string modelName)
        {
            EntityConnectionStringBuilder ecb = new EntityConnectionStringBuilder();

            ecb.Metadata = string.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", modelName);
            ecb.Provider = provider;
            ecb.ProviderConnectionString = providerConnectionString;

            return ecb.ConnectionString;
        }

        static public Errors TryDecodeDbUpdateException(System.Data.Entity.Infrastructure.DbUpdateException ex)
        {
            if (ex.InnerException is System.Data.Entity.Core.UpdateException)
            {
                if (ex.InnerException.InnerException != null && ex.InnerException.InnerException is System.Data.SqlClient.SqlException)
                {
                    System.Data.SqlClient.SqlException SqlException = (System.Data.SqlClient.SqlException)ex.InnerException.InnerException;

                    foreach (System.Data.SqlClient.SqlError sqlError in SqlException.Errors)
                    {
                        switch (sqlError.Number)
                        {
                            case 207:
                                return Errors.InvalidColumn;
                            case 547:
                                return Errors.RelatedEntity;
                            case 2601:
                                return Errors.DuplicatedEntity;
                            case 2627:
                                return Errors.PrimaryKeyViolation;
                            case 5000:
                                return Errors.UserDefinedError;
                            default:
                                break;
                        }
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