using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SoulCenterProject.Interfaces;

namespace SoulCenterProject.Services
{
 
    
     /// <summary>
     /// Manages the memory for conversations, including context and entities.
     /// </summary>
     public class MemoryManagerService : IMemoryManager
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _inMemoryCache = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryManagerService"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public MemoryManagerService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Saves data to memory, both in RAM and the database.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation associated with the data.</param>
        /// <param name="key">The unique key for the data.</param>
        /// <param name="value">The data to be saved.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SaveToMemoryAsync(int conversationId, string key, string value)
        {
            try
            {
                // Save to in-memory cache
                _inMemoryCache[GetKey(conversationId, key)] = value;

                // Save to database
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(
                        "INSERT INTO Soul_Memory (ConversationID, Key, Value, Timestamp) VALUES (@ConversationID, @Key, @Value, @Timestamp)",
                        new
                        {
                            ConversationID = conversationId,
                            Key = key,
                            Value = value,
                            Timestamp = DateTime.Now
                        });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving data to memory.", ex);
            }
        }

        /// <summary>
        /// Retrieves data from memory. First checks RAM, then the database.
        /// </summary>
        /// <param name="conversationId">The ID of the conversation associated with the data.</param>
        /// <param name="key">The unique key for the data.</param>
        /// <returns>A task representing the asynchronous operation, containing the retrieved data, or null if not found.</returns>
        public async Task<string> GetFromMemoryAsync(int conversationId, string key)
        {
            try
            {
                // Try to retrieve from in-memory cache
                string cachedValue;
                if (_inMemoryCache.TryGetValue(GetKey(conversationId, key), out cachedValue))
                {
                    return cachedValue;
                }

                // Retrieve from database if not found in cache
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var value = connection.QueryFirstOrDefault<string>(
                        "SELECT Value FROM Soul_Memory WHERE ConversationID = @conversationId AND Key = @key",
                        new { conversationId, key });

                    // If found in database, add to cache
                    if (!string.IsNullOrEmpty(value))
                    {
                        _inMemoryCache[GetKey(conversationId, key)] = value;
                    }

                    return value;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data from memory.", ex);
            }
        }
        public async Task<List<string>> GetEntityKeysAsync(int conversationId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var keys = connection.Query<string>(
                        "SELECT Key FROM Soul_Memory WHERE ConversationID = @conversationId AND Key LIKE 'entity_%'",
                        new { conversationId }).ToList();
                    return keys;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving entity keys from memory.", ex);
            }
        }
        /// <summary>
        /// Removes data from memory (both RAM and database).
        /// </summary>
        /// <param name="conversationId">The ID of the conversation associated with the data.</param>
        /// <param name="key">The unique key for the data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ForgetMemoryAsync(int conversationId, string key)
        {
            try
            {
                // Remove from in-memory cache
                _inMemoryCache.Remove(GetKey(conversationId, key));

                // Remove from database
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(
                        "DELETE FROM Soul_Memory WHERE ConversationID = @conversationId AND Key = @key",
                        new { conversationId, key });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error forgetting data from memory.", ex);
            }
        }

        // Helper method to create a unique key for memory
        private string GetKey(int conversationId, string key)
        {
            return $"{conversationId}_{key}";
        }
    }


}
