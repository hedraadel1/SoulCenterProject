//-----------------------------------------------------------------------
// <copyright file="IDatabaseConnection.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Data;

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Interface for managing database connections.
    /// </summary>
    public interface IDatabaseConnection
    {
        /// <summary>
        /// Begins a database transaction.
        /// </summary>
        /// <returns>The started database transaction.</returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Closes the database connection.
        /// </summary>
        void Close();

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Creates a database command.
        /// </summary>
        /// <returns>The created database command.</returns>
        IDbCommand CreateCommand();

        /// <summary>
        /// Opens a database connection.
        /// </summary>
        void Open();

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        void RollbackTransaction();
    }
}