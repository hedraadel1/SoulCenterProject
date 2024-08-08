//-----------------------------------------------------------------------
// <copyright file="IWriteOperations.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SoulCenterProject.DataOperations.Interface
{
    /// <summary>
    /// Interface for write operations (Create, Update, Delete).
    /// </summary>
    public interface IWriteOperations
    {
        /// <summary>
        /// This method is used to create a new record in the database. It takes an object as a parameter, which represents the data to be inserted.
        /// </summary>
        /// <param name="data">The object containing the data to be created.</param>
        void Create(object data);

        /// <summary>
        /// This method is used to delete a record from the database. It takes an object as a parameter, which represents the data of the record to be deleted.
        /// </summary>
        /// <param name="data">The object containing the data to be deleted.</param>
        void Delete(object data);

        /// <summary>
        /// This method is used to update an existing record in the database. It takes an object as a parameter, which represents the updated data.
        /// </summary>
        /// <param name="data">The object containing the updated data.</param>
        void Update(object data);
    }
}