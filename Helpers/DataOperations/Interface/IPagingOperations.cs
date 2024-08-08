//-----------------------------------------------------------------------
// <copyright file="IPagingOperations.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Interface for paging operations.
    /// </summary>
    public interface IPagingOperations
    {
        /// <summary>
        /// Retrieves a subset of entities for pagination.
        /// </summary>
        /// <param name="data">The collection of entities to page through.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of entities per page.</param>
        /// <returns>The subset of entities for the specified page.</returns>
        IEnumerable<object> Page(IEnumerable<object> data, int pageNumber, int pageSize);
    }
}
