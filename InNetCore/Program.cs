namespace InNetCore
{
    class Program
    {
        private static void Main(string[] args)
        {
            using (var scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            {
                MsSqlServer();
                Oracle();

                scope.Complete();
            }
        }

        private static void Oracle()
        {
            using (var conn = new Oracle.ManagedDataAccess.Client.OracleConnection("User Id=some_user;Password=some_password;Data Source=some_db"))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update t_hello set id_hello = 2 where id_hello = 1";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        private static void MsSqlServer()
        {
            var builder = new System.Data.SqlClient.SqlConnectionStringBuilder
            {
                DataSource = "some_computer\\some_db",
                UserID = "some_user",
                Password = "some_password",
                InitialCatalog = "some_scheme",
                Enlist = true,
            };

            using (var conn = new System.Data.SqlClient.SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update t_hello set id_hello = 2 where id_hello = 1";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
