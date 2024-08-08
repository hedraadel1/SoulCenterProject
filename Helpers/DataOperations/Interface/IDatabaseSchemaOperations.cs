/// <summary>
/// Provides an interface for performing database schema operations, such as adding columns, creating foreign keys and indexes, and executing SQL queries.
/// </summary>
/// <remarks>
/// This interface is intended to be implemented by a concrete database schema operations class that handles the specific implementation details for a given database system.
/// </remarks>
using System.Collections.Generic;

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Represents an interface for database schema operations.
    /// </summary>
    internal interface IDatabaseSchemaOperations
    {
        /// <summary>
        /// Adds a new column to the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to which the new column will be added.</param>
        /// <param name="column">The details of the new column to be added.</param>
        /// <returns>True if the column was successfully added, false otherwise.</returns>
        bool AddColumn(string tableName, DatabaseField column);

        /// <summary>
        /// Creates a new foreign key constraint on the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table on which the new foreign key will be created.</param>
        /// <param name="columnName">The name of the column that will be part of the foreign key.</param>
        /// <param name="foreignTableName">The name of the table that the foreign key will reference.</param>
        /// <param name="foreignColumnName">The name of the column in the referenced table that the foreign key will reference.</param>
        /// <returns>True if the foreign key was successfully created, false otherwise.</returns>
        bool CreateForeignKey(string tableName, string columnName, string foreignTableName, string foreignColumnName);

        /// <summary>
        /// Creates a new index on the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table on which the new index will be created.</param>
        /// <param name="indexName">The name of the index to be created.</param>
        /// <param name="columnName">The name of the column on which the index will be created.</param>
        /// <param name="isUnique">A boolean value indicating whether the index should be unique or not.</param>
        /// <returns>True if the index was successfully created, false otherwise.</returns>
        bool CreateIndex(string tableName, string indexName, string columnName, bool isUnique = false);

        /// <summary>
        /// Deletes a foreign key constraint from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table from which the foreign key will be deleted.</param>
        /// <param name="foreignKeyName">The name of the foreign key to be deleted.</param>
        /// <returns>True if the foreign key was successfully deleted, false otherwise.</returns>
        bool DeleteForeignKey(string tableName, string foreignKeyName);

        /// <summary>
        /// Deletes an index from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table from which the index will be deleted.</param>
        /// <param name="indexName">The name of the index to be deleted.</param>
        /// <returns>True if the index was successfully deleted, false otherwise.</returns>
        bool DeleteIndex(string tableName, string indexName);

        /// <summary>
        /// Deletes a column from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table from which the column will be deleted.</param>
        /// <param name="columnName">The name of the column to be deleted.</param>
        /// <returns>True if the column was successfully deleted, false otherwise.</returns>
        bool DropColumn(string tableName, string columnName);

        /// <summary>
        /// Executes the specified SQL query on the database.
        /// </summary>
        /// <param name="sqlQuery">The SQL query to be executed.</param>
        /// <param name="parameters">A dictionary containing the parameters for the SQL query.</param>
        /// <returns>True if the SQL query was successfully executed, false otherwise.</returns>
        bool ExecuteSql(string sqlQuery, Dictionary<string, object> parameters);

        /// <summary>
        /// Retrieves a list of all tables in the database.
        /// </summary>
        /// <returns>A list of all tables in the database.</returns>
        List<string> GetSoulTables();

        /// <summary>
        /// Retrieves a dictionary containing the names and types of all columns in the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table for which the column details will be retrieved.</param>
        /// <returns>A dictionary containing the names and types of all columns in the specified table.</returns>
        Dictionary<string, string> GetTableColumns(string tableName);
    }
}