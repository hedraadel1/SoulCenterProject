//-----------------------------------------------------------------------
// <copyright file="TableInfo.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/// <summary>
/// Represents a table in a database.
/// </summary>

using System.Collections.Generic;

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Represents a column in a table.
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>
        /// The name of the column.
        /// </value>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the data type of the column.
        /// </summary>
        /// <value>
        /// The data type of the column.
        /// </value>
        public string DataType { get; set; }
    }

    /// <summary>
    /// Represents a table in a database.
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// Gets or sets the list of columns in the table.
        /// </summary>
        /// <value>
        /// The list of columns in the table.
        /// </value>
        public List<ColumnInfo> Columns { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; }
    }
}