//-----------------------------------------------------------------------
// <copyright file="DatabaseSchemaOperations.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using MySql.Data.MySqlClient;
using SoulCenterProject.DataOperations.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SoulCenterProject.DataOperations.Classes
{
    internal class DatabaseSchemaOperations : IDatabaseSchemaOperations
    {
        private readonly IDatabaseConnection _connection; // Requires an IDatabaseConnection implementation

        public DatabaseSchemaOperations(IDatabaseConnection connection)
        {
            _connection = connection;
            connection.Open();
        }


        /// <summary>
        /// Builds the field definition string for adding a column to a table.
        /// </summary>
        /// <param name="column">The column definition.</param>
        /// <returns>The field definition string.</returns>
        private string BuildFieldDefinition(DatabaseField column)
        {
            string dataType = GetMySqlDataType(column);
            string nullability = column.IsNullable ? "NULL" : "NOT NULL";

            if(column.IsAutoIncrement)
            {
                return $"{column.Name} INT AUTO_INCREMENT PRIMARY KEY";
            } else
            {
                return $"{column.Name} {dataType} {nullability}";
            }
        }

        /// <summary>
        /// Gets the MySQL data type based on the data type of the column.
        /// </summary>
        /// <param name="column">The column definition.</param>
        /// <returns>The corresponding MySQL data type string.</returns>
        private string GetMySqlDataType(DatabaseField column)
        {
            try
            {
                switch(column.DataType.ToLower())
                {
                    case "char":
                        return $"CHAR({column.Length})";
                    case "varchar":
                        return $"VARCHAR({column.Length})";
                    case "binary":
                        return $"BINARY({column.Length})";
                    case "varbinary":
                        return $"VARBINARY({column.Length})";
                    case "tinyblob":
                        return "TINYBLOB";
                    case "tinytext":
                        return "TINYTEXT";
                    case "text":
                        return $"TEXT({column.Length})";
                    case "blob":
                        return $"BLOB({column.Length})";
                    case "mediumtext":
                        return "MEDIUMTEXT";
                    case "mediumblob":
                        return "MEDIUMBLOB";
                    case "longtext":
                        return "LONGTEXT";
                    case "longblob":
                        return "LONGBLOB";
                    case "enum":
                        return $"ENUM({column.Length})";
                    case "set":
                        return $"SET({column.Length})";
                    case "bit":
                        return $"BIT({column.Length})";
                    case "tinyint":
                        return $"TINYINT({column.Length})";
                    case "bool":
                    case "boolean":
                        return "BOOL";
                    case "smallint":
                        return $"SMALLINT({column.Length})";
                    case "mediumint":
                        return $"MEDIUMINT({column.Length})";
                    case "int":
                    case "integer":
                        return $"INT({column.Length})";
                    case "bigint":
                        return $"BIGINT({column.Length})";
                    case "float":
                        return $"FLOAT({column.Length})";
                    case "double":
                        return $"DOUBLE({column.Length})";
                    case "double precision":
                        return $"DOUBLE PRECISION({column.Length})";
                    case "date":
                        return "DATE";
                    case "datetime":
                        return $"DATETIME(YYYY-MM-DD hh:mm:ss)";
                    case "timestamp":
                        return $"TIMESTAMP(YYYY-MM-DD hh:mm:ss)";
                    case "time":
                        return $"TIME(hh:mm:ss)";
                    case "year":
                        return "YEAR";
                    default:
                        throw new ArgumentException("Unsupported data type: " + column.DataType);
                }
            } catch(Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// Adds a new column to the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="column">The column definition (name, data type, etc.).</param>
        /// <returns>True if the column was added successfully; otherwise, false.</returns>
        public bool AddColumn(string tableName, DatabaseField column)
        {
            using(var command = _connection.CreateCommand()) // Assumes _connection gives a MySqlCommand
            {
                command.CommandText = $"ALTER TABLE {tableName} ADD COLUMN {BuildFieldDefinition(column)}";

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return false;
                }
            }
        }

        /// <summary>
        /// Creates a foreign key constraint between two tables in the database.
        /// </summary>
        /// <param name="tableName">The name of the table containing the column to which the foreign key constraint will be added.</param>
        /// <param name="columnName">The name of the column to which the foreign key constraint will be added.</param>
        /// <param name="foreignTableName">The name of the table that the foreign key references.</param>
        /// <param name="foreignColumnName">The name of the column in the referenced table.</param>
        /// <returns>True if the foreign key constraint was created successfully; otherwise, false.</returns>
        public bool CreateForeignKey(
            string tableName,
            string columnName,
            string foreignTableName,
            string foreignColumnName)
        {
            using(var command = _connection.CreateCommand())
            {
                command.CommandText =
                    $"ALTER TABLE {tableName} ADD CONSTRAINT FK_{tableName}_{columnName} FOREIGN KEY ({columnName}) REFERENCES {foreignTableName}({foreignColumnName})";

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return false;
                }
            }
        }

        /// <summary>
        /// Creates an index on a specified column of a table.
        /// </summary>
        /// <param name="tableName">The name of the table on which the index will be created.</param>
        /// <param name="indexName">The name of the index to be created.</param>
        /// <param name="columnName">The name of the column on which the index will be created.</param>
        /// <param name="isUnique">Specifies whether the index should enforce uniqueness.</param>
        /// <returns>True if the index was created successfully; otherwise, false.</returns>
        public bool CreateIndex(string tableName, string indexName, string columnName, bool isUnique = false)
        {
            using(var command = _connection.CreateCommand())
            {
                string unique = isUnique ? "UNIQUE" : string.Empty;
                command.CommandText = $"CREATE {unique} INDEX {indexName} ON {tableName}({columnName})";

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return false;
                }
            }
        }

        public bool CreateTable(string tableName, List<DatabaseField> fields)
        {
            using(var command = _connection.CreateCommand())
            {
                StringBuilder fieldDefinitions = new StringBuilder();

                // Build comma-separated field definitions
                foreach(var field in fields)
                {
                    if(fieldDefinitions.Length > 0)
                    {
                        fieldDefinitions.Append(", ");
                    }
                    fieldDefinitions.Append(BuildFieldDefinition(field));
                }

                command.CommandText = $"CREATE TABLE {tableName} ({fieldDefinitions})";

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                } catch(MySqlException ex)
                {
                    // Handle the specific MySQL exception ...
                    return false; // Or potentially throw a custom exception
                }
            }
        }


        /// <summary>
        /// Deletes a foreign key constraint from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table containing the foreign key constraint to be deleted.</param>
        /// <param name="foreignKeyName">The name of the foreign key constraint to be deleted.</param>
        /// <returns>True if the foreign key constraint was deleted successfully; otherwise, false.</returns>
        public bool DeleteForeignKey(string tableName, string foreignKeyName)
        {
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = $"ALTER TABLE {tableName} DROP FOREIGN KEY {foreignKeyName}";

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return false;
                }
            }
        }


        /// <summary>
        /// Deletes an index from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table containing the index to be deleted.</param>
        /// <param name="indexName">The name of the index to be deleted.</param>
        /// <returns>True if the index was deleted successfully; otherwise, false.</returns>
        public bool DeleteIndex(string tableName, string indexName)
        {
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = $"DROP INDEX {indexName} ON {tableName}";

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return false;
                }
            }
        }


        /// <summary>
        /// Drops a column from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table from which the column will be dropped.</param>
        /// <param name="columnName">The name of the column to be dropped.</param>
        /// <returns>True if the column was dropped successfully; otherwise, false.</returns>
        public bool DropColumn(string tableName, string columnName)
        {
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = $"ALTER TABLE {tableName} DROP COLUMN {columnName}";

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return false;
                }
            }
        }


        /// <summary>
        /// Executes a general SQL query on the database.  Use for non-schema operations.
        /// </summary>
        /// <param name="sqlQuery">The SQL query string.</param>
        /// <param name="parameters">A dictionary of parameters to prevent SQL injection.</param> 
        /// <returns>
        /// True if the query executed without throwing an exception; otherwise, false. To get query results, use other
        /// specialized data access methods.
        /// </returns>
        public bool ExecuteSql(string sqlQuery, Dictionary<string, object> parameters)
        {
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = sqlQuery;

                // Clear any existing parameters
                command.Parameters.Clear();

                // Add parameters to the command
                foreach(var param in parameters)
                {
                    // Use MySqlParameterCollection instead of IDataParameterCollection
                    var parameter = new MySqlParameter(param.Key, param.Value);
                    command.Parameters.Add(parameter);
                }

                try
                {
                    command.ExecuteNonQuery(); // Assumes your query doesn't return data
                    return true;
                } catch(MySqlException ex)
                {
                    // Handle or log the exception
                    return false;
                }
            }
        }

        public List<string> GetSoulTables()
        {
            var tableNames = new List<string>();
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = @"
            SELECT table_name 
            FROM information_schema.tables 
            WHERE table_schema = DATABASE() /* Get tables from the current database */
              AND table_name LIKE 'Soul%'";  /* Filter for tables starting with 'Soul' */

                try
                {
                    using(var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            tableNames.Add(reader.GetString(0)); // Assuming table_name is the first column
                        }
                    }
                    return tableNames;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return tableNames; // Or change the return behavior based on your error handling
                }
            }
        }

        public DataTable GetSoulTablesDataTable()
        {
            var dataTable = new DataTable();
            using(var command = (MySqlCommand)_connection.CreateCommand())
            {
                command.CommandText = @"
        SELECT table_name 
        FROM information_schema.tables 
        WHERE table_schema = DATABASE() /* Get tables from the current database */
         AND table_name LIKE 'Soul%'"; /* Filter for tables starting with 'Soul' */

                try
                {
                    using(var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return dataTable; // Or change the return behavior based on your error handling
                }
            }
        }

        public List<TableInfo> GetTableAndColumnsInfo()
        {
            var allTableInfo = new List<TableInfo>();

            var tableNames = GetSoulTables();
            foreach(string tableName in tableNames)
            {
                var tableInfo = new TableInfo { TableName = tableName };
                var columns = GetTableColumnsDataTable(tableName);

                tableInfo.Columns = columns.AsEnumerable()
                    .Select(
                        row => new ColumnInfo
                        {
                            ColumnName = row.Field<string>("column_name"),
                            DataType = row.Field<string>("data_type")
                        })
                    .ToList();

                allTableInfo.Add(tableInfo);
            }

            return allTableInfo;
        }

        public Dictionary<string, string> GetTableColumns(string tableName)
        {
            var columns = new Dictionary<string, string>();
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = @"
            SELECT column_name, data_type 
            FROM information_schema.columns 
            WHERE table_schema = DATABASE() 
              AND table_name = @tableName";

                var parameter = new MySqlParameter("@tableName", MySqlDbType.VarChar);
                parameter.Value = tableName;
                command.Parameters.Add(parameter);
                try
                {
                    using(var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            columns.Add(reader.GetString(0), reader.GetString(1));
                        }
                    }
                    return columns;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return columns; // Or change the return behavior based on your error handling
                }
            }
        }

        public DataTable GetTableColumnsDataTable(string tableName)
        {
            var dataTable = new DataTable();
            using(var command = (MySqlCommand)_connection.CreateCommand())
            {
                command.CommandText = @"
        SELECT column_name, data_type 
        FROM information_schema.columns 
        WHERE table_schema = DATABASE() 
         AND table_name = @tableName";

                var parameter = new MySqlParameter("@tableName", MySqlDbType.VarChar);
                parameter.Value = tableName;
                command.Parameters.Add(parameter);

                try
                {
                    using(var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                } catch(MySqlException ex)
                {
                    // Handle the MySQL exception, log, or throw a custom exception
                    return dataTable; // Or change the return behavior based on your error handling
                }
            }
        }
    }
}
