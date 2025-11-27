using Microsoft.Data.SqlClient;

namespace JobOpportunity.Repository
{
    public abstract class DbEntity
    {
        private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
        {
            DataSource = "BRUNOPLAY",
            UserID = "sa",
            Password = "SenhaSql",
            InitialCatalog = "JobOpportunity",
            TrustServerCertificate = true
        };
        public readonly SqlConnection connection;
        protected DbEntity()
        {
            connection = new SqlConnection(builder.ConnectionString);
        }
    }
}
