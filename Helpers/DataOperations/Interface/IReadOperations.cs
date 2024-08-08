using System.Collections.Generic;

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Interface for read operations.
    /// </summary>
    public interface IReadOperations : IDataOperations
    {


        /// <summary>
        /// This method takes a table name and an ID as input parameters and returns a dictionary
        /// containing the data of the record with the given ID from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to retrieve the record from.</param>
        /// <param name="id">The id of the record to retrieve.</param>
        /// <returns>A dictionary containing the retrieved record.</returns>

        Dictionary<string, object> GetById(string tableName, object id);





        /// <summary>
        /// This method takes a table name and a list of filters as input parameters and returns a list of dictionaries
        /// containing the data of the records that match the given filters from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to retrieve the records from.</param>
        /// <param name="filters">A list of filters to apply to the query.</param>
        /// <returns>A list of dictionaries containing the retrieved records.</returns>
        List<Dictionary<string, object>> GetWhere(string tableName, List<IDataFilter> filters);

        /// <summary>
        /// This method takes a table name and several optional parameters, including filters, sort by, sort descending,
        /// take, and skip. It returns a list of dictionaries containing the data of the records that match the given filters and options from the specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to retrieve the records from.</param>
        /// <param name="filters">A list of filters to apply to the query.</param>
        /// <param name="sortBy">A list of column names to sort the results by.</param>
        /// <param name="sortDescending">A boolean value indicating whether to sort in descending order.</param>
        /// <param name="take">The maximum number of records to retrieve.</param>
        /// <param name="skip">The number of records to skip before starting the retrieval.</param>
        /// <returns>A list of dictionaries containing the retrieved records.</returns>
        List<Dictionary<string, object>> GetWithOptions(string tableName, List<IDataFilter> filters = null, List<string> sortBy = null, bool sortDescending = false, int? take = null, int? skip = null);
    }
}