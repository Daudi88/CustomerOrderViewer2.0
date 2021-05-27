namespace CustomerOrderViewer2._0.Repositorys
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using Models;

    public class CustomerOrdersCommand
    {
        private string _connectionString;

        public CustomerOrdersCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Upsert(int customerOrderId, int customerId, int itemId, string userId)
        {
            var upsertStatement = "CustomerOrderDetails_Upsert";

            var dataTable = new DataTable();
            dataTable.Columns.Add("CustomerOrderId", typeof(int));
            dataTable.Columns.Add("CustomerId", typeof(int));
            dataTable.Columns.Add("ItemId", typeof(int));
            dataTable.Rows.Add(customerOrderId, customerId, itemId);

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Execute(upsertStatement,
                    new { @CustomerOrdersType = dataTable.AsTableValuedParameter("CustomerOrdersType"), @UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }

        }

        public void Delete(int customerOrderId, string userId)
        {
            var upsertStatement = "CustomerOrderDetails_Delete";

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Execute(upsertStatement, new { @CustomerOrderId = customerOrderId, @UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public IList<CustomerOrderDetailsModel> GetList()
        {
            List<CustomerOrderDetailsModel> customerOrderDetails = new();

            var sql = "CustomerOrderDetails_GetList";

            using (SqlConnection connection = new(_connectionString))
            {
                customerOrderDetails = connection.Query<CustomerOrderDetailsModel>(sql).ToList();
            }

            return customerOrderDetails;
        }
    }
}
