namespace CustomerOrderViewer2._0.Repositorys
{
    using Dapper;
    using Models;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class CustomersCommand
    {
        private string _connectionString;

        public CustomersCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<CustomersModel> GetList()
        {
            List<CustomersModel> customers = new();

            var sql = "Customers_GetList";

            using (SqlConnection connection = new(_connectionString))
            {
                customers = connection.Query<CustomersModel>(sql).ToList();
            }

            return customers;
        }
    }
}
