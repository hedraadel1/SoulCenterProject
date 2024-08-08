//-----------------------------------------------------------------------
// <copyright file="DatabaseService.cs" company="Onoo">
//     Author: Eng Hedra Adel
//     Copyright (c) Onoo. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SoulCenterProject.Modules.Ai.Model;

namespace SoulCenterProject.Modules.Ai.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString) { _connectionString = connectionString; }

        public async Task<bool> InsertMessageAsync(Message message)
        {
            using(var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new MySqlCommand(
                    "INSERT INTO conversations (user_id, text, type, tags, summary, timestamp) VALUES (@userId, @text, @type, @tags, @summary, @timestamp)",
                    connection);

                command.Parameters.AddWithValue("@userId", message.UserId);
                command.Parameters.AddWithValue("@text", message.Text);
                command.Parameters.AddWithValue("@type", message.Type.ToString());
                command.Parameters.AddWithValue("@tags", message.Tags ?? string.Empty); // Handle potential nulls
                command.Parameters.AddWithValue("@summary", message.Summary ?? string.Empty); // Handle potential nulls
                command.Parameters.AddWithValue("@timestamp", message.Timestamp);

                try
                {
                    return await command.ExecuteNonQueryAsync() > 0;
                } catch(MySqlException ex)
                {
                    // Log the specific error details (ex.Number, ex.Message, etc.)
                    Console.Error.WriteLine($"Database Error: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
