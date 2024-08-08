//-----------------------------------------------------------------------
// <copyright file="IFilter.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Represents a filter criteria.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Applies the filter criteria to the specified entity.
        /// </summary>
        /// <param name="entity">The entity to apply the filter to.</param>
        /// <returns>True if the entity satisfies the filter criteria, otherwise false.</returns>
        bool Apply(object entity);
    }
}
