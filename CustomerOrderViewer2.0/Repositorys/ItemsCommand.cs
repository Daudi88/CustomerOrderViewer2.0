namespace CustomerOrderViewer2._0.Repositorys
{
    using Dapper;
    using Models;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class ItemsCommand
    {
        private string _connectionString;

        public ItemsCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<ItemsModel> GetList()
        {
            List<ItemsModel> items = new();

            var sql = "Items_GetList";

            using (SqlConnection connection = new(_connectionString))
            {
                items = connection.Query<ItemsModel>(sql).ToList();
            }

            return items;
        }
    }
}
