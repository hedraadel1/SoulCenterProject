//-----------------------------------------------------------------------
// <copyright file="IFilteringOperations.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Interface for filtering operations.
    /// </summary>
    public interface IFilteringOperations
    {
        /// <summary>
        /// Retrieves entities from the database based on specified filter criteria.
        /// </summary>
        /// <param name="data">The collection of entities to filter.</param>
        /// <param name="filters">The filters to apply to the query.</param>
        /// <returns>A collection of filtered entities.</returns>
        IEnumerable<object> GetWhere(IEnumerable<object> data, IEnumerable<IFilter> filters);
    }
}
