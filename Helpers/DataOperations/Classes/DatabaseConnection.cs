//-----------------------------------------------------------------------
// <copyright file="DatabaseConnection.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//using DevExpress.DataAccess.Sql;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SoulCenterProject.DataOperations.Interface;
using SoulCenterProject.Helpers.Settings;
using System;
using System.Data;
using System.IO;

namespace SoulCenterProject.DataOperations.Classes
{
    /// <summary>
    /// Represents a class for managing database connections and transactions.
    /// </summary>
    internal class DatabaseConnection : IDatabaseConnection, IDisposable
    {
        private static readonly string SettingsFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Helpers",
            "JsonFiles",
            "DatabaseSettings.json");

        private IDbConnection connection;

        //    private static readonly string SettingsFilePath = Path.Combine("Helpers", "JsonFiles", "DatabaseSettings.json");

        private readonly string connectionString;
        private IDbTransaction transaction;


        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnection"/> class.
        /// </summary>
        public DatabaseConnection()
        {
            DatabaseSettings databaseSettings = LoadDatabaseSettings();
            connectionString = GetConnectionString(databaseSettings);
            connection = new MySqlConnection(connectionString);
        }


        /// <summary>
        /// Constructs the connection string based on the provided database settings.
        /// </summary>
        /// <param name="settings">The database settings.</param>
        /// <returns>The constructed connection string.</returns>
        private string GetConnectionString(DatabaseSettings settings)
        {
            if(settings != null)
            {
                // Construct and return connection string based on settings
                return $"Server={settings.Server};Database={settings.Database};User Id={settings.Username};Password={settings.Password};";
            } else
            {
                // Handle case where settings are null
                return null;
            }
        }


        /// <summary>
        /// Loads database settings from the settings file.
        /// </summary>
        /// <returns>The loaded database settings.</returns>
        private DatabaseSettings LoadDatabaseSettings()
        {
            if(File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                return JsonConvert.DeserializeObject<DatabaseSettings>(json);
            } else
            {
                // Handle file not found or empty settings file
                return null;
            }
        }

        public IDbTransaction BeginTransaction()
        {
            if(transaction == null)
            {
                transaction = connection.BeginTransaction();
            }

            return transaction;
        }

        public void Close()
        {
            if(connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public void CommitTransaction()
        {
            if(transaction != null)
            {
                transaction.Commit();
                transaction = null;
            }
        }

        public IDbCommand CreateCommand() { return connection.CreateCommand(); }


        /// <summary>
        /// Releases the resources used by the <see cref="DatabaseConnection"/> object.
        /// </summary>
        public void Dispose()
        {
            if(transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
            if(connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }

        public void Open()
        {
            if(connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                } catch(MySqlException ex)
                {
                    // Log the specific MySQL error  
                    // throw new DatabaseConnectionException("Failed to open database connection", ex);
                }
            }
        }

        public void RollbackTransaction()
        {
            if(transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
        }
    }
}

