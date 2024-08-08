using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
 
using static HedraCode.Gemini.Dal_Dapper.DataAccess;

namespace HedraCode.Gemini.Dal_Dapper
{
    public class DataAccess
    {
        public class DataAccess : DataAccess
        {
            private readonly string _connectionString;

            public DataAccess(string connectionString)
            {
                _connectionString = connectionString;
            }

            public List<SubCategory> GetCategoriesByType(string tableType)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Soul_Categories WHERE TableType = @TableType";
                    return connection.Query<SoulCategory>(query, new { TableType = tableType }).ToList();
                }
            }

            public List<SoulCategory> GetAllCategories()
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Soul_Categories";
                    return connection.Query<SoulCategory>(query).ToList();
                }
            }

            public DataTable GetCategoriesByTypeDataTable(string tableType)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Soul_Categories WHERE TableType = @TableType";
                    var adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@TableType", tableType);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }

            public DataTable GetAllCategoriesDataTable()
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Soul_Categories";
                    var adapter = new SqlDataAdapter(query, connection);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }

            // Implement methods for InsertCategory, UpdateCategory, and DeleteCategory (if needed)
            // using Dapper's parameterization for security and following a similar approach.
        }
    }
}
