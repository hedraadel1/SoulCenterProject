//-----------------------------------------------------------------------
// <copyright file="ISortingOperations.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Interface for sorting operations.
    /// </summary>
    public interface ISortingOperations
    {
        /// <summary>
        /// Sorts entities based on specified sorting criteria.
        /// </summary>
        /// <param name="data">The collection of entities to sort.</param>
        /// <param name="sortBy">The column to sort by.</param>
        /// <param name="sortDescending">Specifies whether to sort the data in descending order.</param>
        /// <returns>The sorted collection of entities.</returns>
        IEnumerable<object> Sort(IEnumerable<object> data, string sortBy, bool sortDescending);
    }
}