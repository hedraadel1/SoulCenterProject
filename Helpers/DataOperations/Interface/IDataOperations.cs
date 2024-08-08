//-----------------------------------------------------------------------
// <copyright file="IDataOperations.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Provides methods for common database operations.
    /// </summary>
    public interface IDataOperations
    {
        /// <summary>
        /// Executes an insert operation in the database.
        /// </summary>
        /// <param name="tableName">The name of the table to insert data into.</param>
        /// <param name="data">A dictionary containing the column names and values to insert.</param>
        /// <returns>The number of rows affected.</returns>
        int ExecuteInsert(string tableName, Dictionary<string, object> data);

        /// <summary>
        /// Executes a scalar query in the database and returns the result.
        /// </summary>
        /// <param name="sqlQuery">The SQL query to execute.</param>
        /// <param name="parameters">A dictionary containing the query parameters.</param>
        /// <returns>The result of the scalar query.</returns>
        object ExecuteScalar(string sqlQuery, Dictionary<string, object> parameters);

        /// <summary>
        /// Executes an update operation in the database.
        /// </summary>
        /// <param name="tableName">The name of the table to update data in.</param>
        /// <param name="data">A dictionary containing the column names and values to update.</param>
        /// <param name="filter">The filter to apply to the update operation.</param>
        /// <returns>The number of rows affected.</returns>
        int ExecuteUpdate(string tableName, Dictionary<string, object> data, IDataFilter filter);

        /// <summary>
        /// Retrieves an entity from the database by its unique identifier.
        /// </summary>
        /// <param name="tableName">The name of the table containing the entity.</param>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A dictionary representing the retrieved entity.</returns>
        Dictionary<string, object> GetById(string tableName, object id);

        /// <summary>
        /// Retrieves a list of entities from the database.
        /// </summary>
        /// <param name="tableName">The name of the table containing the entities.</param>
        /// <param name="filters">A list of filters to apply to the query.</param>
        /// <param name="sortBy">A list of columns to sort the results by.</param>
        /// <param name="sortDescending">A flag indicating whether to sort the results in descending order.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of entities per page.</param>
        /// <returns>A list of dictionaries representing the retrieved entities.</returns>
        List<Dictionary<string, object>> GetEntities(
            string tableName,
            List<IDataFilter> filters = null,
            List<string> sortBy = null,
            bool sortDescending = false,
            int? pageNumber = null,
            int? pageSize = null);
    }
}