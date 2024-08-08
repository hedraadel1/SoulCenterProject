//-----------------------------------------------------------------------
// <copyright file="DatabaseSettings.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SoulCenterProject.Helpers.Settings
{
    /// <summary>
    /// Represents database connection settings.
    /// </summary>
    /// <remarks>
    /// This class defines properties to store the server address, database name, username, and password for database
    /// connection settings.
    /// </remarks>
    public class DatabaseSettings
    {
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        /// <value>The database name.</value>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the password for authentication.
        /// </summary>
        /// <value>The password for authentication.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the server address.
        /// </summary>
        /// <value>The server address.</value>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the username for authentication.
        /// </summary>
        /// <value>The username for authentication.</value>
        public string Username { get; set; }
    }
}