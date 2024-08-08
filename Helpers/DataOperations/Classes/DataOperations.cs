//-----------------------------------------------------------------------
// <copyright file="DataOperations.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using SoulCenterProject.DataOperations.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SoulCenterProject.DataOperations.Classes
{
    /// <summary>
    /// Provides data operations for common database tasks.
    /// </summary>
    public class DataOperations : IWriteOperations, IReadOperations
    {
        private readonly string connectionString;


        /// <summary>
        /// Initializes a new instance of the DataOperations class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>

        public DataOperations(string connectionString) { this.connectionString = connectionString; }

        private string GetSqlOperator(ComparisonOperator comparisonOperator)
        {
            switch(comparisonOperator)
            {
                case ComparisonOperator.Equal:
                    return "=";
                case ComparisonOperator.NotEqual:
                    return "!=";
                case ComparisonOperator.GreaterThan:
                    return ">";
                case ComparisonOperator.LessThan:
                    return "<";
                case ComparisonOperator.GreaterThanOrEqual:
                    return ">=";
                case ComparisonOperator.LessThanOrEqual:
                    return "<=";
                case ComparisonOperator.Contains:
                    return "LIKE";
                case ComparisonOperator.StartsWith:
                    return "LIKE";
                case ComparisonOperator.EndsWith:
                    return "LIKE";
                default:
                    throw new ArgumentException("Invalid comparison operator.");
            }
        }

        // Implementation of IWriteOperations
        // Implement creation logic using ExecuteInsert
        // Assuming data is a dictionary of column names and values
        public void Create(object data)
        {
            var dataDictionary = (Dictionary<string, object>)data;
            string tableName = string.Empty; // Specify your table name
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string columns = string.Join(",", dataDictionary.Keys);
                string values = string.Join(",", dataDictionary.Values);
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }



        // Implement deletion logic using ExecuteUpdate or custom delete query
        // Assuming data is a dictionary of column names and values
        public void Delete(object data)
        {
            var dataDictionary = (Dictionary<string, object>)data;
            string tableName = string.Empty; // Specify your table name
            string idColumnName = string.Empty; // Specify your ID column name
            object id = dataDictionary[idColumnName];
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"DELETE FROM {tableName} WHERE {idColumnName} = '{id}'";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Implementation of IDataOperations
        public int ExecuteInsert(string tableName, Dictionary<string, object> data)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string columns = string.Join(",", data.Keys);
                string values = string.Join(",", data.Values);
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string sqlQuery, Dictionary<string, object> parameters)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    foreach(var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    return command.ExecuteScalar();
                }
            }
        }

        public int ExecuteUpdate(string tableName, Dictionary<string, object> data, IDataFilter filter)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string setClause = string.Join(",", data.Select(kv => $"{kv.Key} = {kv.Value}"));
                string whereClause = $"{filter.Field} {GetSqlOperator(filter.Operator)} '{filter.Value}'";
                string query = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Retrieves an entity from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The retrieved entity, or null if not found.</returns>
        public object GetById(int id)
        {
            // Implement GetById method
            string tableName = string.Empty; // Specify your table name
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM {tableName} WHERE Id = @Id";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            var entity = new Dictionary<string, object>();

                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                entity.Add(reader.GetName(i), reader.GetValue(i));
                            }

                            return entity;
                        }
                    }
                }
            }

            return null;
        }

        public Dictionary<string, object> GetById(string tableName, object id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM {tableName} WHERE Id = @Id";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            var entity = new Dictionary<string, object>();

                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                entity.Add(reader.GetName(i), reader.GetValue(i));
                            }

                            return entity;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieves multiple entities from the database.
        /// </summary>
        /// <returns>A collection of entities retrieved from the database.</returns>
        public IEnumerable<object> GetEntities()
        {
            // Implement GetEntities method
            string tableName = string.Empty; // Specify your table name
            List<object> entities = new List<object>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM {tableName}";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var entity = new Dictionary<string, object>();

                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                entity.Add(reader.GetName(i), reader.GetValue(i));
                            }

                            entities.Add(entity);
                        }
                    }
                }
            }

            return entities;
        }


        /// <summary>
        /// Retrieves entities from the specified table with optional filtering, sorting, and paging.
        /// </summary>
        /// <param name="tableName">The name of the table containing the entities.</param>
        /// <param name="filters">A list of filters to apply to the query.</param>
        /// <param name="sortBy">A list of columns to sort the results by.</param>
        /// <param name="sortDescending">A flag indicating whether to sort the results in descending order.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of entities per page.</param>
        /// <returns>A list of dictionaries representing the retrieved entities.</returns>
        public List<Dictionary<string, object>> GetEntities(
            string tableName,
            List<IDataFilter> filters = null,
            List<string> sortBy = null,
            bool sortDescending = false,
            int? pageNumber = null,
            int? pageSize = null)
        {
            List<Dictionary<string, object>> entities = new List<Dictionary<string, object>>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                StringBuilder queryBuilder = new StringBuilder($"SELECT * FROM {tableName}");

                // Apply filters
                if(filters != null && filters.Any())
                {
                    queryBuilder.Append(" WHERE ");
                    for(int i = 0; i < filters.Count; i++)
                    {
                        if(i > 0)
                        {
                            queryBuilder.Append(" AND ");
                        }

                        queryBuilder.Append(
                            $"{filters[i].Field} {GetSqlOperator(filters[i].Operator)} @{filters[i].Field}");
                    }
                }

                // Apply sorting
                if(sortBy != null && sortBy.Any())
                {
                    queryBuilder.Append(" ORDER BY ");
                    for(int i = 0; i < sortBy.Count; i++)
                    {
                        if(i > 0)
                        {
                            queryBuilder.Append(", ");
                        }

                        queryBuilder.Append($"{sortBy[i]} {(sortDescending ? "DESC" : "ASC")}");
                    }
                }

                // Apply paging
                if(pageNumber.HasValue && pageSize.HasValue)
                {
                    int offset = (pageNumber.Value - 1) * pageSize.Value;
                    queryBuilder.Append($" OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY");
                }

                string query = queryBuilder.ToString();

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    if(filters != null && filters.Any())
                    {
                        for(int i = 0; i < filters.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@{filters[i].Field}", filters[i].Value);
                        }
                    }

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var entity = new Dictionary<string, object>();
                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                entity.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            entities.Add(entity);
                        }
                    }
                }
            }

            return entities;
        }

        /// <summary>
        /// Retrieves entities from the database based on specified filters.
        /// </summary>
        /// <param name="tableName">The name of the table containing the entities.</param>
        /// <param name="filters">A list of filters to apply to the query.</param>
        /// <returns>A list of dictionaries representing the retrieved entities.</returns>
        public List<Dictionary<string, object>> GetWhere(string tableName, List<IDataFilter> filters)
        {
            List<Dictionary<string, object>> entities = new List<Dictionary<string, object>>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                StringBuilder queryBuilder = new StringBuilder($"SELECT * FROM {tableName}");

                // Apply filters
                if(filters != null && filters.Any())
                {
                    queryBuilder.Append(" WHERE ");
                    for(int i = 0; i < filters.Count; i++)
                    {
                        if(i > 0)
                        {
                            queryBuilder.Append(" AND ");
                        }

                        queryBuilder.Append(
                            $"{filters[i].Field} {GetSqlOperator(filters[i].Operator)} @{filters[i].Field}");
                    }
                }

                string query = queryBuilder.ToString();

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    if(filters != null && filters.Any())
                    {
                        for(int i = 0; i < filters.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@{filters[i].Field}", filters[i].Value);
                        }
                    }

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var entity = new Dictionary<string, object>();
                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                entity.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            entities.Add(entity);
                        }
                    }
                }
            }

            return entities;
        }

        /// <summary>
        /// Retrieves entities from the database based on specified options such as filters, sorting, paging, etc.
        /// </summary>
        /// <param name="tableName">The name of the table containing the entities.</param>
        /// <param name="filters">A list of filters to apply to the query.</param>
        /// <param name="sortBy">A list of columns to sort the results by.</param>
        /// <param name="sortDescending">A flag indicating whether to sort the results in descending order.</param>
        /// <param name="take">The number of entities to take (maximum).</param>
        /// <param name="skip">The number of entities to skip (for paging).</param>
        /// <returns>A list of dictionaries representing the retrieved entities.</returns>
        public List<Dictionary<string, object>> GetWithOptions(
            string tableName,
            List<IDataFilter> filters = null,
            List<string> sortBy = null,
            bool sortDescending = false,
            int? take = null,
            int? skip = null)
        {
            List<Dictionary<string, object>> entities = new List<Dictionary<string, object>>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                StringBuilder queryBuilder = new StringBuilder($"SELECT * FROM {tableName}");

                // Apply filters
                if(filters != null && filters.Any())
                {
                    queryBuilder.Append(" WHERE ");
                    for(int i = 0; i < filters.Count; i++)
                    {
                        if(i > 0)
                        {
                            queryBuilder.Append(" AND ");
                        }

                        queryBuilder.Append(
                            $"{filters[i].Field} {GetSqlOperator(filters[i].Operator)} @{filters[i].Field}");
                    }
                }

                // Apply sorting
                if(sortBy != null && sortBy.Any())
                {
                    queryBuilder.Append(" ORDER BY ");
                    for(int i = 0; i < sortBy.Count; i++)
                    {
                        if(i > 0)
                        {
                            queryBuilder.Append(", ");
                        }

                        queryBuilder.Append($"{sortBy[i]} {(sortDescending ? "DESC" : "ASC")}");
                    }
                }

                // Apply paging
                if(take.HasValue)
                {
                    queryBuilder.Append($" OFFSET {skip ?? 0} ROWS FETCH NEXT {take} ROWS ONLY");
                }

                string query = queryBuilder.ToString();

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    if(filters != null && filters.Any())
                    {
                        for(int i = 0; i < filters.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@{filters[i].Field}", filters[i].Value);
                        }
                    }

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var entity = new Dictionary<string, object>();
                            for(int i = 0; i < reader.FieldCount; i++)
                            {
                                entity.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            entities.Add(entity);
                        }
                    }
                }
            }

            return entities;
        }

        public void Update(object data)
        {
            // Implement update logic using ExecuteUpdate
            // Assuming data is a dictionary of column names and values
            var dataDictionary = (Dictionary<string, object>)data;
            string tableName = string.Empty; // Specify your table name
            string idColumnName = string.Empty; // Specify your ID column name
            object id = dataDictionary[idColumnName];
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string setClause = string.Join(",", dataDictionary.Select(kv => $"{kv.Key} = '{kv.Value}'"));
                string query = $"UPDATE {tableName} SET {setClause} WHERE {idColumnName} = '{id}'";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}