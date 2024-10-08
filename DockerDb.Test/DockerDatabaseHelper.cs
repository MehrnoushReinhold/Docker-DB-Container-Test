using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Test
{
    public static class DockerDatabaseHelper
    {

        public static async Task WaitForSqlServer()
        {
            var connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=YourStrong@Passw0rd;";
            var retries = 5;

            while (retries > 0)
            {
                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        return; // Connection successful
                    }
                }
                catch
                {
                    retries--;
                    await Task.Delay(2000); // Wait before retrying
                }
            }

            throw new Exception("SQL Server did not become ready in time.");
        }
    }
}
