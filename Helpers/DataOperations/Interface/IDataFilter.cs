
//-----------------------------------------------------------------------
// <copyright file="IDataFilter.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// Defines an interface for a data filter that can be used to filter data based on a field, comparison operator, and value.
/// </summary>
/// <remarks>
/// The <see cref="IDataFilter"/> interface provides a way to define a data filter that can be used to filter data in a data operation.
/// The filter is defined by a field, a comparison operator, and a value to compare against.
/// </remarks>
namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Represents an interface for a data filter that can be used to filter data based on a field, comparison operator, and value.
    /// </summary>
    public interface IDataFilter
    {
        /// <summary>
        /// Gets or sets the field to filter by.
        /// </summary>
        /// <value>
        /// The field to filter by.
        /// </value>
        string Field { get; set; }

        /// <summary>
        /// Gets or sets the comparison operator to use for filtering.
        /// </summary>
        /// <value>
        /// The comparison operator to use for filtering.
        /// </value>
        ComparisonOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the value to compare against.
        /// </summary>
        /// <value>
        /// The value to compare against.
        /// </value>
        object Value { get; set; }
    }
}